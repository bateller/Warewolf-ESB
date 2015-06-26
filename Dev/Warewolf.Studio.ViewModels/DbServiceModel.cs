﻿using System;
using System.Collections.Generic;
using System.Data;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.DB;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.ServerProxyLayer;
using Warewolf.Core;

namespace Warewolf.Studio.ViewModels
{
    public class DbServiceModel : IDbServiceModel
    {
        readonly IStudioUpdateManager _updateRepository;
        readonly IQueryManager _queryProxy;
        readonly IShellViewModel _shell;
        readonly string _serverName;

        #region Implementation of IDbServiceModel

        public DbServiceModel(IStudioUpdateManager updateRepository, IQueryManager queryProxy,IShellViewModel shell, string serverName)
        {
            _updateRepository = updateRepository;
            _queryProxy = queryProxy;
            _shell = shell;
            _serverName = serverName;

        }

        public ICollection<IDbSource> RetrieveSources()
        {

           return _queryProxy.FetchDbSources();

        }

        public ICollection<IDbAction> GetActions(IDbSource source)
        {
            return _queryProxy.FetchDbActions(source);
        }

        public void CreateNewSource()
        {
            _shell.NewResource(ResourceType.DbSource, Guid.NewGuid());
        }

        public void EditSource(IDbSource selectedSource)
        {
            _shell.EditResource(selectedSource);

        }

        public  DataTable TestService(IDatabaseService inputValues)
        {
           return _updateRepository.TestDbService(inputValues);          
        }

        public IEnumerable<IServiceOutputMapping> GetDbOutputMappings(IDbAction action)
        {
            return new List<IServiceOutputMapping> { new ServiceOutputMapping("bob", "The"), new ServiceOutputMapping("dora", "The"), new ServiceOutputMapping("Tree", "The") }; 
        }

        public void SaveService(IDatabaseService toModel)
        {
      
            _updateRepository.Save(toModel);
        }

        #endregion
    }
}