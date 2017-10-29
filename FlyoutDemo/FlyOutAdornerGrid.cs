using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FlyoutDemo
{
    public class FlyOutAdornerGrid : Grid
    {
        #region Dependency Properties
        public static readonly DependencyProperty IsFlyoutVisibleProperty = DependencyProperty.Register(nameof(IsFlyoutVisible), typeof(bool), typeof(FlyOutAdornerGrid), new FrameworkPropertyMetadata(IsAdornerVisible_PropertyChanged));
        public static readonly DependencyProperty PlaceHolder1Property = DependencyProperty.Register(nameof(PlaceHolder1), typeof(FrameworkElement), typeof(FlyOutAdornerGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty MyYProperty = DependencyProperty.Register(nameof(MyY), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty MyXProperty = DependencyProperty.Register(nameof(MyX), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty ExpandedYProperty = DependencyProperty.Register(nameof(ExpandedY), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty ExpandedXProperty = DependencyProperty.Register(nameof(ExpandedX), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty FlyoutPlacementProperty = DependencyProperty.Register(nameof(FlyoutPlacement), typeof(FlyoutPlacement), typeof(FlyOutAdornerGrid), new PropertyMetadata(FlyoutPlacement.TopLeft, OnFlyoutPositionChanged));

        private static void OnFlyoutPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flyoutAdornerGrid = d as FlyOutAdornerGrid;
            //flyoutAdornerGrid?.ShowOrHideAdornerInternal();
            flyoutAdornerGrid?.OnPlacementChanged();
        }

        public FlyoutPlacement FlyoutPlacement
        {
            get { return (FlyoutPlacement)GetValue(FlyoutPlacementProperty); }
            set { SetValue(FlyoutPlacementProperty, value); }
        }

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

        public int ExpandedY
        {
            get { return (int)GetValue(ExpandedYProperty); }
            set { SetValue(ExpandedYProperty, value); }
        }

        public int ExpandedX
        {
            get { return (int)GetValue(ExpandedXProperty); }
            set { SetValue(ExpandedXProperty, value); }
        }

        public FrameworkElement PlaceHolder1
        {
            get { return (FrameworkElement)GetValue(PlaceHolder1Property); }
            set { SetValue(PlaceHolder1Property, value); }
        }

        public bool IsFlyoutVisible
        {
            get { return (bool)GetValue(IsFlyoutVisibleProperty); }
            set { SetValue(IsFlyoutVisibleProperty, value); }
        }

        private static void IsAdornerVisible_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlyOutAdornerGrid c = (FlyOutAdornerGrid)d;
            c.ShowOrHideAdornerInternal();
            //c.OnPlacementChanged();
        }
        #endregion

        //private DataGrid _adornedDataGrid;
        //private Grid _adornedDataGrid;
        //private UserControl _adornedDataGrid;
        private FrameworkElement _adornedDataGrid;
        //private UIElement _adornedDataGrid;
        private AdornerLayer _adornerLayer;
        private FrameworkElementAdorner _adorner;
        private FrameworkElement _adornerContent;

        public FlyOutAdornerGrid()
        {
            IsVisibleChanged += (o, e) =>
            {
                if (!IsVisible || !IsFlyoutVisible) return;
                //readd adorner since it is lost when control is hidden
                HideAdornerInternal();
                ShowAdornerInternal();
            };

            //ShowAdornerInternal();
        }

        private void ShowOrHideAdornerInternal()
        {
            if (IsFlyoutVisible)
            {
                ShowAdornerInternal();
            }
            else
            {
                HideAdornerInternal();
            }
        }

        private void ShowAdornerInternal()
        {
            if (_adorner != null)
            {
                // Already adorned.
                return;
            }

            if (_adornerLayer == null)
            {
                //var dg = UIHelper.TryFindParent<DataGrid>(this);
                var dg = this;

                if (dg != null /*&& !ControlIsAdorned(dg)*/)
                {
                    //if (_adornerContent == null)
                    //{
                        _adornerContent = CreateContent();
                    //}
                    _adornedDataGrid = dg;
                    _adornerLayer = AdornerLayer.GetAdornerLayer(dg);
                }
            }

            if (_adornerLayer == null) return;
            _adorner = new FrameworkElementAdorner(_adornerContent, _adornedDataGrid, AdornerPlacement.Inside, AdornerPlacement.Inside, /*AdornerX*/0, /*AdornerY*/0);
            _adornerLayer.Add(_adorner);
        }

        public void OnPlacementChanged()
        {
            if (!IsFlyoutVisible) return;
            //if (_adorner != null)
            //{
            //    // Already adorned.
            //    return;
            //}

            //if (_adornerLayer == null)
            //{
            //var dg = UIHelper.TryFindParent<DataGrid>(this);
            var dg = this;

                //if (dg != null && !ControlIsAdorned(dg))
                //{
                    //if (_adornerContent == null)
                    //{
                        _adornerContent = CreateContent();
                    //}
                    _adornedDataGrid = dg;
                    _adornerLayer = AdornerLayer.GetAdornerLayer(dg);
                //}
            //}

            if (_adornerLayer == null) return;

            if (_adorner != null)
            {
                _adornerLayer.Remove(_adorner);
                _adorner.DisconnectChild();
            }
            
            _adorner = new FrameworkElementAdorner(_adornerContent, _adornedDataGrid, AdornerPlacement.Inside, AdornerPlacement.Inside, /*AdornerX*/0, /*AdornerY*/0);
            
            _adornerLayer.Add(_adorner);
        }

        private FrameworkElement CreateContent()
        {
            //return GetCornerFlyout();
            //return GetTopFlyout();
            //return GetFlyout2();
            //return GetTopRightCornerFlyout();
            //return GetBottomRightCornerFlyout();
            //return GetBottomLeftCornerFlyout();

            switch (FlyoutPlacement)
            {
                case FlyoutPlacement.TopLeft:
                return GetCornerFlyout();
                case FlyoutPlacement.TopRight:
                return GetTopRightCornerFlyout();
                case FlyoutPlacement.BottomRight:
                return GetBottomRightCornerFlyout();
                case FlyoutPlacement.BottomLeft:
                return GetBottomLeftCornerFlyout();
                case FlyoutPlacement.Top:
                return GetTopFlyout();
            }
            return GetCornerFlyout();
        }

        private Flyout2 GetFlyout2()
        {
            var flyoutControl = new Flyout2
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ClipToBounds = true,
                DataContext = this
            };

            flyoutControl.Loaded += OnLoaded2;
            return flyoutControl;
        }

        private Flyouts.BottomRightCornerFlyout GetBottomRightCornerFlyout()
        {
            var flyoutControl = new Flyouts.BottomRightCornerFlyout
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                ClipToBounds = true,
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedBottomRight;
            return flyoutControl;
        }

        private Flyouts.BottomLeftCornerFlyout GetBottomLeftCornerFlyout()
        {
            var flyoutControl = new Flyouts.BottomLeftCornerFlyout
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                ClipToBounds = true,
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedBottomLeft;
            return flyoutControl;
        }

        private Flyouts.TopRightCornerFlyout GetTopRightCornerFlyout()
        {
            var flyoutControl = new Flyouts.TopRightCornerFlyout
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                ClipToBounds = true,
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedTopRight;
            return flyoutControl;
        }

        private Flyouts.CornerFlyout GetCornerFlyout()
        {
            var flyoutControl = new /*Flyout2*/Flyouts.CornerFlyout
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ClipToBounds = true,
                DataContext = this
            };

            flyoutControl.Loaded += OnLoaded;

            //Binding contentBinding = new Binding
            //{
            //    Source = this,
            //    Path = new PropertyPath("PlaceHolder1"),
            //    Mode = BindingMode.TwoWay,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //};
            //BindingOperations.SetBinding(flyoutControl, Flyout2.PlaceHolder1Property, contentBinding);

            return flyoutControl;
        }

        private Flyouts.TopFlyout GetTopFlyout()
        {
            var flyoutControl = new Flyouts.TopFlyout
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                ClipToBounds = true,
                DataContext = this
            };
            
            flyoutControl.Loaded += OnLoadedTop;
            return flyoutControl;
        }
        
        private void OnLoadedBottomLeft(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyouts.BottomLeftCornerFlyout;
            if (flyout == null) return;
            MyY = (int)flyout.ActualHeight - 31;
            MyX = 31 - (int)flyout.ActualWidth;
            ExpandedX = -5;
            ExpandedY = 5;
            PlaceHolder1.DataContext = DataContext;
        }

        private void OnLoadedTop(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyouts.TopFlyout;
            if (flyout == null) return;
            MyY = 15 - (int)flyout.ActualHeight;
            PlaceHolder1.DataContext = DataContext;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyouts.CornerFlyout;
            if (flyout == null) return;
            MyX = 31 - (int)flyout.ActualWidth;
            MyY = 31 - (int)flyout.ActualHeight;
            ExpandedX = -5;
            ExpandedY = -5;
            PlaceHolder1.DataContext = DataContext;
        }

        private void OnLoadedTopRight(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyouts.TopRightCornerFlyout;
            if (flyout == null) return;
            MyY = 31 - (int)flyout.ActualHeight;
            MyX = (int)flyout.ActualWidth - 31;
            ExpandedX = 5;
            ExpandedY = -5;
            PlaceHolder1.DataContext = DataContext;
        }

        private void OnLoadedBottomRight(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyouts.BottomRightCornerFlyout;
            if (flyout == null) return;
            MyY = (int)flyout.ActualHeight - 31;
            MyX = (int)flyout.ActualWidth - 31;
            ExpandedX = 5;
            ExpandedY = 5;
            PlaceHolder1.DataContext = DataContext;
        }

        private void OnLoaded2(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyout2;
            if (flyout == null) return;
            MyX = 25 - (int)flyout.ActualWidth;
            MyY = 25 - (int)flyout.ActualHeight;
            PlaceHolder1.DataContext = DataContext;
        }

        private bool ControlIsAdorned(UIElement control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);
            
            if (layer == null) return false;

            var adorners = layer.GetAdorners(control);
            return adorners != null && adorners.Length > 0;
        }

        private void HideAdornerInternal()
        {
            if (_adornerLayer == null || _adorner == null)
            {
                // Not already adorned.
                return;
            }

            _adornerLayer.Remove(_adorner);
            _adorner.DisconnectChild();

            _adorner = null;
            _adornerLayer = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ShowOrHideAdornerInternal();
        }
    }
}
