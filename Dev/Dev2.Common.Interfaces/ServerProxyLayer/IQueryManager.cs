﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.DB;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.Infrastructure.SharedModels;
using Dev2.Common.Interfaces.Toolbox;

namespace Dev2.Common.Interfaces.ServerProxyLayer
{
    /// <summary>
    /// Common interface for server queries
    /// </summary>
    public interface IQueryManager
    {
        /// <summary>
        /// Gets the dependencies of a resource. a dependency referes to a nested resource
        /// </summary>
        /// <param name="resourceId">the resource</param>
        /// <returns>a list of tree dependencies</returns>
        IList<IResource> FetchDependencies(Guid resourceId);
        /// <summary>
        /// Get the list of items that use this resource a nested resource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        IList<IResource> FetchDependants(Guid resourceId);

        /// <summary>
        /// Fetch a heavy weight reource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        StringBuilder FetchResourceXaml(Guid resourceId);
        /// <summary>
        /// Get a list of tables froma db source
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        IList<IDbTable> FetchTables(Guid sourceId);

        /// <summary>
        /// Fetch the resource as per the resource catalogue, without any notion of XML
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        IResourceDefinition FetchResource(Guid resourceId);

        /// <summary>
        /// Fetch the resource including the xaml
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        IXamlResource FetchResourceWithXaml(Guid resourceId);

        /// <summary>
        /// Loads the Tree.
        /// </summary>
        /// <returns></returns>
        Task<IExplorerItem> Load();

        IList<IToolDescriptor> FetchTools();


        IList<string> GetComputerNames();

        IList<IDbSource> FetchDbSources();

        IList<IDbAction> FetchDbActions(IDbSource source);

        IEnumerable<IWebServiceSource> FetchWebServiceSources();

        ObservableCollection<IWebServiceSource> WebSources { get; set; }



        IList<IPluginSource> FetchPluginSources();

        IList<IPluginAction> PluginActions(IPluginSource source, INamespaceItem ns);
        List<IDllListing> GetDllListings(IDllListing listing);

        ICollection<INamespaceItem> FetchNamespaces(IPluginSource source);
    }
}