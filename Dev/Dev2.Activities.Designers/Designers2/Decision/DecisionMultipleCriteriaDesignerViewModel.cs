
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
using System.Collections.ObjectModel;
using System.Linq;
using Dev2.Activities.Designers2.Core;
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;
using Dev2.Common.Interfaces.Infrastructure.Providers.Validation;
using Dev2.Data.Decisions.Operations;
using Dev2.Data.SystemTemplates.Models;
using Dev2.Providers.Validation.Rules;
using Dev2.Runtime.Configuration.ViewModels.Base;
using Dev2.Studio.Core;
using Dev2.Studio.Core.Activities.Utils;

// ReSharper disable CheckNamespace
namespace Dev2.Activities.Designers2.DecisionMultipleCriteria
{
    public class DecisionMultipleCriteriaDesignerViewModel : ActivityCollectionDesignerViewModel<Dev2Decision>
    {
        public Func<string> GetDatalistString = () => DataListSingleton.ActiveDataList.Resource.DataList;
        readonly IList<string> _requiresSearchCriteria = new List<string> { "Doesn't Contain", "Contains", "=", "<> (Not Equal)", "Ends With", "Doesn't Start With", "Doesn't End With", "Starts With", "Is Regex", "Not Regex", ">", "<", "<=", ">=" };
        public DecisionMultipleCriteriaDesignerViewModel(ModelItem modelItem)
            : base(modelItem)
        {
            AddTitleBarLargeToggle();
            AddTitleBarHelpToggle();


            dynamic mi = ModelItem;
            InitializeItems(mi.DecisionStack);
            WhereOptions = new ObservableCollection<string>(GetDescriptions());
            SearchTypeUpdatedCommand = new DelegateCommand(OnSearchTypeChanged);
        }

        private static List<string> GetDescriptions()
        {
            var type = typeof(enDecisionType);
            var names = Enum.GetNames(type);
            return (from name in names 
                    select type.GetField(name) into field 
                    from DecisionTypeDisplayValue fd in field.GetCustomAttributes(typeof(DecisionTypeDisplayValue), true) 
                    select fd.DisplayValue).ToList();
        }

        public DelegateCommand SearchTypeUpdatedCommand { get; set; }

        public ObservableCollection<string> WhereOptions { get; set; }

        void OnSearchTypeChanged(object indexObj)
        {
            var index = (int)indexObj;

            if (index == -1)
            {
                index = 0;
            }

            if (index < 0 || index >= ItemCount)
            {
                return;
            }

            var mi = ModelItemCollection[index];

            var searchType = mi.GetProperty("SearchType") as string;

            if (searchType == "Is Between" || searchType == "Not Between")
            {
                mi.SetProperty("IsSearchCriteriaVisible", false);
            }
            else
            {
                mi.SetProperty("IsSearchCriteriaVisible", true);
            }

            var requiresCriteria = _requiresSearchCriteria.Contains(searchType);
            mi.SetProperty("IsSearchCriteriaEnabled", requiresCriteria);
            if (!requiresCriteria)
            {
                mi.SetProperty("SearchCriteria", string.Empty);
            }
        }

        public override string CollectionName { get { return "DecisionStack"; } }
        public object CancelCommand
        {
            get
            {
                return null;
            }
        }
        public object OkCommand
        {
            get
            {
                return null;
            }
        }

        protected override IEnumerable<IActionableErrorInfo> ValidateThis()
        {
            return Errors;
        }

        protected override IEnumerable<IActionableErrorInfo> ValidateCollectionItem(ModelItem mi)
        {
            return null;
        }

        public IRuleSet GetRuleSet(string propertyName)
        {
            var ruleSet = new RuleSet();

            
            return ruleSet;
        }
    }
}
