using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Dev2.Studio.Core.AppResources.ExtensionMethods;

namespace Warewolf.Studio.AntiCorruptionLayer
{
    public class TreeviewScrollToEndOnNewContent : Behavior<TreeView>
    {
        #region Class Members

        ScrollViewer _treeviewScrollViewer;
        private bool _hasUserScrolled;

        #endregion Class Members

        #region Override Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
        }

        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= AssociatedObjectLoaded;

            _treeviewScrollViewer = DependencyObjectExtensions.GetChildByType(AssociatedObject, typeof(ScrollViewer)) as ScrollViewer;

            //Juries - Removed, instead implement a collection changed handler, to only scroll to end when new items are added.          
            if (_treeviewScrollViewer != null)
            {
                _treeviewScrollViewer.IsDeferredScrollingEnabled = true;
                _treeviewScrollViewer.PreviewMouseDown += (o, args) => _hasUserScrolled = true;
                _treeviewScrollViewer.ScrollChanged += TreeviewScrollViewerScrollChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            //Juries - Removed, instead implement a collection changed handler, to only scroll to end when new items are added.
            if (_treeviewScrollViewer != null)
            {
                _treeviewScrollViewer.ScrollChanged -= TreeviewScrollViewerScrollChanged;
            }
        }

        #endregion Override Methods

        #region Event Handlers

        //Juries - Removed, instead implement a collection changed handler, to only scroll to end when new items are added.
        void TreeviewScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //This means the user has manipulated it and we dont want to scroll to end anymore
            if (_hasUserScrolled || AssociatedObject.SelectedItem != null)
            {
                return;
            }

            if (((e.ExtentHeightChange > 0 && e.ViewportHeightChange.CompareTo(0D) == 0)))
            {
                _treeviewScrollViewer.ScrollToEnd();
            }
        }

        #endregion Event Handlers
    }
}