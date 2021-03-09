using DevExpress.Mvvm;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel_JustFriend.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        #region Properties
        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(p => true, p => Login(p));
            CloseCommand = new RelayCommand<object>(p => true, p => Close(p));
            MouseMoveWindowCommand = new RelayCommand<object>(p => true, p => MouseMoveWindow(p));
        }

        #region Method
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
        public void Login(object p)
        {
            if (p != null)
            {
                MainWindow main = new MainWindow();
                var window = p as Window;
                window.Hide();
                main.ShowDialog();
                window.Show();
            }
        }
        public void Close(object p)
        {
            if (p != null)
            {
                (p as Window).Close();                
            }
        }
        #endregion
    }
}
