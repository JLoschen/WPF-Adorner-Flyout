using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FlyoutDemo.Flyouts;

namespace FlyoutDemo
{
    public class FlyOutAdornerGrid : Grid
    {
        #region Dependency Properties
        public static readonly DependencyProperty IsFlyoutVisibleProperty = DependencyProperty.Register(nameof(IsFlyoutVisible), typeof(bool), typeof(FlyOutAdornerGrid), new FrameworkPropertyMetadata(IsAdornerVisible_PropertyChanged));
        public static readonly DependencyProperty FlyoutContentProperty = DependencyProperty.Register(nameof(FlyoutContent), typeof(FrameworkElement), typeof(FlyOutAdornerGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty HiddenYProperty = DependencyProperty.Register(nameof(HiddenY), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty HiddenXProperty = DependencyProperty.Register(nameof(HiddenX), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
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

        public FrameworkElement FlyoutContent
        {
            get { return (FrameworkElement)GetValue(FlyoutContentProperty); }
            set { SetValue(FlyoutContentProperty, value); }
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
            switch (FlyoutPlacement)
            {
                case FlyoutPlacement.TopLeft:
                    //return GetTopLeftCornerFlyout();
                    //var flyoutControl = new TopLeftCornerFlyout
                    //{
                    //    DataContext = this
                    //};

                    //flyoutControl.Loaded += OnLoadedTopLeft;

                    //return flyoutControl;
                    var element = new TopLeftCornerFlyout { DataContext = this };
                    element.Loaded += OnLoaded;
                    //FlyoutContent.DataContext = DataContext;
                    return element;
                case FlyoutPlacement.TopRight:
                    return GetTopRightCornerFlyout();
                case FlyoutPlacement.BottomRight:
                    return GetBottomRightCornerFlyout();
                case FlyoutPlacement.BottomLeft:
                    return GetBottomLeftCornerFlyout();
                case FlyoutPlacement.Top:
                    return GetTopFlyout();
                case FlyoutPlacement.Right:
                    return GetRightFlyout();
                case FlyoutPlacement.Bottom:
                    return GetBottomFlyout();
                case FlyoutPlacement.Left:
                    return GetLeftFlyout();
            }
            return GetTopLeftCornerFlyout();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FlyoutContent.DataContext = DataContext;
        }

        private LeftFlyout GetLeftFlyout()
        {
            var flyoutControl = new LeftFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedLeft;
            return flyoutControl;
        }

        private void OnLoadedLeft(object sender, RoutedEventArgs e)
        {
            var flyout = sender as LeftFlyout;
            if (flyout == null) return;
            HiddenX = 15 - (int)flyout.ActualWidth;
            FlyoutContent.DataContext = DataContext;
        }

        private BottomFlyout GetBottomFlyout()
        {
            var flyoutControl = new BottomFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedBottom;
            return flyoutControl;
        }

        private void OnLoadedBottom(object sender, RoutedEventArgs e)
        {
            var flyout = sender as BottomFlyout;
            if (flyout == null) return;
            HiddenY = (int)flyout.ActualHeight - 15;
            FlyoutContent.DataContext = DataContext;
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

        private BottomRightCornerFlyout GetBottomRightCornerFlyout()
        {
            var flyoutControl = new BottomRightCornerFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedBottomRight;
            return flyoutControl;
        }

        private BottomLeftCornerFlyout GetBottomLeftCornerFlyout()
        {
            var flyoutControl = new BottomLeftCornerFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedBottomLeft;
            return flyoutControl;
        }

        private TopRightCornerFlyout GetTopRightCornerFlyout()
        {
            var flyoutControl = new TopRightCornerFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedTopRight;
            return flyoutControl;
        }

        private TopLeftCornerFlyout GetTopLeftCornerFlyout()
        {
            var flyoutControl = new TopLeftCornerFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedTopLeft;

            return flyoutControl;
        }

        private TopFlyout GetTopFlyout()
        {
            var flyoutControl = new TopFlyout
            {
                DataContext = this
            };
            
            flyoutControl.Loaded += OnLoadedTop;
            return flyoutControl;
        }

        private RightFlyout GetRightFlyout()
        {
            var flyoutControl = new RightFlyout
            {
                DataContext = this
            };

            flyoutControl.Loaded += OnLoadedRight;
            return flyoutControl;
        }

        private void OnLoadedRight(object sender, RoutedEventArgs e)
        {
            var flyout = sender as RightFlyout;
            if (flyout == null) return;
            HiddenX = (int)flyout.ActualWidth - 15;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoadedBottomLeft(object sender, RoutedEventArgs e)
        {
            var flyout = sender as BottomLeftCornerFlyout;
            if (flyout == null) return;
            HiddenY = (int)flyout.ActualHeight - 31;
            HiddenX = 31 - (int)flyout.ActualWidth;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoadedTop(object sender, RoutedEventArgs e)
        {
            var flyout = sender as TopFlyout;
            if (flyout == null) return;
            HiddenY = 15 - (int)flyout.ActualHeight;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoadedTopLeft(object sender, RoutedEventArgs e)
        {
            var flyout = sender as TopLeftCornerFlyout;
            if (flyout == null) return;
            HiddenX = 31 - (int)flyout.ActualWidth;
            HiddenY = 31 - (int)flyout.ActualHeight;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoadedTopRight(object sender, RoutedEventArgs e)
        {
            var flyout = sender as TopRightCornerFlyout;
            if (flyout == null) return;
            HiddenY = 31 - (int)flyout.ActualHeight;
            HiddenX = (int)flyout.ActualWidth - 31;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoadedBottomRight(object sender, RoutedEventArgs e)
        {
            var flyout = sender as BottomRightCornerFlyout;
            if (flyout == null) return;
            HiddenY = (int)flyout.ActualHeight - 31;
            HiddenX = (int)flyout.ActualWidth - 31;
            FlyoutContent.DataContext = DataContext;
        }

        private void OnLoaded2(object sender, RoutedEventArgs e)
        {
            var flyout = sender as Flyout2;
            if (flyout == null) return;
            HiddenX = 25 - (int)flyout.ActualWidth;
            HiddenY = 25 - (int)flyout.ActualHeight;
            FlyoutContent.DataContext = DataContext;
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
