using DevExpress.Mvvm;
using Hotel_JustFriend.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel_JustFriend.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ICommand MenuOpenCommand { get; set; }
        public ICommand MenuCloseCommand { get; set; }
        #endregion
        public MainViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>(p => true, p => CloseWindow(p));
            MaximizeWindowCommand = new RelayCommand<object>(p => true, p => MaximizeWindow(p));
            MinimizeWindowCommand = new RelayCommand<object>(p => true, p => MinimizeWindow(p));
            MouseMoveWindowCommand = new RelayCommand<object>(p => true, p => MouseMoveWindow(p));
            MenuOpenCommand = new RelayCommand<object>(p => true, p => MenuOpen(p));
            MenuCloseCommand = new RelayCommand<object>(p => true, p => MenuClose(p));
        }
        #region Methods
        public void MenuOpen(object p)
        {
            if (p != null) (p as Border).IsEnabled = false;
        }
        public void MenuClose(object p)
        {
            if (p != null) (p as Border).IsEnabled = true;
        }
        public void MouseMoveWindow(object p)
        {
            if (p != null)
            {
                try
                {
                    (p as Window).DragMove();
                }
                catch { return; }
            }
        }
        public void MaximizeWindow(object p)
        {
            if (p != null)
            {
                var w = p as Window;
                if (w.WindowState != WindowState.Maximized)
                {
                    w.WindowState = WindowState.Maximized;
                }
                else
                {
                    w.WindowState = WindowState.Normal;
                }
            }
        }
        public void MinimizeWindow(object p)
        {
            if (p != null)
            {
                var w = p as Window;
                if (w.WindowState != WindowState.Minimized)
                {
                    w.WindowState = WindowState.Minimized;
                }
            }
        }
        public void CloseWindow(object p)
        {
            if (p != null)
            {
                if (MyMessageBox.Show("Bạn muốn thoát khỏi chương trình?", "Nhắc nhở", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    (p as Window).Close();
                }
            }
        }
        #endregion
    }
}