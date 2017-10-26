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
        }

        private void OnTest()
        {
            Console.WriteLine("huzzah");
        }

        public ICommand TestCommand { get; set; }
    }
}
