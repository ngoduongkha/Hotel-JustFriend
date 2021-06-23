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
        private ObservableCollection<TypeRoom> _ListRoomType;
        private ObservableCollection<Room> _ListRoom;
        private ObservableCollection<Bill> _ListBill;
        private ObservableCollection<BillInfo> _ListBillInfo;
        private string _month;
        private string _year;
        private SeriesCollection pieSerieCollection;

        public string Year { get => _year; set => _year = value; }
        public string Month { get => _month; set => _month = value; }
        public SeriesCollection PieSerieCollection { get => pieSerieCollection; set { pieSerieCollection = value; RaisePropertiesChanged(); } }

        [Command]
        public void report(Grid grid)
        {
            ReportTemplateUC aa = new ReportTemplateUC();
            aa.month.Text = Month.ToString();
            aa.year.Text = Year.ToString();
            decimal tt = 0;
            _ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where(p => p.isDelete == false));
            _ListBillInfo = new ObservableCollection<BillInfo>(DataProvider.Instance.DB.BillInfoes);
            _ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms);
            int y = int.Parse(Year);
            int m = 0;
            if (Month != "") m = int.Parse(Month);
            if (Month == "") _ListBill = new ObservableCollection<Bill>(DataProvider.Instance.DB.Bills.Where(p => p.date.Value.Year == y));
            else _ListBill = new ObservableCollection<Bill>(DataProvider.Instance.DB.Bills.Where(p => p.date.Value.Year == y && p.date.Value.Month == m));
            PieSerieCollection = new SeriesCollection();
            for (int i = 0; i < _ListRoomType.Count; i++)
            {
                double phantram = 0;
                decimal doanhthu = 0;
                int idtype = _ListRoomType[i].idType;
                for (int j = 0; j < _ListBillInfo.Count; j++)
                {
                    int idd = _ListBillInfo[j].idBill;
                    Bill k = _ListBill.Where(p => p.idBill == idd).FirstOrDefault();
                    if (k != null)
                    {
                        Room a = _ListRoom.Where(p => p.idRoom == _ListBillInfo[j].idRoom).FirstOrDefault();
                        if (a.idType == idtype)
                        {
                            phantram = phantram + 1;
                            doanhthu = doanhthu + (decimal)k.totalMoney;
                        }
                    }
                }
                if (_ListBill != null) phantram = phantram / (_ListBill.Count);
                tt = tt + doanhthu;
                ChartValues<decimal> chartValue = new ChartValues<decimal>();
                chartValue.Add(doanhthu);
                PieSeries newPie = new PieSeries
                {
                    Title = _ListRoomType[i].fullname,
                    Values = chartValue,
                    DataLabels = true,
                    FontSize = 15,
                };
                PieSerieCollection.Add(newPie);
                ReportUC c = new ReportUC();
                c.STT.Text = (i + 1).ToString();
                c.displayname.Text = _ListRoomType[i].fullname;
                c.revenue.Text = string.Format("{0:C}", doanhthu);
                c.percent.Text = phantram.ToString("#0.##%");

                aa.stp.Children.Add(c);
            }
            aa.totalmoney.Text = string.Format("{0:C}", tt);
            grid.Children.Clear();
            grid.Children.Add(aa);
        }

    }

}
