﻿using System;
using System.Windows.Media;
using Dev2.Common.Interfaces.Toolbox;

namespace Warewolf.Core
{
    public class ToolDescriptorInfo : Attribute
    {
        public ToolDescriptorInfo(string iconName, string name, ToolType toolType, Guid id, string assemblyname, string version ,string path, string category)
        {
            Category = category;
            Id = id;
            Designer = new WarewolfType(assemblyname,Version.Parse(version),path);
            ToolType = toolType;
            Name = name;
            Icon = GetResourceFromString(iconName);
        }

        DrawingImage GetResourceFromString(string iconName)
        {
            return null;
        }

        public DrawingImage Icon { get; private set; }

        public string Name { get; private set; }

        public ToolType ToolType { get; private set; }

        public Guid Id { get; private set; }

        public IWarewolfType Designer { get; private set; }
        public string Category { get; private set; }
    }
}