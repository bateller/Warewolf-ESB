
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2014 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dev2.Activities.Designers2.DecisionMultipleCriteria;
using Dev2.Data.SystemTemplates.Models;
using Dev2.Studio.Core.Activities.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unlimited.Applications.BusinessDesignStudio.Activities;

namespace Dev2.Activities.Designers.Tests.DecisionMultipleCriteria
{
    // ReSharper disable InconsistentNaming
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DecisionMultipleCriteriaTests
    {
        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("DecisionMultipleCriteriaDesignerViewModel_Constructor")]
        public void DecisionMultipleCriteriaDesignerViewModel_Constructor_ModelItem_ShouldSetupProperties()
        {
            //------------Setup for test--------------------------
            var modelItem = ModelItemUtils.CreateModelItem(new DsfFlowDecisionActivity());
            
            
            //------------Execute Test---------------------------
            var decisionMultipleCriteriaDesignerViewModel = new DecisionMultipleCriteriaDesignerViewModel(modelItem);
            //------------Assert Results-------------------------
            Assert.IsNotNull(decisionMultipleCriteriaDesignerViewModel);
            Assert.IsNotNull(decisionMultipleCriteriaDesignerViewModel.ModelItemCollection);
            Assert.AreEqual(2,decisionMultipleCriteriaDesignerViewModel.ModelItemCollection.Count);
        }

        [TestMethod]
        [Owner("Hagashen Naidu")]
        [TestCategory("DecisionMultipleCriteriaDesignerViewModel_Constructor")]
        public void DecisionMultipleCriteriaDesignerViewModel_Constructor_WhenHasItems_ShouldNotCreatenewItems()
        {
            //------------Setup for test--------------------------
            var activity = new DsfFlowDecisionActivity();
            activity.DecisionStack.Add(new Dev2Decision("[[a]]","1","",1,true));
            activity.DecisionStack.Add(new Dev2Decision("[[b]]","1","2",2,true));
            var modelItem = ModelItemUtils.CreateModelItem(activity);
            
            
            //------------Execute Test---------------------------
            var decisionMultipleCriteriaDesignerViewModel = new DecisionMultipleCriteriaDesignerViewModel(modelItem);
            //------------Assert Results-------------------------
            Assert.IsNotNull(decisionMultipleCriteriaDesignerViewModel);
            var collection = decisionMultipleCriteriaDesignerViewModel.ModelItemCollection;
            Assert.IsNotNull(collection);
            Assert.AreEqual(3, collection.Count);
            Assert.AreEqual("[[a]]",collection[0].GetProperty("Col1"));
            Assert.AreEqual("[[b]]",collection[1].GetProperty("Col1"));
        }

        static void SetDataListString(DecisionMultipleCriteriaDesignerViewModel viewModel)
        {
            viewModel.GetDatalistString = () =>
            {
                const string trueString = "True";
                const string noneString = "None";
                var datalist = string.Format("<DataList><var Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><a Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><b Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><h Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><r Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /><rec Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" ><set Description=\"\" IsEditable=\"{0}\" ColumnIODirection=\"{1}\" /></rec></DataList>", trueString, noneString);
                return datalist;
            };
        }

        void Verify_IsFocused(ModelItem modelItem, Action doError, string isFocusedPropertyName)
        {
            Assert.IsFalse(modelItem.GetProperty<bool>(isFocusedPropertyName));
            doError.Invoke();
            Assert.IsTrue(modelItem.GetProperty<bool>(isFocusedPropertyName));
        }


        static ModelItem CreateModelItem(IEnumerable<Dev2Decision> items, string displayName = "Decision")
        {
            var modelItem = ModelItemUtils.CreateModelItem(new DsfFlowDecisionActivity());
            modelItem.SetProperty("DisplayName", displayName);

            // ReSharper disable PossibleNullReferenceException
            var modelItemCollection = modelItem.Properties["DecisionStack"].Collection;
            foreach(var dto in items)
            {
                modelItemCollection.Add(dto);
            }
            // ReSharper restore PossibleNullReferenceException

            return modelItem;
        }
    }
}
