using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlyoutDemo
{
    public partial class Flyout2
    {
        private readonly Style _arrowButtonMouseOverStyle;
        private readonly Style _arrowButtonOpaqueStyle;
        
        public Flyout2()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
            _arrowButtonMouseOverStyle = FindResource("OuterBorderStyle") as Style;
            _arrowButtonOpaqueStyle = FindResource("OuterBorderOpaqueStyle") as Style;
            OuterBorder.Style = _arrowButtonMouseOverStyle;

            //var tf = new TranslateTransform
            //{
            //    X = -100,
            //    Y = -30
            //};

            //tf.SetValue(FrameworkElement.NameProperty, "FlyOut");
            //OuterBorder.RenderTransform = tf;

            //var x = PlaceHolder1.Width;

            Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("width " + PlaceHolder1.Width);
            Console.WriteLine("Actual width " + PlaceHolder1.ActualWidth);
            MyX = 25 - (int)ActualWidth;
            MyY = 25 - (int)ActualHeight;
        }

        //public int MyX { get; set; } = -5;
        //public int MyY { get; set; } = -10;
        public int BorderHeight { get; set; } = 55;
        public int BorderWidth { get; set; } = 125;

        #region DependencyProps
        public static readonly DependencyProperty ClearFilterCommandProperty = DependencyProperty.Register(nameof(ClearFilterCommand), typeof(ICommand), typeof(Flyout2), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowFilterCheckedProperty = DependencyProperty.Register(nameof(ShowFilterChecked), typeof(bool), typeof(Flyout2), new PropertyMetadata(false));
        public static readonly DependencyProperty PlaceHolder1Property = DependencyProperty.Register(nameof(PlaceHolder1), typeof(FrameworkElement), typeof(Flyout2), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ContentXProperty = DependencyProperty.Register(nameof(ContentX), typeof(int), typeof(Flyout2), new PropertyMetadata(0));
        public static readonly DependencyProperty ContentYProperty = DependencyProperty.Register(nameof(ContentY), typeof(int), typeof(Flyout2), new PropertyMetadata(0));
        public static readonly DependencyProperty MyYProperty = DependencyProperty.Register(nameof(MyY), typeof(int), typeof(Flyout2), new PropertyMetadata(0));
        public static readonly DependencyProperty MyXProperty = DependencyProperty.Register(nameof(MyX), typeof(int), typeof(Flyout2), new PropertyMetadata(0));

        public int MyY
        {
            get { return (int)GetValue(MyYProperty); }
            set { SetValue(MyYProperty, value); }
        }

        public int MyX
        {
            get { return (int)GetValue(MyXProperty); }
            set { SetValue(MyXProperty, value); }
        }

        public int ContentY
        {
            get { return (int)GetValue(ContentYProperty); }
            set { SetValue(ContentYProperty, value); }
        }

        public int ContentX
        {
            get { return (int)GetValue(ContentXProperty); }
            set { SetValue(ContentXProperty, value); }
        }

        public /*object*/FrameworkElement PlaceHolder1
        {
            get { return /*(object)*/(FrameworkElement) GetValue(PlaceHolder1Property); }
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
        #endregion

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
