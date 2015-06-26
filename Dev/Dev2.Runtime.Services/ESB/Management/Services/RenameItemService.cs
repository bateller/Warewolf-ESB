
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Collections.Generic;
using System.Text;
using Dev2.Common;
using Dev2.Common.Interfaces.Core.DynamicServices;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Hosting;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Communication;
using Dev2.DynamicServices;
using Dev2.DynamicServices.Objects;
using Dev2.Explorer;
using Dev2.Runtime.Hosting;
using Dev2.Workspaces;

namespace Dev2.Runtime.ESB.Management.Services
{
    public class RenameItemService : IEsbManagementEndpoint
    {
        private IExplorerServerResourceRepository _serverExplorerRepository;

        public string HandlesType()
        {
            return "RenameItemService";
        }

        public StringBuilder Execute(Dictionary<string, StringBuilder> values, IWorkspace theWorkspace)
        {
            IExplorerRepositoryResult item;
            var serializer = new Dev2JsonSerializer();
            try
            {
                if(values == null)
                {
                    throw new ArgumentNullException("values");
                }
                StringBuilder itemToBeRenamed;
                StringBuilder sb;
                if(!values.TryGetValue("itemToRename", out itemToBeRenamed))
                {
                    throw new ArgumentException("itemToRename value not supplied.");
                }
                if(!values.TryGetValue("newName", out sb))
                {
                    throw new ArgumentException("newName value not supplied.");
                }
                var newName = sb.ToString();
                var itemToRename = Guid.Parse(itemToBeRenamed.ToString());
               
                var foundItem = ServerExplorerRepo.Find(itemToRename);
            
                if (foundItem == null)
                {
                    item = new ExplorerRepositoryResult(ExecStatus.Fail, "Item Not found");
                }
                else
                {
                    if (foundItem.ResourceType == ResourceType.Folder)
                    {
                        if(foundItem.ResourcePath != foundItem.DisplayName)
                       newName = ( foundItem.ResourcePath.Substring(0, foundItem.ResourcePath.LastIndexOf(foundItem.DisplayName, System.StringComparison.Ordinal)) + newName);
                    }

                    Dev2Logger.Log.Info(String.Format("Rename Item. Path:{0} NewPath:{1}", itemToBeRenamed, newName));
                    item = ServerExplorerRepo.RenameItem(foundItem, newName, GlobalConstants.ServerWorkspaceID);
                }
            }
            catch(Exception e)
            {
                Dev2Logger.Log.Error(e);
                item = new ExplorerRepositoryResult(ExecStatus.Fail, e.Message);
            }
            return serializer.SerializeToBuilder(item);
        }

        public DynamicService CreateServiceEntry()
        {
            var findServices = new DynamicService { Name = HandlesType(), DataListSpecification = new StringBuilder("<DataList><itemToRename ColumnIODirection=\"Input\"/><newName ColumnIODirection=\"Input\"/><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>") };

            var fetchItemsAction = new ServiceAction { Name = HandlesType(), ActionType = enActionType.InvokeManagementDynamicService, SourceMethod = HandlesType() };

            findServices.Actions.Add(fetchItemsAction);

            return findServices;
        }

        public IExplorerServerResourceRepository ServerExplorerRepo
        {
            get { return _serverExplorerRepository ?? ServerExplorerRepository.Instance; }
            set { _serverExplorerRepository = value; }
        }
    }
}
