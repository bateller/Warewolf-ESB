using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Data;
using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.Infrastructure;
using Dev2.Common.Interfaces.ServerProxyLayer;
using Dev2.Common.Interfaces.Toolbox;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Services.Security;
using Moq;
using Newtonsoft.Json;
using Warewolf.Core;

namespace Warewolf.AcceptanceTesting.Core
{
    public class ServerForTesting : Resource, IServer
    {
        public ServerForTesting(Mock<IExplorerRepository> explorerRepository)
        {
            MockExplorerRepo = explorerRepository;
            _explorerProxy = explorerRepository.Object;
            ResourceName = "localhost";
        }

        private readonly IExplorerRepository _explorerProxy;

        public ServerForTesting(IResource copy) : base(copy)
        {
        }

        public ServerForTesting(XElement xml) : base(xml)
        {
        }

        public Task<bool> Connect()
        {
            return Task.FromResult(true);
        }

        public List<IResource> Load()
        {
            return CreateResources();
        }

        private List<IResource> CreateResources()
        {
            return new List<IResource>();
        }

        public Task<IExplorerItem> LoadExplorer()
        {
            return Task.FromResult(CreateExplorerItems());
        }

        private IExplorerItem CreateExplorerItems()
        {
            var mockExplorerItem = new Mock<IExplorerItem>();
            mockExplorerItem.Setup(item => item.DisplayName).Returns("Level 0");
            var children = new List<IExplorerItem>();
            children.AddRange(CreateFolders(new[] { "Folder 1", "Folder 2", "Folder 3", "Folder 4", "Folder 5" }));
            mockExplorerItem.Setup(item => item.Children).Returns(children);
            return mockExplorerItem.Object;
        }

        private IEnumerable<IExplorerItem> CreateFolders(IEnumerable<string> names)
        {
            var folders = new List<IExplorerItem>();
            foreach (var name in names)
            {
                var mockIExplorerItem = new Mock<IExplorerItem>();
                mockIExplorerItem.Setup(item => item.ResourceType).Returns(ResourceType.Folder);
                mockIExplorerItem.Setup(item => item.DisplayName).Returns(name);
                mockIExplorerItem.Setup(item => item.Children).Returns(new List<IExplorerItem>());
                folders.Add(mockIExplorerItem.Object);
            }
            CreateChildrenForFolder(folders[1], new[] { "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1", "Child 1" });
            return folders;
        }

        private void CreateChildrenForFolder(IExplorerItem explorerItem, IEnumerable<string> childNames)
        {
            int i = 1;
            var resourceType = ResourceType.EmailSource;
            foreach (var name in childNames)
            {
                if (i % 2 == 0)
                {
                    resourceType = ResourceType.WorkflowService;
                }
                if (i % 3 == 0)
                {
                    resourceType = ResourceType.DbService;
                }
                if (i % 4 == 0)
                {
                    resourceType = ResourceType.WebSource;
                }
                var mockIExplorerItem = new Mock<IExplorerItem>();
                mockIExplorerItem.Setup(item => item.ResourceType).Returns(resourceType);
                mockIExplorerItem.Setup(item => item.DisplayName).Returns(explorerItem.DisplayName + " " + name);
                mockIExplorerItem.Setup(item => item.ResourceId).Returns(Guid.NewGuid());
                explorerItem.Children.Add(mockIExplorerItem.Object);
                i++;
            }
        }

        public IList<IServer> GetServerConnections()
        {
            return null;
        }

        public IList<IToolDescriptor> LoadTools()
        {
            return new List<IToolDescriptor>
            {
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Decision","",new Version(),true,"Controlflow",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Data Merge","",new Version(),true,"Controlflow",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Data Split","",new Version(),true,"Controlflow",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Delete","",new Version(),true,"Controlflow",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Base Conversion","",new Version(),true,"Data",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Drop box","",new Version(),true,"Dropbox",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"SQL Bulk Insert","",new Version(),true,"Recordset",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Web Request","",new Version(),true,"Recordset",ToolType.Native, "" ),
                new ToolDescriptor(Guid.NewGuid(), new Mock<IWarewolfType>().Object,new Mock<IWarewolfType>().Object,"Format Number","",new Version(),true,"Utility",ToolType.Native, "" )
            };
        }

        public IExplorerRepository ExplorerRepository
        {
            get
            {
                if (_explorerProxy != null)
                {
                    return _explorerProxy;
                }
                return new Mock<IExplorerRepository>().Object;
            }
        }

        [JsonIgnore]
        public IQueryManager QueryProxy
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsConnected()
        {
            return true;
        }

        public void ReloadTools()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public List<IWindowsGroupPermission> Permissions
        {
            get
            {
                return new List<IWindowsGroupPermission>{new WindowsGroupPermission
                {
                    Administrator = true,
                    IsServer = true,
                    ResourceID = Guid.Empty
                
                }};
            }
        }
        
        public event PermissionsChanged PermissionsChanged;
        public event NetworkStateChanged NetworkStateChanged;
        public event ItemAddedEvent ItemAddedEvent;

        [JsonIgnore]
        public IStudioUpdateManager UpdateRepository
        {
            get { throw new NotImplementedException(); }
        }
        public Mock<IExplorerRepository> MockExplorerRepo { get; set; }

        public string GetServerVersion()
        {
            throw new NotImplementedException();
        }
    }
}