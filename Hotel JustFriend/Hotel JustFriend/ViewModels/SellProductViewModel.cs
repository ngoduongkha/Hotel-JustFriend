using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_JustFriend.UserControls;
using Hotel_JustFriend.Views;
using DevExpress.Mvvm.DataAnnotations;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Hotel_JustFriend.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using Hotel_JustFriend.Template;
using MvvmHelpers;
using System.Runtime.CompilerServices;
namespace Hotel_JustFriend.ViewModels
{
    class SellProductViewModel : ViewModelBase, INotifyPropertyChanged
    {
        SellProductDetailUC temp;
        static SellProductUC pp;
        private ObservableCollection<Bill> _ListBill;
        private ObservableCollection<Billinfo> _ListBillInfo;
        private ObservableCollection<Product> _ListProduct;
        public ObservableCollection<Product> ListProduct { get => _ListProduct; set { _ListProduct = value; RaisePropertyChanged(); } }
        public ObservableCollection<Billinfo> ListBillInfo { get => _ListBillInfo; set { _ListBillInfo = value; RaisePropertyChanged(); } }
        public ObservableCollection<Bill> ListBill { get => _ListBill; set { _ListBill = value; RaisePropertyChanged(); } }
        string xoadau(string x)
        {
            string res = "";
            for (int i = 0; i < x.Length; i++)
                if (x[i] != ',') res = res + x[i];
            return (res);
        }
        [Command]
        public void LoadDB(SellProductView p)
        {
            //p.tbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ListBill = new ObservableCollection<Bill>(DataProvider.Instance.DB.Bills);
            string mahd = "0";
            for (int i = 0; i < ListBill.Count; i++)
            {
                string tg = ListBill[i].idBill.ToString();
                if (int.Parse(tg) > int.Parse(mahd)) mahd = tg;
            }
            //p.tbl_mahd.Text = (int.Parse(mahd) + 1).ToString();
            ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products.Where(x => x.isDelete == false));
            for (int i = 0; i < ListProduct.Count; i++)
            {
                SellProductUC product = new SellProductUC();
                product.amount.Text = ListProduct[i].quantity.ToString();
                product.id.Text = ListProduct[i].idProduct.ToString();
                product.name.Text = ListProduct[i].displayName.ToString();
                product.amount.Text = ListProduct[i].quantity.ToString();
                product.money.Text = ListProduct[i].pricePerUnit.ToString("#,##0");
                product.img.Source = (ImageSource)new Utility.ImageToByteConverter().ConvertBack(ListProduct[i].image as object, null, null, null);
                p.stp_ListProduct.Children.Add(product);
            }
        }
        [Command]
        public void UpdateSelected(SellProductView p)
        {
            temp = new SellProductDetailUC();
            temp.id.Text = pp.id.Text;
            temp.amount.Text = pp.amount.Text;
            temp.money.Text = pp.money.Text;
            temp.ProductName.Text = pp.name.Text;
            temp.NumericSpinner.tb_soluong.Text = "0";
            temp.masp.Text = pp.id.Text;
            string tg = "";
            int l = pp.money.Text.Length;
            for (int i = 0; i < l; i++)
            {
                if (pp.money.Text[i] != ',') tg = tg + pp.money.Text[i];
            }
            int k = int.Parse(temp.NumericSpinner.tb_soluong.Text) * int.Parse(tg);
            temp.TotalMoney.Text = string.Format("{0:N0}", k);
            foreach (object child in p.stp_SelectedProduct.Children)
            {
                string childid = null;
                if (child is ProductDetailUC)
                {
                    childid = (child as ProductDetailUC).id.Text;
                    if (childid == temp.id.Text) return;
                }
            }
            if (temp != null) p.stp_SelectedProduct.Children.Add(temp);
        }
        [Command]
        public void PickProduct(SellProductUC p)
        {
            pp = p;
        }
        [Command]
        public void DeleteBillInfo(SellProductDetailUC p)
        {
            ((StackPanel)p.Parent).Children.Remove(p);
        }
        [Command]
        public void ChangeQuantity(SellProductDetailUC p)
        {
            string tg = "";
            if (int.Parse(p.NumericSpinner.tb_soluong.Text) > int.Parse(p.amount.Text)) p.NumericSpinner.tb_soluong.Text = p.amount.Text;
            int l = p.money.Text.Length;
            for (int i = 0; i < l; i++)
            {
                if (p.money.Text[i] != ',') tg = tg + p.money.Text[i];
            }
            int k = (int.Parse(p.NumericSpinner.tb_soluong.Text) * int.Parse(tg));
            p.TotalMoney.Text = k.ToString("#,##0");
        }
    }
}
