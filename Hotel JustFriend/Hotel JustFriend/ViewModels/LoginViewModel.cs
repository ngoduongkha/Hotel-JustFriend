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
        #endregion

        #region Method
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(p => true, p => Login(p));
        }
        public void Login(object p)
        {
            if (p != null)
            {
                MainWindow main = new MainWindow();
                Window window = p as Window;
                window.Hide();
                main.ShowDialog();
                window.Show();
            }
        }
        #endregion
    }
}
