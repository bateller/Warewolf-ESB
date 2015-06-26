using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.ServerProxyLayer;
using Dev2.Common.Interfaces.Toolbox;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Moq;
using Warewolf.AcceptanceTesting.Core;
using Warewolf.Studio.Models.Toolbox;
using Warewolf.Studio.ViewModels.ToolBox;
using Warewolf.Studio.Views;

namespace Warewolf.AcceptanceTesting.Toolbox
{
    internal class UnityBootstrapperForToolboxTesting : UnityBootstrapperForTesting
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance<IToolboxViewModel>(new ToolboxViewModel(new ToolboxModel(Container.Resolve<IServer>(), Container.Resolve<IServer>(), new Mock<IPluginProxy>().Object), new ToolboxModel(Container.Resolve<IServer>(), Container.Resolve<IServer>(), new Mock<IPluginProxy>().Object), new Mock<IEventAggregator>().Object));

            var toolboxView = new ToolboxView { DataContext = Container.Resolve<IToolboxViewModel>() };
            Container.RegisterInstance<IToolboxView>(toolboxView);
        }
    }
}