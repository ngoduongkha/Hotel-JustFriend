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
        public string UserName { get => _UserName; set { _UserName = value; RaisePropertyChanged(); } }
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
        public void Login(LoginWindow p)
        {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    p.txtUserName.Focus();
                    MyMessageBox.Show("Vui lòng điền tài khoản!", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    p.txtPassword.Focus();
                    MyMessageBox.Show("Vui lòng điền mật khẩu!", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                string passEncode = Utility.Encryption.EncryptPassword(Password);
                var count = DataProvider.Instance.DB.Accounts.Where(x => x.username == UserName && x.password == passEncode).Count();

                if (count > 0)
                {
                    MainWindow main = new MainWindow(UserName);
                    p.Hide();
                    main.ShowDialog();
                    p.Show();
                }
                else
                {
                    p.txtUserName.Text = string.Empty;
                    p.txtPassword.Clear();
                    p.txtUserName.Focus();
                    MyMessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch
            {
                MyMessageBox.Show("Lỗi", "Thông báo", MessageBoxButton.OK);
                App.Current.Shutdown();
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
