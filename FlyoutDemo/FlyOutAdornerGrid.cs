using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using FlyoutDemo.Flyouts;

namespace FlyoutDemo
{
    public class FlyOutAdornerGrid : Grid
    {
        #region Dependency Properties
        public static readonly DependencyProperty IsFlyoutVisibleProperty = DependencyProperty.Register(nameof(IsFlyoutVisible), typeof(bool), typeof(FlyOutAdornerGrid), new FrameworkPropertyMetadata(IsAdornerVisible_PropertyChanged));
        public static readonly DependencyProperty FlyoutContentProperty = DependencyProperty.Register(nameof(FlyoutContent), typeof(FrameworkElement), typeof(FlyOutAdornerGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty FlyoutPlacementProperty = DependencyProperty.Register(nameof(FlyoutPlacement), typeof(FlyoutPlacement), typeof(FlyOutAdornerGrid), new PropertyMetadata(FlyoutPlacement.TopLeft, OnFlyoutPositionChanged));
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(nameof(BorderColor), typeof(SolidColorBrush), typeof(FlyOutAdornerGrid), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
        public static readonly DependencyProperty ShowDropShadowProperty = DependencyProperty.Register(nameof(ShowDropShadow), typeof(bool), typeof(FlyOutAdornerGrid), new PropertyMetadata(true));
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(FlyOutAdornerGrid), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

        public bool ShowDropShadow
        {
            get { return (bool)GetValue(ShowDropShadowProperty); }
            set { SetValue(ShowDropShadowProperty, value); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
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
            var content = GetFlyout();
            content.DataContext = this;
            content.Loaded += OnLoaded;
            return content;
        }

        private FrameworkElement GetFlyout()
        {
            switch (FlyoutPlacement)
            {
                case FlyoutPlacement.TopLeft:
                    return new TopLeftCornerFlyout();
                case FlyoutPlacement.TopRight:
                    return new TopRightCornerFlyout();
                case FlyoutPlacement.BottomRight:
                    return new BottomRightCornerFlyout();
                case FlyoutPlacement.BottomLeft:
                    return new BottomLeftCornerFlyout();
                case FlyoutPlacement.Top:
                    return new TopFlyout();
                case FlyoutPlacement.Right:
                    return new RightFlyout();
                case FlyoutPlacement.Bottom:
                    return new BottomFlyout();
                case FlyoutPlacement.Left:
                    return new LeftFlyout();
                default:
                    return new TopLeftCornerFlyout();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
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
