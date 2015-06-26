﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Dev2.Common.Interfaces;
//using Dev2.Common.Interfaces.Help;
//using Dev2.Common.Interfaces.Toolbox;
//using Moq;
//using Warewolf.Core;

//namespace Dev2.ToolBox
//{
//    public class ServerToolRepository : IToolManager
//    {
//        List<IToolDescriptor> _tools;
//        readonly  static Dictionary<Type, IToolDescriptor> LegacyTools = new Dictionary<Type, IToolDescriptor>(); 

//        public ServerToolRepository(IList<string> nativeToolPaths, IList<string> searchFolders)
//        {
//            _tools = new List<IToolDescriptor>();
//            _tools.AddRange(nativeToolPaths.SelectMany(CreateTools));
//        }

//        private IEnumerable<IToolDescriptor> CreateTools(string path)
//        {
//            var assembly = Assembly.LoadFile(path);
//            Type basetype = typeof(IDev2Activity);
//            return assembly.ExportedTypes.Where(basetype.IsAssignableFrom).Select(CreateTool);
//        }

//        IToolDescriptor CreateTool(Type arg)
//        {

//            if (arg.GetCustomAttributes().Any(a => a is ToolDescriptorInfo))
//                return GetDescriptorFromAttribute(arg);
//            return GetDescriptorLegacy(arg);
//        }

//        IToolDescriptor GetDescriptorFromAttribute(Type type)
//        {
//            var info =type.GetCustomAttributes(typeof(ToolDescriptorInfo)).First() as ToolDescriptorInfo;
//            // ReSharper disable once PossibleNullReferenceException
//            return new ToolDescriptor(info.Id,info.Designer,new WarewolfType(type.FullName,type.Assembly.GetName().Version,type.Assembly.Location),info.Name,info.Icon,type.Assembly.GetName().Version,new Mock<IHelpDescriptor>().Object,true,info.Category,ToolType.Native);
//        }

//        IToolDescriptor GetDescriptorLegacy(Type type)
//        {
//            if(LegacyTools.ContainsKey(type))
//            {
//                return LegacyTools[type];
//            }
//            else
//            {
//                return null;
//            }
//        }
//        #region Implementation of IToolManager

//        public IList<IToolDescriptor> LoadTools()
//        {
//            return _tools;
//        }

//        #endregion
//    }
//}
