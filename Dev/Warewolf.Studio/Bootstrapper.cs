﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading;
using System.Windows;
using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.DataList.DatalistView;
using Dev2.Common.Interfaces.ErrorHandling;
using Dev2.Common.Interfaces.Help;
using Dev2.Common.Interfaces.PopupController;
using Dev2.Common.Interfaces.Studio;
using Dev2.Common.Interfaces.Studio.ViewModels;
using Dev2.Util;
using Infragistics.Windows.DockManager;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Moq;
using Warewolf.Core;
using Warewolf.Studio.AntiCorruptionLayer;
using Warewolf.Studio.Core;
using Warewolf.Studio.Core.Infragistics_Prism_Region_Adapter;
using Warewolf.Studio.Core.Popup;
using Warewolf.Studio.Models.Help;
using Warewolf.Studio.ViewModels;
using Warewolf.Studio.ViewModels.Help;
using Warewolf.Studio.ViewModels.VariableList;
using Warewolf.Studio.Views;
using Warewolf.Studio.Views.Variable_List;
using IVariableListViewModel = Dev2.Common.Interfaces.DataList.DatalistView.IVariableListViewModel;

namespace Warewolf.Studio
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        #region Overrides of UnityBootstrapper

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer"/>. May be overwritten in a derived class to add specific
        ///             type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            AppSettings.LocalHost = ConfigurationManager.AppSettings["LocalHostServer"];
            

            Container.RegisterInstance<IVersionChecker>(new VersionChecker());
            Container.RegisterType<IWebLatestVersionDialog, WebLatestVersionDialog>();
            Container.RegisterInstance<IRequestServiceNameView>(new RequestServiceNameView());
            Container.RegisterType<IServer, Server>(new InjectionConstructor(typeof(Uri)));
            Container.RegisterInstance<IVersionChecker>(new VersionChecker());
            Container.RegisterInstance<IShellViewModel>(new ShellViewModel(Container, Container.Resolve<IRegionManager>(), Container.Resolve<IEventAggregator>()));
            Container.RegisterInstance<IExplorerViewModel>(new ExplorerViewModel(Container.Resolve<IShellViewModel>(), Container.Resolve<IEventAggregator>()));
            Container.RegisterInstance<IMenuViewModel>(new MenuViewModel(Container.Resolve<IShellViewModel>()));
      
            Container.RegisterInstance<IHelpWindowModel>(new HelpModel(Container.Resolve<IEventAggregator>()));
            Container.RegisterInstance<IHelpWindowViewModel>(new HelpWindowViewModel(new HelpDescriptorViewModel(new HelpDescriptor("","<body>This is the default help</body>",null)) , Container.Resolve<IHelpWindowModel>()));

            Container.RegisterInstance<IExplorerView>(new ExplorerView());
            Container.RegisterInstance<IToolboxView>(new ToolboxView());
            Container.RegisterInstance<IMenuView>(new MenuView());
            Container.RegisterInstance<IExceptionHandler>(new WarewolfExceptionHandler(new Dictionary<Type, Action>()));
            Container.RegisterInstance<ISplashView>(new SplashPage());
            Container.RegisterInstance<IExternalProcessExecutor>(new ExternalProcessExecutor());
            Container.RegisterType<ISplashViewModel,SplashViewModel>();
            Container.RegisterInstance<IHelpView>(new HelpView());
            Container.RegisterInstance<IPasteView>(new ManageWebservicePasteView());
     
            ICollection<IVariableListViewColumnViewModel> colls = new ObservableCollection<IVariableListViewColumnViewModel>();
            colls.Add(new VariableListColumnViewModel("col", "bob", new Mock<IVariableListViewModel>().Object, colls) { Input = true });

            var convertedRecset = new VariableListViewRecordSetViewModel("bob", colls, new Mock<IVariableListViewModel>().Object, new List<IVariablelistViewRecordSetViewModel>());
            var convertedRecset2 = new VariableListViewRecordSetViewModel("dave", new VariableListColumnViewModel[0], new Mock<IVariableListViewModel>().Object, new List<IVariablelistViewRecordSetViewModel>());
            var convertedScalar = new VariableListItemViewScalarViewModel("martha", new Mock<IVariableListViewModel>().Object, new List<IVariableListViewScalarViewModel>());
            var convertedScalar2 = new VariableListItemViewScalarViewModel("stewart", new Mock<IVariableListViewModel>().Object, new List<IVariableListViewScalarViewModel>()) { Used = false };
            var expressions = new List<IDataExpression> { new Mock<IDataExpression>().Object, new Mock<IDataExpression>().Object, new Mock<IDataExpression>().Object, new Mock<IDataExpression>().Object };
            var convertor = new Mock<IDatalistViewExpressionConvertor>();
            convertor.Setup(a => a.Create(expressions[0])).Returns(convertedRecset);
            convertor.Setup(a => a.Create(expressions[1])).Returns(convertedRecset2);
            convertor.Setup(a => a.Create(expressions[2])).Returns(convertedScalar);
            convertor.Setup(a => a.Create(expressions[3])).Returns(convertedScalar2);
            IVariableListViewModel vm = new VariableListViewModel(expressions, convertor.Object);
            Container.RegisterInstance<IVariableListView>(new VariableListView());
            Container.RegisterInstance(vm);
            Container.RegisterInstance<IPopupController>(new PopupController(new PopupMessageBoxFactory(),new PopupView()));
            
        }
        
        #endregion
        public static ISplashView _splashView;

        private ManualResetEvent ResetSplashCreated;
        private Thread SplashThread;
        protected override void InitializeShell()
        {
            ResetSplashCreated = new ManualResetEvent(false);
            
            SplashThread = new Thread(ShowSplash);
            SplashThread.SetApartmentState(ApartmentState.STA);
            SplashThread.IsBackground = true;
            SplashThread.Name = "Splash Screen";
            SplashThread.Start();
            ResetSplashCreated.WaitOne();
            base.InitializeShell();
            var window = (Window)Shell;
            window.Show();
            if (window.IsVisible)
            {
                _splashView.CloseSplash();
            }            

        }


        private void ShowSplash()
        {
            // Create the window 
            
            var shellViewModel = Container.Resolve<IShellViewModel>();
            var splashViewModel = Container.Resolve<ISplashViewModel>(new ParameterOverrides { { "server", shellViewModel.LocalhostServer }, { "externalProcessExecutor", Container.Resolve<IExternalProcessExecutor>() } });

            SplashPage splashPage = new SplashPage();
            splashPage.DataContext = splashViewModel;
            _splashView = splashPage;
            // Show it 
            splashPage.Show(false);
            // Now that the window is created, allow the rest of the startup to run 
            if(ResetSplashCreated != null)
            {
                ResetSplashCreated.Set();
            }
            System.Windows.Threading.Dispatcher.Run();
        }
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(TabGroupPane), Container.Resolve<TabGroupPaneRegionAdapter>());
            return mappings;
        }

    }
}
