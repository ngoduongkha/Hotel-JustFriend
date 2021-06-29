using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_JustFriend.Template;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using Hotel_JustFriend.Models;

namespace Hotel_JustFriend.ViewModels
{
    public class CustomerInBill
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public int IdTypeCustomer { get; set; }
        public string TypeCustomer { get; set; }
        public double CoefficientsObtained { get; set; }
    }

    class BilltemplateViewModel : ViewModelBase
    {
        public ObservableCollection<CustomerInBill> ListCustomer { get; set; }
        public int TotalDay { get; set; }
        public string RoomName { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal TotalMoney { get; set; } = 0;
        public decimal Surcharge { get; set; } = 0;
        public decimal RealRevenue { get; set; } = 0;

        public BilltemplateViewModel(RentInvoice rentInvoice)
        {
            try
            {
                var room = DataProvider.Instance.DB.Rooms.Where(x => x.IsDelete == false).Where(x => x.IdRoom == rentInvoice.IdRoom).SingleOrDefault();
                var rentInfoInfo = DataProvider.Instance.DB.RentInvoiceInfoes.Where(x => x.IdRentInvoice == rentInvoice.IdRentInvoice);

                RoomName = room.DisplayName;
                RoomPrice = DataProvider.Instance.DB.TypeRooms.Where(x => x.IdTypeRoom == room.IdTypeRoom).SingleOrDefault().Price;
                TotalDay = DateTime.Now.Subtract(rentInvoice.Date).Days + 1;

                ListCustomer = new ObservableCollection<CustomerInBill>(
                    rentInfoInfo.Join(
                        DataProvider.Instance.DB.Customers,
                        RentInvoiceInfo => RentInvoiceInfo.IdCustomer,
                        Customer => Customer.IdCustomer,
                        (RentInvoiceInfo, Customer) => new CustomerInBill
                        {
                            FullName = Customer.FullName,
                            Address = Customer.Address,
                            IdTypeCustomer = Customer.IdTypeCustomer,
                        }));

                ListCustomer = new ObservableCollection<CustomerInBill>(
                    ListCustomer.Join(
                        DataProvider.Instance.DB.TypeCustomers.Where(x => x.IsDelete == false),
                        CustomerInBill => CustomerInBill.IdTypeCustomer,
                        TypeCustomer => TypeCustomer.IdTypeCustomer,
                        (CustomerInBill, TypeCustomer) => new CustomerInBill
                        {
                            FullName = CustomerInBill.FullName,
                            Address = CustomerInBill.Address,
                            IdTypeCustomer = CustomerInBill.IdTypeCustomer,
                            TypeCustomer = TypeCustomer.DisplayName,
                            CoefficientsObtained = TypeCustomer.CoefficientsObtained,
                        })); ;

                var maxCustomer = DataProvider.Instance.DB.Constants.SingleOrDefault().MaxCustomer;
                var maxCoefficientsObtained = DataProvider.Instance.DB.TypeCustomers.OrderByDescending(x => x.CoefficientsObtained).FirstOrDefault().CoefficientsObtained;

                TotalMoney = RoomPrice * TotalDay * (decimal)maxCoefficientsObtained;

                if (ListCustomer.Count >= 3)
                {
                    Surcharge = TotalMoney * (decimal)(DataProvider.Instance.DB.Constants.SingleOrDefault().Percent / 100) * (ListCustomer.Count - 2);
                }

                RealRevenue = TotalMoney + Surcharge;

                Bill bill = new Bill()
                {
                    Date = rentInvoice.Date,
                    TotalMoney = RealRevenue
                };

                DataProvider.Instance.DB.Bills.Add(bill);

                BillInfo billInfo = new BillInfo()
                {
                    NumberDay = TotalDay,
                    Price = RealRevenue,
                    IdBill = bill.IdBill,
                    IdRoom = rentInvoice.IdRoom,
                };

                DataProvider.Instance.DB.BillInfoes.Add(billInfo);

                DataProvider.Instance.DB.SaveChanges();

            }
            catch { return; }
        }

        [Command]
        public void PrintBill(BillTemplate parameter)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    parameter.btnPrint.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(parameter.grdPrint, "Bill");
                }
            }
            finally
            {
                parameter.btnPrint.Visibility = Visibility.Visible;
            }
        }
    }
}
