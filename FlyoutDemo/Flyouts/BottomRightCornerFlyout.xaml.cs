using System;
using System.Windows;

namespace FlyoutDemo.Flyouts
{
    public partial class BottomRightCornerFlyout 
    {
        private readonly Style _arrowButtonMouseOverStyle;
        private readonly Style _arrowButtonOpaqueStyle;
        public BottomRightCornerFlyout()
        {
            InitializeComponent();
            _arrowButtonMouseOverStyle = FindResource("OuterBorderStyle") as Style;
            _arrowButtonOpaqueStyle = FindResource("OuterBorderOpaqueStyle") as Style;
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }

        private void OnExpanded(object sender, EventArgs e)
        {
            OuterBorder.Style = _arrowButtonOpaqueStyle;
        }

        private void OnHidden(object sender, EventArgs e)
        {
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }
    }
}
