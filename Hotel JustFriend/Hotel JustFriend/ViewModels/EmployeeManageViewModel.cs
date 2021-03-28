using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
namespace Hotel_JustFriend.Views
{
    [POCOViewModel]
    public class EmployeeManageViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _ListEmployee;
        private Employee _SelectEmployee;
        private ObservableCollection<EmployeeRole> _ListEmployeeRole;
        public ObservableCollection<Employee> ListEmployee { get => _ListEmployee; set => _ListEmployee = value; }
        public Employee SelectEmployee { get => _SelectEmployee; set => _SelectEmployee = value; }
        public ObservableCollection<EmployeeRole> ListEmployeeRole { get => _ListEmployeeRole; set => _ListEmployeeRole = value; }

        private void LoadDB()
        {
            try
            {
                ListEmployee = new ObservableCollection<Employee>(DataProvider.Instance.DB.Employees.Where(x => x.isDelete == false));
            }
            catch { return; }
        }
        public EmployeeManageViewModel()
        {
            LoadDB();
        }
        [Command]
        public void DeleteEmployee()
        {
            try
            {
                DataProvider.Instance.DB.Employees.Where(x => x.idEmployee == SelectEmployee.idEmployee).SingleOrDefault().isDelete = true;
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Xóa thành công!", "Thông báo", System.Windows.MessageBoxButton.OK);
                LoadDB();
            }
            catch
            {
                return;
            }
        }
        [Command]
        public void AddEmployee()
        {
            try
            {
                EmployeeDetailView addemployee = new EmployeeDetailView();
                addemployee.ShowDialog();
                LoadDB();
            }
            catch { return; }
        }

    }
}