﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class RoomManageViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _ListRoom;
        private ObservableCollection<string> _ListRoomType;
        private ObservableCollection<string> _ListRoomStatus;
        private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomStatus { get => _ListRoomStatus; set { _ListRoomStatus = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public RoomManageViewModel()
        {
            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));
                ListRoomStatus = new ObservableCollection<string>(ListRoom.Select(x => x.status).Distinct());
                ListRoomType = new ObservableCollection<string>(ListRoom.Select(x => x.type).Distinct());
            }
            catch { return; }
        }

        [Command]
        public void AddRoom()
        {
            try
            {
                RoomDetailView addRoom = new RoomDetailView();
                addRoom.ShowDialog();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void DeleteRoom()
        {
            try
            {
                if (SelectedRoom == null)
                    MyMessageBox.Show("Không có gì để xóa cả!", "Thông báo", System.Windows.MessageBoxButton.OK);

                DataProvider.Instance.DB.Rooms.Where(x => x.idRoom == SelectedRoom.idRoom).SingleOrDefault().isDelete = true;
                DataProvider.Instance.DB.SaveChanges();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void EditRoom()
        {
            try
            {
                if (SelectedRoom == null)
                    MyMessageBox.Show("Không có gì để sửa cả!", "Thông báo", System.Windows.MessageBoxButton.OK);

                RoomDetailView editRoom = new RoomDetailView();
                editRoom.txtFloor.Text = SelectedRoom.floor.ToString();
                editRoom.txtNumber.Text = SelectedRoom.number.ToString();
                editRoom.txtPrice.Text = SelectedRoom.price.ToString("C");
                editRoom.ShowDialog();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void RoomFilter(RoomManageView p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));

                if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.type == p.txtFilterType.Text));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.status == p.txtFilterStatus.Text));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.type == p.txtFilterType.Text && x.status == p.txtFilterStatus.Text));
                }

                p.txtFilterStatus.Text = string.Empty;
                p.txtFilterType.Text = string.Empty;
            }
            catch { return; }
        }
    }
}
