using System;
using System.Windows;
using System.Windows.Media;

namespace FlyoutDemo
{
    public partial class CornerFlyout
    {
        private readonly Style _arrowButtonMouseOverStyle;
        private readonly Style _arrowButtonOpaqueStyle;

        public CornerFlyout()
        {
            InitializeComponent();
            _arrowButtonMouseOverStyle = FindResource("OuterBorderStyle") as Style;
            _arrowButtonOpaqueStyle = FindResource("OuterBorderOpaqueStyle") as Style;
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }

        private void OnExpanded(object sender, EventArgs e)
        {
            //var rotate = arrow.RenderTransform as RotateTransform;

            //if (rotate != null)
            //{
            //    rotate.Angle = 45;
            //}
            OuterBorder.Style = _arrowButtonOpaqueStyle;
        }

        private void OnHidden(object sender, EventArgs e)
        {
            //var rotate = arrow.RenderTransform as RotateTransform;
            //if (rotate != null)
            //{
            //    rotate.Angle = -135;
            //}
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }
    }
}
