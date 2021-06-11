using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using Hotel_JustFriend.UserControls;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class RoomManageViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _ListRoom;
        private CollectionView _View;
        private ObservableCollection<TypeRoom> _ListRoomType;
        private ObservableCollection<string> _ListNameRoomType;
        private ObservableCollection<int> _ListRoomStatus;
        private CollectionView _Selected;
        private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public ObservableCollection<int> ListRoomStatus { get => _ListRoomStatus; set { _ListRoomStatus = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListNameRoomType { get => _ListNameRoomType; set { _ListNameRoomType = value; RaisePropertyChanged(); } }

        public CollectionView View { get => _View; set { _View = value; RaisePropertyChanged(); } }

        public CollectionView Selected { get => _Selected; set { _Selected = value; RaisePropertiesChanged(); } }

        public RoomManageViewModel()
        {

        }
        public void ham_da_nang(RoomManageView p)
        {
            p.stp_ListRoom.Children.Clear();
            for (int i = 0; i < ListRoom.Count; i++)
            {
                RoomUC a = new RoomUC();
                a.floor.Text = ListRoom[i].floor.ToString();
                a.id.Text = ListRoom[i].idRoom.ToString();
                a.displayname.Text = ListRoom[i].displayName.ToString();
                for (int j = 0; j < ListRoomType.Count; j++)
                {
                    if (ListRoomType[j].idType == ListRoom[i].idType)
                    {
                        a.Type.Text = ListRoomType[j].fullname;
                        a.price.Text = ListRoomType[j].price.ToString();
                        break;
                    }
                }
                if (ListRoom[i].status > 0) a.status.Text = "Chưa sẵn sàng";
                else a.status.Text = "Sẵn sàng";
                a.note.Text = ListRoom[i].note.ToString();
                p.stp_ListRoom.Children.Add(a);
            }
        }
        [Command]
        public void LoadDB(RoomManageView p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                    .Where(x => x.isDelete == false)
                    .OrderBy(x => x.floor)
                    );
                ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
                for (int i=0;i<ListRoom.Count;i++)
                {
                    RoomUC a = new RoomUC();
                    a.floor.Text = ListRoom[i].floor.ToString();
                    a.id.Text = ListRoom[i].idRoom.ToString();
                    a.displayname.Text = ListRoom[i].displayName.ToString();
                    for (int j=0;j<ListRoomType.Count;j++)
                    {
                        if (ListRoomType[j].idType==ListRoom[i].idType)
                        {
                            a.Type.Text = ListRoomType[j].fullname;
                            a.price.Text = ListRoomType[j].price.ToString();
                            break;
                        }
                    }
                    if (ListRoom[i].status > 0) a.status.Text = "Chưa sẵn sàng";
                    else a.status.Text = "Sẵn sàng";
                    a.note.Text = ListRoom[i].note.ToString();
                    p.stp_ListRoom.Children.Add(a);
                }
            }
            catch 
            { 
                return; 
            }
        }

        [Command]
        public void AddRoom(RoomManageView p)
        {
            try
            {
                RoomDetailView addRoom = new RoomDetailView();
                addRoom.ShowDialog();
                p.stp_ListRoom.Children.Clear();
                LoadDB(p);
           }
            catch { return; }
        }

        [Command]
        public void DeleteRoom(RoomUC p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                    .Where(x => x.isDelete == false)
                    );
                int k = int.Parse(p.id.Text);
                Room result = (from u in ListRoom
                               where u.idRoom == k
                               select u).Single();
                result.isDelete = true;
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Xóa thành công!", "Thông báo", System.Windows.MessageBoxButton.OK);
                ((StackPanel)p.Parent).Children.Remove(p);
            }
            catch { return; }
        }

        [Command]
        public void FixRoom()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    MyMessageBox.Show("Không có gì để sửa chữa!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (SelectedRoom.status != 0)
                {
                    MyMessageBox.Show("Phòng không hỏng!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                //DataProvider.Instance.DB.Rooms.Where(x => x.idRoom == SelectedRoom.idRoom).SingleOrDefault().status = "Sẵn sàng";
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Sửa chữa thành công!", "Thông báo", System.Windows.MessageBoxButton.OK);

                //LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void RoomFilter(RoomManageView p)
        {
           // try
            //{
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                    .Where(x => x.isDelete == false)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));
            ham_da_nang(p);
                if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    int k = int.Parse(p.txtFilterType.SelectedValue.ToString());
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.idType == k)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.displayName)) ;
                    ham_da_nang(p);
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    int id = 0;
                    if (p.txtFilterStatus.Text == "Sẵn sàng") id = 0;
                    else id = 1;
                    if (id == 0)
                    {
                        ListRoom = new ObservableCollection<Room>(ListRoom
                            .Where(x => x.status == 0)
                            .OrderBy(x => x.floor)
                            .ThenBy(x => x.displayName));
                        ham_da_nang(p);
                    }
                    else
                    {
                        ListRoom = new ObservableCollection<Room>(ListRoom
                          .Where(x => x.status > 0)
                          .OrderBy(x => x.floor)
                          .ThenBy(x => x.displayName));
                        ham_da_nang(p);
                    }
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    int id = 0;
                    if (p.txtFilterStatus.Text == "Sẵn sàng") id = 0;
                    else id = 1;
                    int k = int.Parse(p.txtFilterType.SelectedValue.ToString());
                    if (id == 0)
                    {
                        ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.idType == k && x.status == id)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.displayName));
                        ham_da_nang(p);
                    }
                    else
                    {
                        ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.idType == k && x.status > 0)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.displayName));
                        ham_da_nang(p);
                    }
                
                }
                p.txtFilterStatus.Text = string.Empty;
                p.txtFilterType.Text = string.Empty;
        //    }
         //   catch { return; }
        }

        [Command]
        public void SearchRoom(RoomManageView p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                    .Where(x => x.isDelete == false)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));

                if (string.IsNullOrEmpty(p.txtSearch.Text))
                    return;

                ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.displayName.Contains(p.txtSearch.Text))
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName)
                    .ToList());
                ham_da_nang(p);
                p.txtSearch.Text = string.Empty;
            }
            catch { return; }
        }
    }
}
