namespace Gu.Wpf.BreadCrumbs
{
    using System.Windows;
    using System.Windows.Controls;

    public class BreadCrumbBar : ItemsControl
    {
        static BreadCrumbBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadCrumbBar), new FrameworkPropertyMetadata(typeof(BreadCrumbBar)));
        }
    }
}
