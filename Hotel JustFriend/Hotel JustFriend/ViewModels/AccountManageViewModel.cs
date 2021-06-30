using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    public class AccountAndType
    {
        public int IdAccount { get; set; }
        public int IdTypeAccount { get; set; }
        public string Username { get; set; }
        public string TypeAccount { get; set; }
    }

    [POCOViewModel]
    class AccountManageViewModel : ViewModelBase
    {
        private ObservableCollection<Account> _ListAccount;
        private ObservableCollection<TypeAccount> _ListTypeAccount;
        private ObservableCollection<TypeAccount> _ListTypeAccountCanAdd;
        private ObservableCollection<AccountAndType> _ListAccountWithType;
        private AccountAndType _SelectedAccount;

        public ObservableCollection<Account> ListAccount { get => _ListAccount; set => _ListAccount = value; }
        public ObservableCollection<TypeAccount> ListTypeAccount { get => _ListTypeAccount; set => _ListTypeAccount = value; }
        public ObservableCollection<TypeAccount> ListTypeAccountCanAdd { get => _ListTypeAccountCanAdd; set => _ListTypeAccountCanAdd = value; }
        public ObservableCollection<AccountAndType> ListAccountWithType { get => _ListAccountWithType; set { _ListAccountWithType = value; RaisePropertyChanged(); } }
        public AccountAndType SelectedAccount { get => _SelectedAccount; set => _SelectedAccount = value; }
        
        public void LoadDB()
        {
            ListAccount = new ObservableCollection<Account>(DataProvider.Instance.DB.Accounts);
            ListAccountWithType = new ObservableCollection<AccountAndType>(
                _ListAccount.Join(_ListTypeAccount, (Account) => Account.IdTypeAccount, (TypeAccount) => TypeAccount.IdTypeAccount,
                (Account, TypeAccount) => new AccountAndType { 
                    IdAccount = Account.IdAccount, 
                    IdTypeAccount = TypeAccount.IdTypeAccount,
                    Username = Account.Username, 
                    TypeAccount = TypeAccount.DisplayName 
                }));
        }

        public AccountManageViewModel()
        {
            ListTypeAccount = new ObservableCollection<TypeAccount>(DataProvider.Instance.DB.TypeAccounts);
            ListTypeAccountCanAdd = new ObservableCollection<TypeAccount>(DataProvider.Instance.DB.TypeAccounts.Where((x) => x.DisplayName != "Admin"));

            LoadDB();
        }

        [Command]
        public void AccountTypeFilter(AccountManageView p)
        {
            LoadDB();

            if (!string.IsNullOrEmpty(p.cbFilterType.Text))
            {
                ListAccountWithType = new ObservableCollection<AccountAndType>(ListAccountWithType.Where((x) => x.TypeAccount == p.cbFilterType.Text));
            }

            p.cbFilterType.Text = string.Empty;
        }

        [Command]
        public void Search(AccountManageView p)
        {
            LoadDB();

            if (!string.IsNullOrEmpty(p.tbSearch.Text) && !string.IsNullOrWhiteSpace(p.tbSearch.Text))
            {
                ListAccountWithType = new ObservableCollection<AccountAndType>(ListAccountWithType.Where((x) => x.Username.Contains(p.tbSearch.Text)));
            }

            p.tbSearch.Text = string.Empty;
        }
        
        [Command] 
        public void ConfirmButton(AccountManageView p)
        {
            if (!string.IsNullOrEmpty(p.tbUsername.Text) && !string.IsNullOrWhiteSpace(p.tbUsername.Text))
            {
                if (p.tbUsername.Text.Contains(" "))
                {
                    MyMessageBox.Show("Tài khoản không được chứa khoảng trắng", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                if (string.IsNullOrEmpty(p.cbTypeAccount.Text))
                {
                    MyMessageBox.Show("Hãy chọn loại tài khoản", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                if (DataProvider.Instance.DB.Accounts.Where((x) => x.Username == p.tbUsername.Text).Count() > 0)
                {
                    if (p.tbUsername.IsEnabled == false)
                    {
                        var account = DataProvider.Instance.DB.Accounts.Where(x => x.Username == p.tbUsername.Text).SingleOrDefault();
                        account.IdTypeAccount = DataProvider.Instance.DB.TypeAccounts.Where(x => x.DisplayName == p.cbTypeAccount.Text).SingleOrDefault().IdTypeAccount;
                        DataProvider.Instance.DB.SaveChanges();

                        CancelAdjustment(p);
                        p.DataContext = new AccountManageViewModel();

                        MyMessageBox.Show("Sửa tài khoản thành công", "Thông báo", MessageBoxButton.OK);

                        return;
                    }
                    else
                    {
                        MyMessageBox.Show("Tài khoản đã tồn tại", "Thông báo", MessageBoxButton.OK);
                        return;
                    }
                }

                int idTypeAccount = ListTypeAccount.Where(x => x.DisplayName == p.cbTypeAccount.Text).SingleOrDefault().IdTypeAccount;

                Account newAccount = new Account() {
                    Username = p.tbUsername.Text,
                    Password = Utility.Encryption.EncryptPassword("1"),
                    IdTypeAccount = idTypeAccount,
                };

                DataProvider.Instance.DB.Accounts.Add(newAccount);
                DataProvider.Instance.DB.SaveChanges();

                p.tbUsername.Text = "";
                p.cbTypeAccount.Text = "";

                MyMessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButton.OK);

                CancelAdjustment(p);
                p.DataContext = new AccountManageViewModel();
            }
            else
            {
                MyMessageBox.Show("Tài khoản không được trống", "Thông báo", MessageBoxButton.OK);
                return;
            }
        }

        [Command] 
        public void ResetPassword()
        {
            try
            {
                if (SelectedAccount != null)
                {
                    Account account = DataProvider.Instance.DB.Accounts.Where(x => x.IdAccount == SelectedAccount.IdAccount).SingleOrDefault();
                    account.Password = Utility.Encryption.EncryptPassword("1");
                    DataProvider.Instance.DB.SaveChanges();

                    MyMessageBox.Show("Đặt lại mật khẩu là 1", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch { return; }
        }

        [Command]
        public void EditAccount(AccountManageView uc)
        {
            try
            {
                if (SelectedAccount != null)
                {
                    if (SelectedAccount.TypeAccount == "Admin")
                    {
                        MyMessageBox.Show("Không thể sửa khoản Admin", "Thông báo", MessageBoxButton.OK);
                        return;
                    }

                    uc.gridMain.IsEnabled = false;
                    uc.gridButtonAdjust.IsEnabled = false;
                    uc.gridButtonResetPassword.IsEnabled = false;
                    uc.gridDetail.IsEnabled = true;
                    uc.tbUsername.IsEnabled = false;

                    uc.tbUsername.Text = SelectedAccount.Username;
                    uc.cbTypeAccount.SelectedValue = DataProvider.Instance.DB.TypeAccounts.Where(x => x.IdTypeAccount == SelectedAccount.IdTypeAccount).SingleOrDefault().IdTypeAccount;
                }
            }
            catch { return; }
        }

        [Command]
        public void DeleteAccount()
        {
            try
            {
                if (SelectedAccount != null)
                {
                    if (SelectedAccount.TypeAccount == "Admin")
                    {
                        MyMessageBox.Show("Không thể xóa tài khoản Admin", "Thông báo", MessageBoxButton.OK);
                        return;
                    }

                    if (MyMessageBox.Show("Bạn có chắc chắn xóa tài khoản này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var beenDeleted = DataProvider.Instance.DB.Accounts.Where((x) => x.IdAccount == SelectedAccount.IdAccount).SingleOrDefault();
                        DataProvider.Instance.DB.Accounts.Remove(beenDeleted);
                        DataProvider.Instance.DB.SaveChanges();
                        MyMessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK);

                        LoadDB();
                    }
                }

                SelectedAccount = null;
            }
            catch { return; }
        }

        [Command]
        public void AddAccount(AccountManageView p)
        {
            try
            {
                p.gridMain.IsEnabled = false;
                p.gridButtonAdjust.IsEnabled = false;
                p.gridButtonResetPassword.IsEnabled = false;
                p.gridDetail.IsEnabled = true;
            }
            catch { return; }
        }
        
        [Command]
        public void CancelAdjustment(AccountManageView p)
        {
            try
            {
                p.tbUsername.Text = "";
                p.cbTypeAccount.Text = "";
                p.gridMain.IsEnabled = true;
                p.gridButtonAdjust.IsEnabled = true;
                p.gridButtonResetPassword.IsEnabled = true;
                p.gridDetail.IsEnabled = false;
            }
            catch { return; }
        }
    }
}
