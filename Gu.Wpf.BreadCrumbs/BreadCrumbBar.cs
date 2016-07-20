namespace Gu.Wpf.Breadcrumbs
{
    using System.Windows;
    using System.Windows.Controls;

    public class BreadcrumbBar : ItemsControl
    {
        static BreadcrumbBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadcrumbBar), new FrameworkPropertyMetadata(typeof(BreadcrumbBar)));
        }
    }
}
