using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dev2;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Deploy;
using Dev2.Common.Interfaces.ErrorHandling;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.PopupController;
using Dev2.Common.Interfaces.Runtime.ServiceModel;
using Dev2.Common.Interfaces.ServerDialogue;
using Dev2.Common.Interfaces.ServerProxyLayer;
using Dev2.Common.Interfaces.Studio;
using Dev2.Common.Interfaces.Studio.ViewModels;
using Dev2.Common.Interfaces.Studio.ViewModels.Dialogues;
using Dev2.Common.Interfaces.Toolbox;
using Dev2.Data.ServiceModel;
using Dev2.Util;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Moq;
using Warewolf.Core;
using Warewolf.Studio.Core;
using Warewolf.Studio.Core.Popup;
using Warewolf.Studio.Models.Help;
using Warewolf.Studio.Models.Toolbox;
using Warewolf.Studio.ViewModels.ToolBox;
using IVariableListViewModel = Dev2.Common.Interfaces.DataList.DatalistView.IVariableListViewModel;

namespace Warewolf.Studio.ViewModels
{
    public class ShellViewModel : BindableBase, IShellViewModel
    {
        readonly IUnityContainer _unityContainer;
        readonly IRegionManager _regionManager;
        readonly IEventAggregator _aggregator;
        IExceptionHandler _handler;
        object _activeItem;
        IServer _activeServer;
        double _menuPanelWidth;
        bool _menuExpanded;

        public ShellViewModel(IUnityContainer unityContainer, IRegionManager regionManager, IEventAggregator aggregator)
        {
            VerifyArgument.AreNotNull(new Dictionary<string, object> { { "unityContainer", unityContainer }, { "regionManager", regionManager } });
            _unityContainer = unityContainer;
            _regionManager = regionManager;
            _aggregator = aggregator;
            var localHostString = AppSettings.LocalHost;
            var localhostUri = new Uri(localHostString);
            LocalhostServer = unityContainer.Resolve<IServer>(new ParameterOverrides { { "uri", localhostUri } });
            LocalhostServer.ResourceName = "localhost (" + localHostString + ")";
            ActiveServer = LocalhostServer;
        
            _menuPanelWidth = 60;
            _menuExpanded = false;
        }

        public void Initialize()
        {

            PopupController = _unityContainer.Resolve<IPopupController>();
            _unityContainer.RegisterInstance<IToolboxViewModel>(new ToolboxViewModel(new ToolboxModel(LocalhostServer, LocalhostServer, new Mock<IPluginProxy>().Object), new ToolboxModel(LocalhostServer, LocalhostServer, new Mock<IPluginProxy>().Object),_aggregator));
           
            InitializeRegion<IExplorerView,IExplorerViewModel>(RegionNames.Explorer);
            InitializeRegion<IToolboxView, IToolboxViewModel>(RegionNames.Toolbox);
            InitializeRegion<IMenuView, IMenuViewModel>(RegionNames.Menu);
            InitializeRegion<IVariableListView, IVariableListViewModel>(RegionNames.VariableList);
            InitializeRegion<IHelpView, IHelpWindowViewModel>(RegionNames.Help);

            _handler = _unityContainer.Resolve<IExceptionHandler>();
           
            _handler.AddHandler(typeof(WarewolfInvalidPermissionsException), () => PopupController.Show(PopupMessages.GetInvalidPermissionException()));
            
        }

        public IPopupController PopupController { get;  set; }

        void InitializeRegion<T,TU>(string regionName) where T:IView
        {
            var region = _regionManager.Regions[regionName];
            var view = _unityContainer.Resolve<T>();
            view.DataContext = _unityContainer.Resolve<TU>();
            region.Add(view, regionName);
            region.Activate(view);
        }

        public bool RegionHasView(string regionName)
        {
           return GetRegionViews(regionName).Any();
        }



        public async Task<bool> CheckForNewVersion()
        {
            var versionChecker = _unityContainer.Resolve<IVersionChecker>();
            var hasNewVersion = await versionChecker.GetNewerVersionAsync();
            return hasNewVersion;
        }
        
        public async void DisplayDialogForNewVersion()
        {
            var hasNewVersion = await CheckForNewVersion();
            if (hasNewVersion)
            {
                var dialog = _unityContainer.Resolve<IWebLatestVersionDialog>();
                dialog.ShowDialog();                
            }
        }

        public void OpenVersion(Guid resourceId, string versionNumber)
        {
            //todo:
        }

        public void ExecuteOnDispatcher(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public void ServerSourceAdded(IServerSource source)
        {
            if (source != null && _aggregator != null)
            {
                _aggregator.GetEvent<ServerAddedEvent>().Publish(source); 
            }
        }

        public void EditResource(IDbSource selectedSource)
        {
            var dbSourceViewModel = new ManageDatabaseSourceViewModel(new ManageDatabaseSourceModel(ActiveServer.UpdateRepository, ActiveServer.QueryProxy, ActiveServer.ResourceName), _aggregator,selectedSource);
            GetRegion("Workspace").Add(dbSourceViewModel);
        } 
        public void EditResource(IWebServiceSource selectedSource)
        {
            var sourceViewModel = new ManageWebserviceSourceViewModel(new ManageWebServiceSourceModel(ActiveServer.UpdateRepository, ActiveServer.ResourceName), _aggregator,selectedSource);
            GetRegion("Workspace").Add(sourceViewModel);            
        }

        public void EditResource(IPluginSource selectedSource)
        {
            IManagePluginSourceModel src = new ManagePluginSourceModel(ActiveServer.UpdateRepository,ActiveServer.QueryProxy,ActiveServer.ResourceName);
            var dbSourceViewModel = new ManagePluginSourceViewModel(src, _aggregator,selectedSource);
            GetRegion("Workspace").Add(dbSourceViewModel);
        }

        public string OpenPasteWindow(string current)
        {
            var pasteView = _unityContainer.Resolve<IPasteView>();
            return  pasteView.ShowView(current);
        }

        public void ShowAboutBox(IServer server)
        {
            var splashView = _unityContainer.Resolve<ISplashView>();
            var splashViewModel = _unityContainer.Resolve<ISplashViewModel>(new ParameterOverrides { { "server", server }, { "externalProcessExecutor", _unityContainer.Resolve<IExternalProcessExecutor>() } });
            splashView.DataContext = splashViewModel;
            splashView.Show(true);

        }

        public IViewsCollection GetRegionViews(string regionName)
        {
            var region = GetRegion(regionName);
            return region.Views;
        }

        IRegion GetRegion(string regionName)
        {
            var region = _regionManager.Regions[regionName]; //get the region
            return region;
        }

        public bool RegionViewHasDataContext(string regionName)
        {
            var region = GetRegion(regionName);
            var view = region.GetView(regionName);
            var userView = view as IView;
            if (userView != null)
            {
                return userView.DataContext != null;
            }
            return false;
        }

        public void AddService(Guid resourceId, IServer server )
        {

            var region = GetRegion(RegionNames.Workspace);
            var foundViewModel = region.Views.FirstOrDefault(o =>
            {
                var viewModel = o as IServiceDesignerViewModel;
                if (viewModel == null)
                {
                    return false;
                }
                return Equals(viewModel.Resource.ResourceID, resourceId);
            });
            if (foundViewModel == null)
            {
                var resource = server.QueryProxy.FetchResourceWithXaml(resourceId);
                foundViewModel = resource.ResourceType == ResourceType.WorkflowService ? new WorkflowServiceDesignerViewModel(resource) : _unityContainer.Resolve<IServiceDesignerViewModel>(new ParameterOverrides { { "resource", resource } });
                region.Add(foundViewModel); //add the viewModel
            }
            region.Activate(foundViewModel); //active the viewModel
        }


        public void DeployService(IExplorerItemViewModel resourceToDeploy)
        {
            VerifyArgument.IsNotNull("resourceToDeploy", resourceToDeploy);
            var region = GetRegion(RegionNames.Workspace);
            var vm = region.Views.FirstOrDefault(a => a is IDeployViewModel);

            if (vm == null)
            {
                var deployVm = _unityContainer.Resolve<IDeployViewModel>();
                deployVm.SelectSourceItem(resourceToDeploy);
                region.Add(deployVm);
                region.Activate(deployVm); 
            }
            else
            {
                var deploy = vm as IDeployViewModel;
                // ReSharper disable once PossibleNullReferenceException
                deploy.SelectSourceItem(resourceToDeploy);
                region.Activate(deploy); //active the viewModel
            }
            
        }

        public void UpdateHelpDescriptor(IHelpDescriptor helpDescriptor)
        {
            //!!!!!!!/// Reviewer please note this could go either way here. Used the event aggregator to ensure order of events... but could go the other way as well cos the shell view model owns this
             VerifyArgument.IsNotNull("helpDescriptor", helpDescriptor);
              _aggregator.GetEvent<HelpChangedEvent>().Publish(helpDescriptor);
            
        }
       

        public double MenuPanelWidth
        {
            get
            {
                return _menuPanelWidth;
            }
            set
            {
                _menuPanelWidth = value;
                OnPropertyChanged(() => MenuPanelWidth);
            }
        }

        public void NewResource(ResourceType? type,Guid selectedId)
        {
            if (type == null)
                return;
            switch (type.Value)
            {
                case ResourceType.ServerSource:
                    CreateNewServerSource(selectedId);
                    break;
                case ResourceType.DbSource:
                    CreateDatabaseSource();
                    break;
                case ResourceType.WebSource:
                    CreateWebServiceSource();
                    break;
                case ResourceType.PluginSource:
                    CreatePluginSource();
                    break;
                case ResourceType.EmailSource:
                    CreateEmailSource();
                    break;
                case ResourceType.PluginService:
                    CreatePluginService();
                    break;
                case ResourceType.WebService:
                    CreateWebService();
                    break;
                case ResourceType.WorkflowService:
                    CreateWorkflowService();
                    break;
                case ResourceType.DbService:
                    CreateDbService();
                    break;
                default: return;

            }
        }

        void CreatePluginService()
        {
            var selectedId = Guid.NewGuid();
            var sourceViewModel = new ManagePluginServiceViewModel(new PluginServiceModel(ActiveServer.UpdateRepository, ActiveServer.QueryProxy, this, ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId));
            GetRegion("Workspace").Add(sourceViewModel);
        }

        void CreateDbService()
        {

            var dbServiceViewModel = new ManageDatabaseServiceViewModel(new DbServiceModel(ActiveServer.UpdateRepository,ActiveServer.QueryProxy,this,ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), Guid.NewGuid()) );
            GetRegion("Workspace").Add(dbServiceViewModel);
        }

        private void CreateWorkflowService()
        {
            var viewModel = new WorkflowServiceDesignerViewModel(new XamlResource(new SerializableResource(),null));
            GetRegion(RegionNames.Workspace).Add(viewModel);
        }

        private void CreateWebServiceSource()
        {
            var selectedId = Guid.NewGuid();
            var sourceViewModel = new ManageWebserviceSourceViewModel(new ManageWebServiceSourceModel(ActiveServer.UpdateRepository,ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId),_aggregator);
            GetRegion("Workspace").Add(sourceViewModel);
        
        }
        
        private void CreatePluginSource()
        {
            var selectedId = Guid.NewGuid();
            var sourceViewModel = new ManagePluginSourceViewModel(new ManagePluginSourceModel(ActiveServer.UpdateRepository,ActiveServer.QueryProxy,ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId),_aggregator);
            GetRegion("Workspace").Add(sourceViewModel);
        
        }

        private void CreateEmailSource()
        {
            var selectedId = Guid.NewGuid();
            var sourceViewModel = new ManageEmailSourceViewModel(new ManageEmailSourceModel(ActiveServer.UpdateRepository, ActiveServer.QueryProxy, ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId), _aggregator);
            GetRegion("Workspace").Add(sourceViewModel);

        }

        private void CreateWebService()
        {
            var selectedId = Guid.NewGuid();
            var sourceViewModel = new ManageWebServiceViewModel(new WebServiceModel(ActiveServer.UpdateRepository,ActiveServer.QueryProxy,this,ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId));
            GetRegion("Workspace").Add(sourceViewModel);
        
        }

        private void CreateDatabaseSource()
        {
            var selectedId = Guid.NewGuid();
            var dbSourceViewModel = new ManageDatabaseSourceViewModel(new ManageDatabaseSourceModel( ActiveServer.UpdateRepository,ActiveServer.QueryProxy,ActiveServer.ResourceName), new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId),_aggregator);
            GetRegion("Workspace").Add(dbSourceViewModel);
        
        }

        void CreateNewServerSource(Guid selectedId)
        {
            var server = new NewServerViewModel(new ServerSource { UserName = "", Address = "", AuthenticationType = AuthenticationType.Windows, ID = Guid.NewGuid(), Name = "", Password = "", ResourcePath = "" }, ActiveServer.UpdateRepository, new RequestServiceNameViewModel(new EnvironmentViewModel(LocalhostServer, this), _unityContainer.Resolve<IRequestServiceNameView>(), selectedId), this,
                ActiveServer.ResourceName.Substring(0,ActiveServer.ResourceName.IndexOf("(", StringComparison.Ordinal)),selectedId) { ServerSource = new ServerSource { UserName = "", Address = "", AuthenticationType = AuthenticationType.Windows, ID = Guid.NewGuid(), Name = "", Password = "", ResourcePath = "" } };
            GetRegion("Workspace").Add(server);

        }

        public void SaveService()
        {
        }

        public void ExecuteService()
        {
        }

        public void OpenScheduler()
        {
        }

        public void OpenSettings()
        {
        }

        public IServer ActiveServer
        {
            get
            {
                return _activeServer;
            }
            set
            {
                if (!value.Equals(_activeServer))
                {
                    _activeServer = value;
                    RaiseActiveServerChanged();
                }
            }
        }

        void RaiseActiveServerChanged()
        {
            if (ActiveServerChanged != null)
            {
                ActiveServerChanged();
            }
        }

        public object ActiveItem
        {
            get
            {
                return _activeItem;
            }
            set
            {
                _activeItem = value;
                RaiseActiveItemChanged();
            }
        }

        void RaiseActiveItemChanged()
        {
            if (ActiveItemChanged != null)
            {
                ActiveItemChanged();
            }
        }

        public IServer LocalhostServer { get; set; }

        public void Handle(Exception err)
        {
            _handler.Handle(err);
        }

        public bool ShowPopup(IPopupMessage msg)
        {
            var res = PopupController.Show(msg);
            return res == MessageBoxResult.OK || res == MessageBoxResult.Yes;
        }

        public void RemoveServiceFromExplorer(IExplorerItemViewModel explorerItemViewModel)
        {
            var explorervm = _unityContainer.Resolve<IExplorerViewModel>();
            explorervm.RemoveItem(explorerItemViewModel);
        }

        public event Action ActiveServerChanged;
        public event Action ActiveItemChanged;




        public bool MenuExpanded
        {
            get
            {
                return _menuExpanded;
            }
            set
            {
                _menuExpanded = value;
                OnPropertyChanged(() => MenuExpanded);
            }
        }
    }
}