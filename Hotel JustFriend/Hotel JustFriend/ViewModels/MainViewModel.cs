using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class MainViewModel : ViewModelBase
    {
        private string _userName;

        public Visibility IsCollapsedTabHome { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabRoomManage { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabCustomerManage { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabAccountManage { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabReport { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabCustomizeParameters { get; set; } = Visibility.Visible;
        public Visibility IsCollapsedTabAccountInfo { get; set; } = Visibility.Visible;

        public MainViewModel(string userName)
        {
            _userName = userName;
            var account = DataProvider.Instance.DB.Accounts.Where(x => x.Username == userName).SingleOrDefault();

            switch (account.IdTypeAccount)
            {
                case 2:
                    IsCollapsedTabRoomManage = Visibility.Collapsed;
                    IsCollapsedTabCustomerManage = Visibility.Collapsed;
                    IsCollapsedTabAccountManage = Visibility.Collapsed;
                    IsCollapsedTabReport = Visibility.Collapsed;
                    IsCollapsedTabCustomizeParameters = Visibility.Collapsed;
                    break;
                case 3:
                    IsCollapsedTabRoomManage = Visibility.Collapsed;
                    IsCollapsedTabCustomerManage = Visibility.Collapsed;
                    IsCollapsedTabAccountManage = Visibility.Collapsed;
                    IsCollapsedTabCustomizeParameters = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        #region Command
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
        public void MaximizeWindow(Window p)
        {
            try
            {
                if (p.WindowState != WindowState.Maximized)
                {
                    p.WindowState = WindowState.Maximized;
                }
                else
                {
                    p.WindowState = WindowState.Normal;
                }
            }
            catch { return; }
        }

        [Command]
        public void MinimizeWindow(Window p)
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

        [Command]
        public void CloseWindow(Window p)
        {
            try
            {
                if (MyMessageBox.Show("Bạn muốn thoát khỏi chương trình?", "Nhắc nhở", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    p.Close();
                }
            }
            catch { return; }
        }

        [Command]
        public void OpenMenu(Border p)
        {
            try
            {
                p.Visibility = Visibility.Visible;
            }
            catch { return; }
        }

        [Command]
        public void CloseMenu(Border p)
        {
            try
            {
                p.Visibility = Visibility.Hidden;
            }
            catch { return; }
        }

        [Command]
        public void CloseMenuByClick(MainWindow p)
        {
            try
            {
                p.borderHide.Visibility = Visibility.Hidden;
                (p.FindResource("CloseMenu") as Storyboard).Begin();
            }
            catch { return; }
        }

        [Command]
        public void OpenTabHome(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                BusinessView uc = new BusinessView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }

        [Command]
        public void OpenTabRoomManage(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                RoomManageView uc = new RoomManageView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        [Command]
        public void OpenTabReport(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                ReportView uc = new ReportView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        [Command]
        public void OpenTabCustomizeParameters(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                CustomizeParametersView uc = new CustomizeParametersView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        [Command]
        public void OpenTabAccountInfo(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                AccountInfoView uc = new AccountInfoView(_userName);
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        [Command]
        public void OpenTabAccountManage(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                AccountManageView uc = new AccountManageView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        [Command]
        public void OpenTabCustomerManage(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                CustomerManageView uc = new CustomerManageView();
                CloseMenuByClick(p);
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        #endregion
    }
}