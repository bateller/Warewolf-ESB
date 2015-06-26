
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2014 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/


// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 11.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace Dev2.Server.UI.Tests.DatePickerWizardUIMapClasses
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public partial class DatePickerWizardUIMap
    {
        
        /// <summary>
        /// ClickNameTextBox
        /// </summary>
        public void ClickNameTextBox()
        {
            #region Variable Declarations
            HtmlEdit uIDev2elementNameDatePEdit = this.UIWebpageWindowsInternWindow.UIWebpageDocument.UIDev2elementNameDatePEdit;
            #endregion

            // Click 'Dev2elementNameDatePicker' text box
            Mouse.Click(uIDev2elementNameDatePEdit, new Point(72, 12));
        }

        public string GetNameTextBoxText()
        {
            HtmlEdit uIDev2elementNameDatePEdit = this.UIWebpageWindowsInternWindow.UIWebpageDocument.UIDev2elementNameDatePEdit;
            return uIDev2elementNameDatePEdit.Text;
        }
        
        #region Properties
        public UIWebpageWindowsInternWindow UIWebpageWindowsInternWindow
        {
            get
            {
                if ((this.mUIWebpageWindowsInternWindow == null))
                {
                    this.mUIWebpageWindowsInternWindow = new UIWebpageWindowsInternWindow();
                }
                return this.mUIWebpageWindowsInternWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIWebpageWindowsInternWindow mUIWebpageWindowsInternWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UIWebpageWindowsInternWindow : BrowserWindow
    {
        
        public UIWebpageWindowsInternWindow()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.Name] = "Webpage";
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
            this.WindowTitles.Add("Webpage");
            #endregion
        }
        
        public void LaunchUrl(System.Uri url)
        {
            this.CopyFrom(BrowserWindow.Launch(url));
        }
        
        #region Properties
        public UIWebpageDocument UIWebpageDocument
        {
            get
            {
                if ((this.mUIWebpageDocument == null))
                {
                    this.mUIWebpageDocument = new UIWebpageDocument(this);
                }
                return this.mUIWebpageDocument;
            }
        }
        #endregion
        
        #region Fields
        private UIWebpageDocument mUIWebpageDocument;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class UIWebpageDocument : HtmlDocument
    {
        
        public UIWebpageDocument(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[HtmlDocument.PropertyNames.Id] = null;
            this.SearchProperties[HtmlDocument.PropertyNames.RedirectingPage] = "False";
            this.SearchProperties[HtmlDocument.PropertyNames.FrameDocument] = "False";
            this.FilterProperties[HtmlDocument.PropertyNames.Title] = "Webpage";
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/services/Date%20Picker.wiz";
            this.FilterProperties[HtmlDocument.PropertyNames.PageUrl] = "http://127.0.0.1:1234/services/Date%20Picker.wiz";
            this.WindowTitles.Add("Webpage");
            #endregion
        }
        
        #region Properties
        public HtmlEdit UIDev2elementNameDatePEdit
        {
            get
            {
                if ((this.mUIDev2elementNameDatePEdit == null))
                {
                    this.mUIDev2elementNameDatePEdit = new HtmlEdit(this);
                    #region Search Criteria
                    this.mUIDev2elementNameDatePEdit.SearchProperties[HtmlEdit.PropertyNames.Id] = "Dev2elementNameDatePicker";
                    this.mUIDev2elementNameDatePEdit.SearchProperties[HtmlEdit.PropertyNames.Name] = "Dev2elementNameDatePicker";
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.LabeledBy] = null;
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.Type] = "SINGLELINE";
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.Class] = "requiredClass ";
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "id=\"Dev2elementNameDatePicker\" class=\"re";
                    this.mUIDev2elementNameDatePEdit.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "1";
                    this.mUIDev2elementNameDatePEdit.WindowTitles.Add("Webpage");
                    #endregion
                }
                return this.mUIDev2elementNameDatePEdit;
            }
        }
        #endregion
        
        #region Fields
        private HtmlEdit mUIDev2elementNameDatePEdit;
        #endregion
    }
}
