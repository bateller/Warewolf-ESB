﻿using System;
using System.Windows.Input;

namespace Dev2.Common.Interfaces.Studio.ViewModels
{
    public interface IExplorerItemViewModel : IExplorerTreeItem
    {
        
        bool Checked { get; set; }
        Guid ResourceId { get; set; }
        ICommand OpenCommand { get; set; }
        bool IsRenaming{ get; set; }
        bool IsNotRenaming { get;  }
        ICommand ItemSelectedCommand { get; set; }
        ICommand LostFocus { get; set; }
        bool IsVisible { get; set; }
        bool AllowEditing { get; set; }
        bool CanExecute { get; set; }
        bool CanEdit { get; set; }
        bool CanView { get; set; }
        bool CanShowDependencies { get; set; }

        bool IsVersion { get; set; }
        string VersionNumber { get; set; }
        string VersionHeader { get; set; }
        void Filter(string filter);
        bool Move(IExplorerItemViewModel destination);
        bool CanDrop { get; set; }
        bool CanDrag { get; set; }
        ICommand OpenVersionCommand { get; set; }
        ICommand DeleteVersionCommand { get; set; }
        ICommand ShowDependenciesCommand { get; set; }
        
        string Inputs { get; set; }
        string Outputs { get; set; }
        string ExecuteToolTip { get; }
        string EditToolTip { get; }
        void AddSibling(IExplorerItemViewModel sibling);
        void CreateNewFolder();
        void Apply(Action<IExplorerItemViewModel> action);
        IExplorerItemViewModel Find(string resourcePath);

    }

    public enum ExplorerEventContext
    {
        Selected
    }
}