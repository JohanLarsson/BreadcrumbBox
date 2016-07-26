namespace BreadcrumbBox
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var newValue = (INode)e.NewValue;
            if (newValue != null)
            {
                CurrentSelection.Instance.SelectedNode = newValue;
            }
        }
    }
}
