using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class LoginViewModel : ViewModelBase
    {
        #region Variables
        private string _UserName;
        private string _Password;
        #endregion
        
        #region Properties
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set { _Password = value; RaisePropertyChanged(); } }
        #endregion

        #region Method
        [Command]
        public void MouseMoveWindow(Window p)
        {
            try
            {
                p.DragMove();
            }
            catch { return; }
        }
        [Command]
        public void Login(Window p)
        {
            try
            {
                string passEncode = Utility.Encryption.EncryptPassword(Password);
                var count = DataProvider.Instance.DB.Accounts.Where(x => x.username == UserName && x.password == passEncode).Count();

                if (count > 0)
                {
                    MainWindow main = new MainWindow();
                    p.Hide();
                    main.ShowDialog();
                    p.Show();
                }
                else
                {
                    MyMessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch 
            {
                MyMessageBox.Show("Vui lòng điền tài khoản và mật khẩu", "Thông báo", MessageBoxButton.OK);
                return; 
            }
        }
        [Command]
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        [Command]
        public void OnPasswordChanged(PasswordBox p)
        {
            try
            {
                Password = p.Password;
            }
            catch { return; }
        }
        #endregion
    }
}
