using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Hotel_JustFriend.Utility;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core.Native;

namespace Hotel_JustFriend.ViewModels
{
    class EmployeeDetailViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _ListEmployee;
        private int _IdEmployeeRole;
        private int _IdEmployee;
        private string _FullName;
        private string _IdCard;
        private string _Gender;
        private string _Phone;
        private DateTime _DateOfBirth;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private byte[] _Image;
        private bool isDelete;
        public ObservableCollection<Employee> ListEmployee { get => _ListEmployee; set => _ListEmployee = value; }
        public int IdEmployee { get => _IdEmployee; set => _IdEmployee = value; }
        public string FullName { get => _FullName; set => _FullName = value; }
        public string IdCard { get => _IdCard; set => _IdCard = value; }
        public string Gender { get => _Gender; set => _Gender = value; }
        public string Phone { get => _Phone; set => _Phone = value; }
        public DateTime DateOfBirth { get => _DateOfBirth; set => _DateOfBirth = value; }
        public DateTime StartDate { get => _StartDate; set => _StartDate = value; }
        public DateTime EndDate { get => _EndDate; set => _EndDate = value; }
        public byte[] Image { get => _Image; set { _Image = value; RaisePropertyChanged(); } }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public int IdEmployeeRole { get => _IdEmployeeRole; set => _IdEmployeeRole = value; }

        [Command]
        public void SelectImage(EmployeeDetailView p)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));
                    p.imgField.Source = image;
                    Image = new Utility.ImageToByteConverter().Convert(image, null, null, null) as byte[];
                }
            }
            catch { return; }
        }
        [Command]
        public void View()
        {
            try
            {
                ListEmployee = new ObservableCollection<Employee>(DataProvider.Instance.DB.Employees.Where(x => x.isDelete == false));
            }
            catch
            {
                return;
            }
        }
        [Command]
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch
            {
                return;
            }
        }
        [Command]
        public void Save(Window p)
        {
            try
            {
                string a = "0";
                if (ListEmployee != null)
                {
                    for (int i = 0; i < ListEmployee.Count; i++)
                    {
                        string b = ListEmployee[i].idEmployee.ToString();
                        if (int.Parse(a) < int.Parse(b)) a = b;
                    }
                }
                IdEmployee = int.Parse(a) + 1;
                Employee newEmployee = new Employee() { idEmployeeRole = 2, idEmployee = IdEmployee, fullName = FullName, idCard = IdCard, gender = Gender, phone = Phone, dateOfBirth = DateOfBirth, startDate = StartDate, endDate = EndDate, image = Image, isDelete = false };
                DataProvider.Instance.DB.Employees.Add(newEmployee);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                p.Close();
            }
            catch
            {
                MyMessageBox.Show("Ngu chua", "Thông báo", MessageBoxButton.OK);
            }
        }
    }
}
