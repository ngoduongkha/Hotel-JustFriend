using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_JustFriend.Views;
using System.Diagnostics;
using System;
using Hotel_JustFriend.UserControls;
using Hotel_JustFriend.Template;
using LiveCharts;
using LiveCharts.Wpf;

namespace Hotel_JustFriend.ViewModels
{
    class ReportViewModel : ViewModelBase
    {
        private ObservableCollection<TypeRoom> listRoomType;
        private ObservableCollection<Room> listRoom;
        private ObservableCollection<Bill> listBill;
        private ObservableCollection<BillInfo> listBillInfo;
        private ObservableCollection<int> listYears;
        private SeriesCollection pieSerieCollection;

        public SeriesCollection PieSerieCollection { get => pieSerieCollection; set { pieSerieCollection = value; RaisePropertiesChanged(); } }
        public ObservableCollection<int> ListYears { get => listYears; set => listYears = value; }

        public ReportViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            listRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where(p => p.IsDelete == false));
            listBillInfo = new ObservableCollection<BillInfo>(DataProvider.Instance.DB.BillInfoes);
            listRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms);
            ListYears = new ObservableCollection<int>(DataProvider.Instance.DB.Bills.Select(x => x.Date.Year).Distinct());
        }

        [Command]
        public void ShowReport(ReportView uc)
        {
            ReportTemplateUC reportTemplate = new ReportTemplateUC();
            reportTemplate.month.Text = uc.cbboxMonths.Text;
            reportTemplate.year.Text = uc.cbboxYears.Text;
            decimal total = 0;
            int year = int.Parse(uc.cbboxYears.Text);
            int month = uc.cbboxMonths.SelectedIndex + 1;
            if (month == 0) listBill = new ObservableCollection<Bill>(DataProvider.Instance.DB.Bills.Where(p => p.Date.Year == year));
            else listBill = new ObservableCollection<Bill>(DataProvider.Instance.DB.Bills.Where(p => p.Date.Year == year && p.Date.Month == month));
            if (listBill.Count > 0)
            {
                PieSerieCollection = new SeriesCollection();
                for (int i = 0; i < listRoomType.Count; i++)
                {
                    double percent = 0;
                    decimal revenue = 0;
                    int idtype = listRoomType[i].IdTypeRoom;
                    for (int j = 0; j < listBillInfo.Count; j++)
                    {
                        int idd = listBillInfo[j].IdBill;
                        Bill k = listBill.Where(p => p.IdBill == idd).FirstOrDefault();
                        if (k != null)
                        {
                            Room a = listRoom.Where(p => p.IdRoom == listBillInfo[j].IdRoom).FirstOrDefault();
                            if (a.IdTypeRoom == idtype)
                            {
                                percent = percent + 1;
                                revenue = revenue + (decimal)k.TotalMoney;
                            }
                        }
                    }
                    if (listBill != null) percent = percent / (listBill.Count);
                    total = total + revenue;
                    ChartValues<decimal> chartValue = new ChartValues<decimal>();
                    chartValue.Add(revenue);
                    PieSeries newPie = new PieSeries
                    {
                        Title = listRoomType[i].DisplayName,
                        Values = chartValue,
                        DataLabels = true,
                        FontSize = 15,
                    };
                    PieSerieCollection.Add(newPie);
                    ReportUC c = new ReportUC();
                    c.STT.Text = (i + 1).ToString();
                    c.displayname.Text = listRoomType[i].DisplayName;
                    c.revenue.Text = string.Format("{0:C}", revenue);
                    c.percent.Text = percent.ToString("#0.##%");

                    reportTemplate.stp.Children.Add(c);
                }
                reportTemplate.totalmoney.Text = string.Format("{0:C}", total);
                uc.gridReportTemplate.Children.Clear();
                uc.gridReportTemplate.Children.Add(reportTemplate);
            }
            else { MyMessageBox.Show("Dữ liệu không tồn tại, mời nhập lại thời gian", "Nhắc nhở", MessageBoxButton.OK); }
        }

        [Command]
        public void ExportReport(ReportTemplateUC uc)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    uc.btnExport.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(uc.gridExport, "Bill");
                }
            }
            finally
            {
                uc.btnExport.Visibility = Visibility.Visible;
            }
        }
    }

}
