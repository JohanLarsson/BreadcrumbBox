namespace BreadCrumbBox
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public static class Hack
    {
        private static readonly List<TreeViewItem> Items = new List<TreeViewItem>();
        public static readonly DependencyProperty SyncSelectionProperty = DependencyProperty.RegisterAttached(
            "SyncSelection",
            typeof(bool),
            typeof(Hack),
            new PropertyMetadata(default(bool), OnSyncSelectionChanged));

        static Hack()
        {
            CurrentSelection.Instance.SelectedNodeChanged += OnSelectedNodeChanged;
        }

        public static void SetSyncSelection(this TreeViewItem element, bool value)
        {
            element.SetValue(SyncSelectionProperty, value);
        }

        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TreeViewItem))]
        public static bool GetSyncSelection(this TreeViewItem element)
        {
            return (bool)element.GetValue(SyncSelectionProperty);
        }

        private static void OnSyncSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Items.Add((TreeViewItem)d);
        }

        private static void OnSelectedNodeChanged(object sender, INode e)
        {
            foreach (var item in Items)
            {
                item.IsSelected = item.DataContext == e;
            }
        }
    }
}