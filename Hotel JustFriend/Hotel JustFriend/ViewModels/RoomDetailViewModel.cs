using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using Hotel_JustFriend.Views;
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
        private ObservableCollection<string> _ListNameRoomType;
        private ObservableCollection<Room> _ListRoom;
        private byte _RoomFloor;
        private byte _RoomNumber;
        private string _DisplayName;
        private string _RoomStatus;
        private string _RoomType;
        private string _RoomPrice;
        private string _RoomNote;
        private int _idRoom;
        public ObservableCollection<string> ListRoomName { get => _ListRoomName; set => _ListRoomName = value; }
        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertyChanged(); } }
        public byte RoomFloor { get => _RoomFloor; set => _RoomFloor = value; }
        public byte RoomNumber { get => _RoomNumber; set => _RoomNumber = value; }
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; RaisePropertyChanged(); } }
        public string RoomStatus { get => _RoomStatus; set => _RoomStatus = value; }
        public string RoomType { get => _RoomType; set => _RoomType = value; }
        public string RoomPrice { get => _RoomPrice; set { _RoomPrice = value; RaisePropertyChanged(); } }
        public string RoomNote { get => _RoomNote; set => _RoomNote = value; }
        public ObservableCollection<string> ListNameRoomType { get => _ListNameRoomType; set => _ListNameRoomType = value; }
        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }

        public int IdRoom { get => _idRoom; set => _idRoom = value; }

        public RoomDetailViewModel()
        {
            System.Data.Entity.DbSet<Room> data = DataProvider.Instance.DB.Rooms;
            ListRoomName = new ObservableCollection<string>(data.Where(x => x.isDelete == false).Select(x => x.displayName));
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p)=>p.isDelete==false));
            ListNameRoomType = new ObservableCollection<string>(DataProvider.Instance.DB.TypeRooms.Select(x => x.fullname));            
        }
        #region Command
        [Command]
        public void Save(RoomDetailView p)
        {
            try
            {
                int d = -1;
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms);
                for (int i=0;i<ListRoom.Count;i++)
                {
                    if (d < ListRoom[i].idRoom) d = ListRoom[i].idRoom;
                }
                IdRoom = d + 1;
                if (ListRoomName.Contains(DisplayName))
                {
                    MyMessageBox.Show("Phòng đã tồn tại", "Thông báo", MessageBoxButton.OK);
                    return;
                }
                Room newRoom = new Room()
                {
                    idRoom = IdRoom,
                    floor = RoomFloor,
                    displayName = DisplayName,
                    status = false,
                    idType = int.Parse(p.txtType.SelectedValue.ToString()),
                    note = RoomNote,
                    isDelete=false
                };

                DataProvider.Instance.DB.Rooms.Add(newRoom);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                Close(p);
            }
            catch { return; }
        }
        string get_string(string a)
        {
            int l = a.Length;
            string rs = "";
            for (int i=0;i<l;i++)
            {
                if (a[i] == '.') break;
                if (a[i] >= '0' && a[i] <= '9') rs = rs + a[i];
            }
            return (rs);
        }
        [Command]
        public void GeneratePrice(RoomDetailView p)
        {
            string id = p.txtType.SelectedValue.ToString();
            for (int i = 0; i<ListRoomType.Count;i++)
            {
                if (ListRoomType[i].idType.ToString() == id)
                {
                    RoomPrice = int.Parse(get_string(ListRoomType[i].price.ToString())).ToString("#,##0");
                    break;
                }
            }
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