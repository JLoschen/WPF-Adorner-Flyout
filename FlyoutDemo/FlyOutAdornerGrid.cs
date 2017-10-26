using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace FlyoutDemo
{
    public class FlyOutAdornerGrid : Grid
    {
        #region Dependency Properties
        public static readonly DependencyProperty IsFlyoutVisibleProperty = DependencyProperty.Register(nameof(IsFlyoutVisible), typeof(bool), typeof(FlyOutAdornerGrid), new FrameworkPropertyMetadata(IsAdornerVisible_PropertyChanged));
        public static readonly DependencyProperty ClearFilterCommandProperty = DependencyProperty.Register(nameof(ClearFilterCommand), typeof(ICommand), typeof(FlyOutAdornerGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowFilterProperty = DependencyProperty.Register(nameof(ShowFilter), typeof(bool), typeof(FlyOutAdornerGrid), new PropertyMetadata(false));
        public static readonly DependencyProperty PlaceHolder1Property = DependencyProperty.Register(nameof(PlaceHolder1), typeof(/*object*/FrameworkElement), typeof(FlyOutAdornerGrid), new /*UI*/PropertyMetadata(null));
        public static readonly DependencyProperty ContentXProperty = DependencyProperty.Register(nameof(ContentX), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));
        public static readonly DependencyProperty ContentYProperty = DependencyProperty.Register(nameof(ContentY), typeof(int), typeof(FlyOutAdornerGrid), new PropertyMetadata(0));

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
            get { return (/*object*/FrameworkElement)GetValue(PlaceHolder1Property); }
            set { SetValue(PlaceHolder1Property, value); }
        }
        
        public ICommand ClearFilterCommand
        {
            get { return (ICommand)GetValue(ClearFilterCommandProperty); }
            set { SetValue(ClearFilterCommandProperty, value); }
        }

        public bool IsFlyoutVisible
        {
            get { return (bool)GetValue(IsFlyoutVisibleProperty); }
            set { SetValue(IsFlyoutVisibleProperty, value); }
        }

        public bool ShowFilter
        {
            get { return (bool)GetValue(ShowFilterProperty); }
            set { SetValue(ShowFilterProperty, value); }
        }

        private static void IsAdornerVisible_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlyOutAdornerGrid c = (FlyOutAdornerGrid)d;
            c.ShowOrHideAdornerInternal();
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

            ShowAdornerInternal();
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
                //var dg = 

                if (dg != null && !ControlIsAdorned(dg))
                {
                    if (_adornerContent == null)
                    {
                        _adornerContent = CreateContent();
                    }
                    _adornedDataGrid = dg;
                    _adornerLayer = AdornerLayer.GetAdornerLayer(dg);
                }
            }

            if (_adornerLayer == null) return;
            _adorner = new FrameworkElementAdorner(_adornerContent, _adornedDataGrid, AdornerPlacement.Inside, AdornerPlacement.Inside, /*AdornerX*/0, /*AdornerY*/0);
            _adornerLayer.Add(_adorner);
        }

        private FrameworkElement CreateContent()
        {
            var flyoutControl = new Flyout2
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ClipToBounds = true
            };

            Binding showFilterBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("ShowFilter"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(flyoutControl, Flyout2.ShowFilterCheckedProperty, showFilterBinding);

            Binding contentBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("PlaceHolder1"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(flyoutControl, Flyout2.PlaceHolder1Property, contentBinding);

            Binding clearFilterCommandBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("ClearFilterCommand"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(flyoutControl, Flyout2.ClearFilterCommandProperty, clearFilterCommandBinding);

            return flyoutControl;
        }

        private bool ControlIsAdorned(UIElement control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);
            
            //New
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
