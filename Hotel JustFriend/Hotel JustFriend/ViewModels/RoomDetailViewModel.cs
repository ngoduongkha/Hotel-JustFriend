﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class RoomDetailViewModel : ViewModelBase
    {
        private ObservableCollection<string> _ListRoomName;
        private ObservableCollection<TypeRoom> _ListRoomType;
        private TypeRoom _SelectedRoomType;
        private byte _RoomFloor;
        private byte _RoomNumber;
        private string _DisplayName;
        private string _RoomStatus;
        private string _RoomType;
        private decimal _RoomPrice;
        private string _RoomNote;

        public ObservableCollection<string> ListRoomName { get => _ListRoomName; set => _ListRoomName = value; }
        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set => _ListRoomType = value; }
        public byte RoomFloor { get => _RoomFloor; set => _RoomFloor = value; }
        public byte RoomNumber { get => _RoomNumber; set => _RoomNumber = value; }
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; RaisePropertyChanged(); } }
        public string RoomStatus { get => _RoomStatus; set => _RoomStatus = value; }
        public string RoomType { get => _RoomType; set => _RoomType = value; }
        public decimal RoomPrice { get => _RoomPrice; set => _RoomPrice = value; }
        public string RoomNote { get => _RoomNote; set => _RoomNote = value; }
        public TypeRoom SelectedRoomType { get => _SelectedRoomType; set { _SelectedRoomType = value; RaisePropertyChanged(); } }

        public RoomDetailViewModel()
        {
            ListRoomName = new ObservableCollection<string>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).Select(x => x.displayName));
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
        }

        #region Command
        [Command]
        public void Save(Window p)
        {
            try
            {
                if (ListRoomName.Contains(DisplayName))
                {
                    MyMessageBox.Show("Phòng đã tồn tại", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                Room newRoom = new Room()
                {
                    floor = RoomFloor,
                    number = RoomNumber,
                    displayName = DisplayName,
                    status = RoomStatus,
                    idType = SelectedRoomType.idType,
                    note = RoomNote,
                    isDelete = false,
                };

                DataProvider.Instance.DB.Rooms.Add(newRoom);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                p.Close();
            }
            catch { return; }
        }

        [Command]
        public void GenerateDisplayName()
        {
            if (RoomNumber < 10)
                DisplayName = $"Phòng {RoomFloor}0{RoomNumber}";
            else
                DisplayName = $"Phòng {RoomFloor}{RoomNumber}";
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