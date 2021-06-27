using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    class AccountInfoViewModel : ViewModelBase
    {
        private string _userName;
        private string _password;
        private string _newPassword;
        private string _confirmPassword;

        public string UserName { get => _userName; }
        public string Password { set => _password = value; }
        public string NewPassword { set => _newPassword = value; }
        public string ConfirmPassword { set => _confirmPassword = value; }

        public AccountInfoViewModel(string userName)
        {
            _userName = userName;
        }

        [Command]
        public void ChangePassword(AccountInfoView uc)
        {
            if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_newPassword) || string.IsNullOrEmpty(_confirmPassword))
            {
                MyMessageBox.Show("Các trường không thể trống!", "Thông báo", MessageBoxButton.OK);
                return;
            }

            var passEncode = Utility.Encryption.EncryptPassword(_password);
            var count = DataProvider.Instance.DB.Accounts.Where(x => x.username == _userName && x.password == passEncode).Count();

            if (count != 1)
            {
                uc.txtPassword.Clear();
                uc.txtNewPassword.Clear();
                uc.txtConfirmPassword.Clear();
                uc.txtPassword.Focus();
                MyMessageBox.Show("Mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButton.OK);
                return;
            }

            if (_newPassword != _confirmPassword)
            {
                uc.txtConfirmPassword.Clear();
                uc.txtConfirmPassword.Focus();
                MyMessageBox.Show("Xác nhận mật khẩu không chính xác!", "Thông báo", MessageBoxButton.OK);
                return;
            }

            var account = DataProvider.Instance.DB.Accounts.Where(x => x.username == _userName && x.password == passEncode).SingleOrDefault();
            account.password = Utility.Encryption.EncryptPassword(_newPassword);

            DataProvider.Instance.DB.SaveChanges();

            MyMessageBox.Show("Thay đổi mật khẩu thành công", "Thông báo", MessageBoxButton.OK);
        }

        [Command]
        public void OnPasswordChanged(PasswordBox p)
        {
            try
            {
                if (p.Name == "txtPassword")
                {
                    Password = p.Password;
                    return;
                }
                if (p.Name == "txtNewPassword")
                {
                    NewPassword = p.Password;
                    return;
                }
                if (p.Name == "txtConfirmPassword")
                {
                    ConfirmPassword = p.Password;
                    return;
                }
            }
            catch { return; }
        }
    }
}
