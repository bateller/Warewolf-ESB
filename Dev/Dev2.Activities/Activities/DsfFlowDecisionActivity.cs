
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2014 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/


using System.Collections.Generic;
using System.ComponentModel;
using Dev2.Activities;
using Dev2.Data.SystemTemplates.Models;
using Dev2.Enums;

// ReSharper disable CheckNamespace


namespace Unlimited.Applications.BusinessDesignStudio.Activities
// ReSharper restore CheckNamespace
{
    public class DsfFlowDecisionActivity : DsfFlowNodeActivity<bool>
    {
        #region Ctor

        public DsfFlowDecisionActivity()
            : base("Decision")
        {
            DecisionStack = new List<Dev2Decision>();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<Dev2Decision> DecisionStack { get; set; }
        #endregion

        public override enFindMissingType GetFindMissingType()
        {
            return enFindMissingType.MixedActivity;
        }

        public override void UpdateForEachInputs(IList<System.Tuple<string, string>> updates, System.Activities.NativeActivityContext context)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateForEachOutputs(IList<System.Tuple<string, string>> updates, System.Activities.NativeActivityContext context)
        {
            throw new System.NotImplementedException();
        }

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            throw new System.NotImplementedException();
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            throw new System.NotImplementedException();
        }
    }



}
