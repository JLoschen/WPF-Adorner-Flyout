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

        public bool ShowAdorner 
        { 
            get{ return _showAdorner; } 
            set{ Set(ref _showAdorner, value); } 
        } 
        private bool _showAdorner;
        
        //private bool ResetFlyoutLocation()
        //{

        //}

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
    }
}
