﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Runtime.ServiceModel;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Windows;
using Warewolf.Studio.ViewModels;

namespace Warewolf.Studio.Views
{
    /// <summary>
    /// Interaction logic for ManageServerControl.xaml
    /// </summary>
    public partial class ManageDatabaseSourceControl:IManageDatabaseSourceView,ICheckControlEnabledView
    {
        public ManageDatabaseSourceControl()
        {
            InitializeComponent();
        }

        public void EnterServerName(string serverName)
        {
            //ServerTextBox.EmptyText = serverName;
        }

        public Visibility GetDatabaseDropDownVisibility()
        {
            BindingExpression be = DatabaseComboxContainer.GetBindingExpression(VisibilityProperty);
            if (be != null)
            {
                be.UpdateTarget();
            }
            return DatabaseComboxContainer.Visibility;
        }

        public bool GetControlEnabled(string controlName)
        {
            switch (controlName)
            {
                case "Save":
                    return SaveButton.Command.CanExecute(null);
                case "Test Connection":
                    return TestConnectionButton.Command.CanExecute(null);
            }
            return false;
        }

        public void SetAuthenticationType(AuthenticationType authenticationType)
        {
            if (authenticationType == AuthenticationType.Windows)
            {
                WindowsRadioButton.IsChecked = true;
            }
            else
            {
                UserRadioButton.IsChecked = true;
            }
        }

        public void SelectDatabase(string databaseName)
        {
            DatabaseComboxBox.SelectedItem = databaseName;
        }

        public Visibility GetUsernameVisibility()
        {
            BindingExpression be = UserNamePasswordContainer.GetBindingExpression(VisibilityProperty);
            if (be != null)
            {
                be.UpdateTarget();
            }
            return UserNamePasswordContainer.Visibility;
        }

        public Visibility GetPasswordVisibility()
        {
            BindingExpression be = UserNamePasswordContainer.GetBindingExpression(VisibilityProperty);
            if (be != null)
            {
                be.UpdateTarget();
            }
            return UserNamePasswordContainer.Visibility;
        }

        public void PerformTestConnection()
        {
            TestConnectionButton.Command.Execute(null);
        }

        public void PerformSave()
        {
            SaveButton.Command.Execute(null);
        }

        public void EnterUserName(string userName)
        {
            UserNameTextBox.Text = userName;
        }

        public void EnterPassword(string password)
        {
            PasswordTextBox.Password = password;
        }

        public string GetErrorMessage()
        {
            return ErrorTextBlock.Text;
        }
        private void XamComboEditor_Loaded(object sender, RoutedEventArgs e)
        {
            SpecializedTextBox txt = Utilities.GetDescendantFromType(sender as DependencyObject, typeof(SpecializedTextBox), false) as SpecializedTextBox;
            if (txt != null)
            {
                txt.SelectAll();
                txt.Focus();
                var selectedItem = ServerTextBox.SelectedItem as ComputerName;
                if (selectedItem != null) txt.Text = selectedItem.Name;
                ServerTextBox.Style = Application.Current.TryFindResource("XamComboEditorStyle") as Style;
            }
        }
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}