﻿using System.Linq;
using System.Windows;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Deploy;
using Dev2.Common.Interfaces.PopupController;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;
using Warewolf.AcceptanceTesting.Core;

namespace Warewolf.AcceptanceTesting.Deploy
{
    [Binding]
    public class DeployTabSteps
    {
        [BeforeFeature("Deploy")]
        public static void SetupForFeature()
        {
//            var bootStrapper = new UnityBootstrapperForDatabaseSourceConnectorTesting();
//            bootStrapper.Run();
            var view = new Mock<IDeployViewControl>();
            var viewModel = new Mock<IDeployViewModel>();
            view.Object.DataContext = viewModel;
            Utils.ShowTheViewForTesting(view.Object);
            FeatureContext.Current.Add(Utils.ViewNameKey, view.Object);
            FeatureContext.Current.Add(Utils.ViewModelNameKey, viewModel.Object);
        }

        [BeforeScenario("Deploy")]
        public void SetupForScenerio()
        {
            ScenarioContext.Current.Add(Utils.ViewNameKey, FeatureContext.Current.Get<IDeployViewControl>(Utils.ViewNameKey));
            ScenarioContext.Current.Add(Utils.ViewModelNameKey, FeatureContext.Current.Get<IDeployViewModel>(Utils.ViewModelNameKey));
        }

        [Given(@"I have deploy tab opened")]
        public void GivenIHaveDeployTabOpened()
        {
            var view = Utils.GetView<IDeployViewControl>();
            Assert.IsNotNull(view);
        }

        [Given(@"selected Source Server is ""(.*)""")]
        [When(@"selected Source Server is ""(.*)""")]
        public void GivenSelectedSourceServerIs(string sourceServerName)
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.SelectSourceServer(sourceServerName);
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(viewModel.SelectedSourceModel.Server.ResourceName,sourceServerName);
        }


        [Given(@"selected Destination Server is ""(.*)""")]
        [When(@"selected Destination Server is ""(.*)""")]
        public void GivenSelectedDestinationServerIs(string destinationServer)
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.SelectDestinationServer(destinationServer);
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(viewModel.SelectedDestinationModel.Server.ResourceName, destinationServer);
        }

        [Given(@"I select ""(.*)"" from Source Server")]
        [When(@"I select ""(.*)"" from Source Server")]
        public void GivenISelectFromSourceServer(string resourceName)
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.SelectSourceResource(resourceName);
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var selectedItems = viewModel.SelectedSourceModel.SelectedItems;
            Assert.AreEqual(1,selectedItems.Count());
        }

        [Given(@"I deploy")]
        [When(@"I deploy")]
        public void GivenIDeploy()
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.Deploy();
        }

        [When(@"I click OK on Resource exists in the destination server popup")]
        public void WhenIClickOKOnPopup()
        {
            var mockPopupController = ScenarioContext.Current.Get<Mock<IPopupController>>();
            mockPopupController.Setup(controller => controller.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.OK);
        }

        [When(@"I click Cancel on Resource exists in the destination server popup")]
        public void WhenIClickCancelOnPopup()
        {
            var mockPopupController = ScenarioContext.Current.Get<Mock<IPopupController>>();
            mockPopupController.Setup(controller => controller.Show(It.IsAny<IPopupMessage>())).Returns(MessageBoxResult.OK);
        }
        
        [When(@"I Select All Dependecies")]
        public void WhenISelectAllDependecies()
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.SelectAllDependencies();
        }

        [When(@"I type ""(.*)"" in Destination Server filter")]
        public void WhenITypeInDestinationServerFilter(string filterTerm)
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.EnterDestinationFilter(filterTerm);
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(filterTerm,viewModel.Destination.SearchText);
        }

        [When(@"I clear filter on Destination Server")]
        public void WhenIClearFilterOnDestinationServer()
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.ClearDestinationFilter();
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(string.Empty, viewModel.Destination.SearchText);
        }

        [When(@"I type ""(.*)"" in Source Server filter")]
        public void WhenITypeInSourceServerFilter(string filterTerm)
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.EnterSourceFilter(filterTerm);
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(filterTerm, viewModel.Source.SearchText);
        }

        [When(@"I clear filter on Source Server")]
        public void WhenIClearFilterOnSourceServer()
        {
            var view = Utils.GetView<IDeployViewControl>();
            view.ClearSourceFilter();
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            Assert.AreEqual(string.Empty, viewModel.Source.SearchText);
        }

        [Then(@"the validation message as ""(.*)""")]
        public void ThenTheValidationMessageAs(string validationMessage)
        {
            var view = Utils.GetView<IDeployViewControl>();
            string currentValidationMessage = view.GetCurrentValidationMessage();
            Assert.AreEqual(validationMessage,currentValidationMessage);
        }

        [Then(@"deploy is successfull")]
        public void ThenDeployIsSuccessfull()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"""(.*)"" is visible on Destination Server")]
        public void ThenIsVisibleOnDestinationServer(string resourceName)
        {
            //Is this going to be mocked?
            ScenarioContext.Current.Pending();
        }

        [Then(@"Resource exists in the destination server popup is shown")]
        public void ThenPopupIsShown(Table table)
        {
            //Get Pop?
            ScenarioContext.Current.Pending();
        }

        [Then(@"deploy is not successfull")]
        public void ThenDeployIsNotSuccessfull()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"""(.*)"" from Source Server is ""(.*)""")]
        public void ThenFromSourceServerIs(string resourceName, string state)
        {
            var view = Utils.GetView<IDeployViewControl>();
            if (state.ToLowerInvariant() == "visible")
            {
                bool isVisible = view.IsSourceResourceIsVisible(resourceName);
                Assert.IsTrue(isVisible);
            }
            else if(state.ToLowerInvariant() == "selected")
            {
                bool isSelected = view.IsSourceResourceSelected(resourceName);
                Assert.IsTrue(isSelected);
            }
        }

        [Then(@"""(.*)"" from Destination Server is ""(.*)""")]
        public void ThenFromDestinationServerIs(string resourceName, string state)
        {
            var view = Utils.GetView<IDeployViewControl>();
            if (state.ToLowerInvariant() == "visible")
            {
                bool isVisible = view.IsDestinationResourceIsVisible(resourceName);
                Assert.IsTrue(isVisible);
            }
            else if (state.ToLowerInvariant() == "selected")
            {
                bool isSelected = view.IsDestinationResourceSelected(resourceName);
                Assert.IsTrue(isSelected);
            }
        }

        [Then(@"I select ""(.*)"" from Source Server")]
        public void ThenISelectFromSourceServer(string resourceName)
        {
            var view = Utils.GetView<IDeployViewControl>();
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            view.SelectSourceServerResource(resourceName);
            Assert.AreEqual(resourceName,viewModel.SelectedSourceModel.SelectedItems);
        }


        [Then(@"Data Connectors is ""(.*)""")]
        public void ThenDataConnectorsIs(int numberOfDataConnectors)
        {
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var deployStatsTos = viewModel.Stats.FirstOrDefault(to => to.Name == "Data Connector");         
            Assert.IsNotNull(deployStatsTos);
            Assert.AreEqual(numberOfDataConnectors,deployStatsTos.Description);
        }

        [Then(@"Services is ""(.*)""")]
        public void ThenServicesIs(int numberOfServices)
        {
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var deployStatsTos = viewModel.Stats.FirstOrDefault(to => to.Name == "Services");
            Assert.IsNotNull(deployStatsTos);
            Assert.AreEqual(numberOfServices, deployStatsTos.Description);
        }

        [Then(@"Sources is ""(.*)""")]
        public void ThenSourcesIs(int numberOfSources)
        {
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var deployStatsTos = viewModel.Stats.FirstOrDefault(to => to.Name == "Sources");
            Assert.IsNotNull(deployStatsTos);
            Assert.AreEqual(numberOfSources, deployStatsTos.Description);
        }

        [Then(@"New Resource is ""(.*)""")]
        public void ThenNewResourceIs(int numberOfNewResources)
        {
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var deployStatsTos = viewModel.Stats.FirstOrDefault(to => to.Name == "New Resource");
            Assert.IsNotNull(deployStatsTos);
            Assert.AreEqual(numberOfNewResources, deployStatsTos.Description);
        }

        [Given(@"Override is ""(.*)""")]
        [When(@"Override is ""(.*)""")]
        [Then(@"Override is ""(.*)""")]
        public void ThenOverrideIs(int numberOfOverridingResources)
        {
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            var deployStatsTos = viewModel.Stats.FirstOrDefault(to => to.Name == "Override");
            Assert.IsNotNull(deployStatsTos);
            Assert.AreEqual(numberOfOverridingResources, deployStatsTos.Description);
        }

        [When(@"I Unselect ""(.*)"" from Source Server")]
        public void WhenIUnselectFromSourceServer(string resourceName)
        {
            var view = Utils.GetView<IDeployViewControl>();
            var viewModel = Utils.GetViewModel<IDeployViewModel>();
            view.DeselectSourceServerResource(resourceName);
            Assert.AreNotEqual(resourceName, viewModel.SelectedSourceModel.SelectedItems);
        }

        [Then(@"""(.*)"" popup is shown")]
        public void ThenPopupIsShown(string p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
