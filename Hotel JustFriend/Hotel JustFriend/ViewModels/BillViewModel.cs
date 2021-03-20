using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class BillViewModel : ViewModelBase
    {
        private Bill _Bill;
        private ObservableCollection<Billinfo> _ListBillInfo;

        public Bill Bill { get => _Bill; set { _Bill = value; RaisePropertyChanged(); } }

        public ObservableCollection<Billinfo> ListBillInfo { get => _ListBillInfo; set => _ListBillInfo = value; }

        public BillViewModel()
        {

        }

        [Command]
        public void LoadBill()
        {
            Bill = DataProvider.Instance.DB.Bills.Where(x => x.idBill == "1").SingleOrDefault();
            ListBillInfo = new ObservableCollection<Billinfo>(DataProvider.Instance.DB.Billinfoes.Where(x => x.idBill == Bill.idBill));
        }
    }
}
