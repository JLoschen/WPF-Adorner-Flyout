using System;
using System.Windows;
using System.Windows.Media;

namespace FlyoutDemo
{
    public partial class TopFlyout 
    {
        private readonly Style _arrowButtonMouseOverStyle;
        private readonly Style _arrowButtonOpaqueStyle;

        public TopFlyout()
        {
            InitializeComponent();
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
