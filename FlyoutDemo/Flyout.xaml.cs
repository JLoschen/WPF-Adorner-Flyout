using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlyoutDemo
{
    public partial class Flyout
    {
        private readonly Style _arrowButtonMouseOverStyle;
        private readonly Style _arrowButtonOpaqueStyle;
        public Flyout()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
            _arrowButtonMouseOverStyle = FindResource("OuterBorderStyle") as Style;
            _arrowButtonOpaqueStyle = FindResource("OuterBorderOpaqueStyle") as Style;
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }

        public static readonly DependencyProperty ClearFilterCommandProperty = DependencyProperty.Register(nameof(ClearFilterCommand), typeof(ICommand), typeof(Flyout), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowFilterCheckedProperty = DependencyProperty.Register(nameof(ShowFilterChecked), typeof(bool), typeof(Flyout), new PropertyMetadata(false));
        public static readonly DependencyProperty PlaceHolder1Property = DependencyProperty.Register(nameof(PlaceHolder1), typeof(object), typeof(Flyout), new UIPropertyMetadata(null));

        public object PlaceHolder1
        {
            get { return (object)GetValue(PlaceHolder1Property); }
            set { SetValue(PlaceHolder1Property, value); }
        }

        public ICommand ClearFilterCommand
        {
            get { return (ICommand)GetValue(ClearFilterCommandProperty); }
            set { SetValue(ClearFilterCommandProperty, value); }
        }

        public bool ShowFilterChecked
        {
            get { return (bool)GetValue(ShowFilterCheckedProperty); }
            set { SetValue(ShowFilterCheckedProperty, value); }
        }

        private void OnExpanded(object sender, EventArgs e)
        {
            var rotate = arrow.RenderTransform as RotateTransform;

            if (rotate != null)
            {
                rotate.Angle = 45;
            }
            OuterBorder.Style = _arrowButtonOpaqueStyle;
        }

        private void OnHidden(object sender, EventArgs e)
        {
            var rotate = arrow.RenderTransform as RotateTransform;
            if (rotate != null)
            {
                rotate.Angle = -135;
            }
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }
    }
}
