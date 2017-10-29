using System;
using System.Windows;

namespace FlyoutDemo.Flyouts
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
            OuterBorder.Style = _arrowButtonOpaqueStyle;
        }

        private void OnHidden(object sender, EventArgs e)
        {
            OuterBorder.Style = _arrowButtonMouseOverStyle;
        }
    }
}
