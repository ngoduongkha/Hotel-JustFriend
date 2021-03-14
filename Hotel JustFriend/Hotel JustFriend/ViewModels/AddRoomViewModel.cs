using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class AddRoomViewModel : ViewModelBase
    {
        private ObservableCollection<string> _ListRoomName;
        private ObservableCollection<string> _ListRoomType;
        private int _RoomFloor;
        private int _RoomNumber;
        private string _DisplayName;
        private string _RoomType;
        private decimal _RoomPrice;
        private string _RoomNote;

        public ObservableCollection<string> ListRoomName { get => _ListRoomName; set => _ListRoomName = value; }
        public ObservableCollection<string> ListRoomType { get => _ListRoomType; set => _ListRoomType = value; }
        public int RoomFloor { get => _RoomFloor; set { _RoomFloor = value; RaisePropertyChanged(); } }
        public int RoomNumber { get => _RoomNumber; set { _RoomNumber = value; RaisePropertyChanged(); } }
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; RaisePropertyChanged(); } }
        public string RoomType { get => _RoomType; set { _RoomType = value; RaisePropertyChanged(); } }
        public decimal RoomPrice { get => _RoomPrice; set { _RoomPrice = value; RaisePropertyChanged(); } }
        public string RoomNote { get => _RoomNote; set { _RoomNote = value; RaisePropertyChanged(); } }

        public AddRoomViewModel()
        {
            DbSet<Room> data = DataProvider.Instance.DB.Rooms;
            ListRoomName = new ObservableCollection<string>(data.Where(x => x.isDelete == false).Select(x => x.displayName));
            ListRoomType = new ObservableCollection<string>(data.Select(x => x.type).Distinct());
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region Command
        [Command]
        public void Add()
        {
            try
            {
                if (ListRoomName.Contains(DisplayName))
                {
                    return;
                }
                Room newRoom = new Room() { displayName = DisplayName, type = RoomType, price = RoomPrice, note = RoomNote };
                DataProvider.Instance.DB.Rooms.Add(newRoom);
                DataProvider.Instance.DB.SaveChanges();
            }
            catch { return; }
        }

        [Command]
        public void SeparateThousands(TextBox obj)
        {
            try
            {
                decimal money = Converter.Instance.ConvertCurrencyToDecimal(obj.Text);
                    obj.Text = Converter.Instance.ConvertDecimalToCurrency(money);
                    obj.Select(obj.Text.Length, 0);
               
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
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        #endregion
    }
}