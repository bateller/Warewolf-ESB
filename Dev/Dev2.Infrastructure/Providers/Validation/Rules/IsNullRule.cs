
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
using Dev2.Common.Interfaces.Infrastructure.Providers.Errors;

namespace Dev2.Providers.Validation.Rules
{
    public class IsNullRule : Rule<object>
    {
        public IsNullRule(Func<object> getValue)
            : base(getValue)
        {
            ErrorText = "cannot be null";
        }

        public override IActionableErrorInfo Check()
        {
            var value = GetValue();
            return value == null ? CreatError() : null;
        }
    }
}
