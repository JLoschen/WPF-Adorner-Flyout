using System.Windows;
using System.Windows.Controls;

namespace FlyoutDemo.Flyouts
{
    public class FlyoutBase:UserControl
    {
        public static readonly DependencyProperty HiddenYProperty = DependencyProperty.Register(nameof(HiddenY), typeof(int), typeof(FlyoutBase), new PropertyMetadata(0));
        public static readonly DependencyProperty HiddenXProperty = DependencyProperty.Register(nameof(HiddenX), typeof(int), typeof(FlyoutBase), new PropertyMetadata(0));

        public int HiddenY
        {
            get { return (int)GetValue(HiddenYProperty); }
            set { SetValue(HiddenYProperty, value); }
        }

        public int HiddenX
        {
            get { return (int)GetValue(HiddenXProperty); }
            set { SetValue(HiddenXProperty, value); }
        }

        //public FlyoutBase()
        //{
        //    (Content as FrameworkElement).DataContext = this;
        //}
    }
}
