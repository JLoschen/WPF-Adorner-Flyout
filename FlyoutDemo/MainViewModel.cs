using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FlyoutDemo
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            TestCommand = new RelayCommand(OnTest);
            _flyoutLocation = FlyoutPlacement.TopLeft;
            _isTopLeft = true;
            _showAdorner = true;
        }

        private void OnTest()
        {
            Console.WriteLine("huzzah");
        }

        public ICommand TestCommand { get; set; }

        public FlyoutPlacement FlyoutLocation 
        { 
            get{ return _flyoutLocation; } 
            set{ Set(ref _flyoutLocation, value); } 
        } 
        private FlyoutPlacement _flyoutLocation;

        public bool IsExpanded 
        { 
            get{ return _isExpanded; } 
            set{ Set(ref _isExpanded, value); } 
        } 
        private bool _isExpanded;

        public bool ShowAdorner 
        { 
            get{ return _showAdorner; } 
            set{ Set(ref _showAdorner, value); } 
        } 
        private bool _showAdorner;

        public bool IsTopLeft 
        { 
            get{ return _isTopLeft; }
            set
            {
                if (Set(ref _isTopLeft, value) && _isTopLeft)
                {
                    FlyoutLocation = FlyoutPlacement.TopLeft;
                }
            } 
        } 
        private bool _isTopLeft;

        public bool IsLeft
        {
            get { return _isLeft; }
            set
            {
                if (Set(ref _isLeft, value) && _isLeft)
                {
                    FlyoutLocation = FlyoutPlacement.Left;
                }
            }
        }
        private bool _isLeft;

        public bool IsTopRight 
        { 
            get{ return _isTopRight; }
            set
            {
                if (Set(ref _isTopRight, value) && _isTopRight)
                {
                    FlyoutLocation = FlyoutPlacement.TopRight;
                }
            } 
        } 
        private bool _isTopRight;

        public bool IsTop
        {
            get { return _isTop; }
            set
            {
                if (Set(ref _isTop, value) && _isTop)
                {
                    FlyoutLocation = FlyoutPlacement.Top;
                }
            }
        }
        private bool _isTop;

        public bool IsBottomRight 
        { 
            get{ return _isBottomRight; }
            set
            {
                if (Set(ref _isBottomRight, value) && _isBottomRight)
                {
                    FlyoutLocation = FlyoutPlacement.BottomRight;
                }
            } 
        } 
        private bool _isBottomRight;

        public bool IsRight
        {
            get { return _isRight; }
            set
            {
                if (Set(ref _isRight, value) && _isRight)
                {
                    FlyoutLocation = FlyoutPlacement.Right;
                }
            }
        }
        private bool _isRight;

        public bool IsBottomLeft 
        { 
            get{ return _isBottomLeft; }
            set
            {
                if (Set(ref _isBottomLeft, value) && _isBottomLeft)
                {
                    FlyoutLocation = FlyoutPlacement.BottomLeft;
                }
            } 
        } 
        private bool _isBottomLeft;

        public bool IsBottom
        {
            get { return _isBottom; }
            set
            {
                if (Set(ref _isBottom, value) && _isBottom)
                {
                    FlyoutLocation = FlyoutPlacement.Bottom;
                }
            }
        }
        private bool _isBottom;
    }
}
