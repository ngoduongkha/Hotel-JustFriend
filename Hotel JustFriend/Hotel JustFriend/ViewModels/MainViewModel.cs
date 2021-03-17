using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class MainViewModel : ViewModelBase
    {
        #region Command
        [Command]
        public void AddRoom(Window p)
        {
            try
            {
                AddRoomWindow addRoom = new AddRoomWindow();
                addRoom.ShowDialog();
            }
            catch { return; }
        }
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
                p.IsEnabled = false;
            }
            catch { return; }
        }
        [Command]
        public void CloseMenu(Border p)
        {
            try
            {
                p.IsEnabled = true;
            }
            catch { return; }
        }
        [Command]
        public void OpenTabHome(MainWindow p)
        {
            try
            {
                p.gridMain.Children.Clear();
                UserControlHome uc = new UserControlHome();
                (p.FindResource("CloseMenu") as Storyboard).Begin();
                p.borderMain.IsEnabled = true;
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
                UserControlRoomManage uc = new UserControlRoomManage();
                (p.FindResource("CloseMenu") as Storyboard).Begin();
                p.borderMain.IsEnabled = true;
                p.gridMain.Children.Add(uc);
            }
            catch { return; }
        }
        #endregion
    }
}