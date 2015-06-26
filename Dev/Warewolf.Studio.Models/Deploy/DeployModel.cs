﻿using System;
using System.Collections.Generic;
using Dev2;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Deploy;
using Dev2.Common.Interfaces.Security;
using Dev2.Common.Interfaces.ServerProxyLayer;

namespace Warewolf.Studio.Models.Deploy
{
    public class DeployModel : IDeployModel
    {
        readonly IUpdateManager _updateManager;
        readonly IQueryManager _queryManager;
        //todo: change to environment
        public DeployModel(IServer server,IUpdateManager updateManager,IQueryManager queryManager)
        {
            VerifyArgument.AreNotNull(new Dictionary<string, object> { { "server", server }, { "updateManager", updateManager }, { "queryManager", QueryManager } });
            _updateManager = updateManager;
            _queryManager = queryManager;
             Server = server;
        }

        #region Implementation of IDeployModel

        public IServer Server { get; private set; }
        public object[] SelectedItems { get; set; }
        public IUpdateManager UpdateManager
        {
            get
            {
                return _updateManager;
            }
        }
        public IQueryManager QueryManager
        {
            get
            {
                return _queryManager;
            }
        }

        public void Deploy(IResource resource)
        {
            VerifyArgument.IsNotNull("resource", resource);
            if(CanDeploy(resource))
            _updateManager.DeployItem(resource);
            else
            {
                throw new WarewolfPermissionsException("The user does not have the right to deploy to this resource");
            }
        }

        public IList<IResource> GetDependancies(Guid id)
        {
            VerifyArgument.IsNotNull("resource", id);
            return _queryManager.FetchDependencies(id);
        }

        public bool CanDeploy(IResource resource)
        {
            return resource.UserPermissions.HasFlag(Permissions.DeployFrom); 
        }

        #endregion
    }
}