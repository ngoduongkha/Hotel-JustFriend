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
        public int id { get; set; }
        public string username { get; set; }
        public string typeAccount { get; set; }

        public AccountAndType(int id, string username, string typeAccount)
        {
            this.id = id;
            this.username = username;
            this.typeAccount = typeAccount;
        }
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
            ListAccountWithType = new ObservableCollection<AccountAndType>(
                _ListAccount.Join(_ListTypeAccount, (Account) => Account.idTypeAccount, (TypeAccount) => TypeAccount.idTypeAccount,
                (Account, TypeAccount) => new AccountAndType(Account.idAccount, Account.username, TypeAccount.displayname)));
        }

        public AccountManageViewModel()
        {
            ListAccount = new ObservableCollection<Account>(DataProvider.Instance.DB.Accounts);
            ListTypeAccount = new ObservableCollection<TypeAccount>(DataProvider.Instance.DB.TypeAccounts);
            ListTypeAccountCanAdd = new ObservableCollection<TypeAccount>(DataProvider.Instance.DB.TypeAccounts.Where((x) => x.displayname != "Admin"));

            LoadDB();
        }

        [Command]
        public void AccountTypeFilter(AccountManageView p)
        {
            LoadDB();

            if (!string.IsNullOrEmpty(p.cbFilterType.Text))
            {
                ListAccountWithType = new ObservableCollection<AccountAndType>(ListAccountWithType.Where((x) => x.typeAccount == p.cbFilterType.Text));
            }

            p.cbFilterType.Text = string.Empty;
        }

        [Command]
        public void Search(AccountManageView p)
        {
            LoadDB();

            if (!string.IsNullOrEmpty(p.tbSearch.Text) && !string.IsNullOrWhiteSpace(p.tbSearch.Text))
            {
                ListAccountWithType = new ObservableCollection<AccountAndType>(ListAccountWithType.Where((x) => x.username.Contains(p.tbSearch.Text)));
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

                if (DataProvider.Instance.DB.Accounts.Where((x) => x.username == p.tbUsername.Text).Count() > 0)
                {
                    MyMessageBox.Show("Tài khoản đã tồn tại", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                int idTypeAccount = ListTypeAccount.Where(x => x.displayname == p.cbTypeAccount.Text).SingleOrDefault().idTypeAccount;

                Account newAccount = new Account();
                newAccount.username = p.tbUsername.Text;
                newAccount.password = Utility.Encryption.EncryptPassword("1");
                newAccount.idTypeAccount = idTypeAccount;
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
        public void DeleteAccount()
        {
            if (SelectedAccount != null)
            {
                if (SelectedAccount.typeAccount == "Admin")
                {
                    MyMessageBox.Show("Không thể xóa tài khoản Admin", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                if (MyMessageBox.Show("Bạn có chắc chắn xóa tài khoản này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var beenDeleted = DataProvider.Instance.DB.Accounts.Where((x) => x.idAccount == SelectedAccount.id).FirstOrDefault();
                    DataProvider.Instance.DB.Accounts.Remove(beenDeleted);
                    DataProvider.Instance.DB.SaveChanges();
                    MyMessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK);
                    
                    LoadDB();
                }
            }

            SelectedAccount = null;
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
        public void EditAccount(AccountManageView p)
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
