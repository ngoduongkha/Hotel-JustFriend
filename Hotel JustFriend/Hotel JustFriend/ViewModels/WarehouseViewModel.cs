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
    [POCOViewModel]
    class WarehouseViewModel : ViewModelBase,INotifyPropertyChanged
    {/*
        static WarehouseView whview;
        ProductDetailUC temp;
        static ImportProductUC pp;
        static int TotalMoney_int = 0;
        private string totalMoney_string = "0";
        private ObservableCollection<Product> _ListProduct;
        private ObservableCollection<ProductImport> _ListProductImport;
        private ObservableCollection<ProductImportInfo> _ListProductImportInfo;
        public ObservableCollection<Product> ListProduct { get => _ListProduct; set { _ListProduct = value; RaisePropertyChanged(); } }
        public ObservableCollection<ProductImport> ListProductImport { get => _ListProductImport; set => _ListProductImport = value; }
        public ObservableCollection<ProductImportInfo> ListProductImportInfo { get => _ListProductImportInfo; set => _ListProductImportInfo = value; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string TotalMoney_string 
        {
            get => totalMoney_string;
            set
            {
                totalMoney_string = value;
                OnPropertyChanged();
            }
        }

        public WarehouseViewModel()
        {
        }
        [Command] 
        public void UpdateSelected(WarehouseView p)
        {
            temp = new ProductDetailUC();
            temp.id.Text = pp.id.Text;
            temp.money.Text = pp.money.Text;
            temp.ProductName.Text = pp.name.Text;
            temp.NumericSpinner.tb_soluong.Text = "1";
            temp.masp.Text = pp.id.Text;
            string tg = "";
            int l = pp.money.Text.Length;
            for (int i = 0; i < l; i++)
            {
                if (pp.money.Text[i] != ',') tg = tg + pp.money.Text[i];
            }
            int k = int.Parse(temp.NumericSpinner.tb_soluong.Text) * int.Parse(tg);
            temp.TotalMoney.Text = string.Format("{0:N0}",k);
            TotalMoney_int = TotalMoney_int + k;
            totalMoney_string = TotalMoney_int.ToString("#,##0");
            foreach (object child in p.stp_SelectedProduct.Children)
            {
                string childid = null;
                if (child is ProductDetailUC)
                {
                    childid = (child as ProductDetailUC).id.Text;
                    if (childid == temp.id.Text) return;
                }
            }
            if (temp!=null) p.stp_SelectedProduct.Children.Add(temp);
            whview = p;
            whview.total_money.Text = totalMoney_string;
        }
        string xoadau(string x)
        {
            string res = "";
            for (int i = 0; i < x.Length; i++)
                if (x[i] != ',') res = res + x[i];
            return (res);
        }

        [Command]
        public void MoneyChanged(ProductDetailUC p)
        {
            string tg = "";
            int l = p.money.Text.Length;
            if (l == 0)
            {
                p.money.Text = "0";
                return;
            }
            int k=0;
            for (int i = 0; i < l; i++)
            {
                if (p.money.Text[i] >='0' && p.money.Text[i]<='9') tg = tg + p.money.Text[i];
                if (tg!="")k = int.Parse(tg);
                if (k > 9999999)
                {
                    k = 9999999;
                    p.money.Text = k.ToString("#,##0");
                    return;
                }
                else
                if (k < 0) 
                { 
                    k = 0;
                    p.money.Text = k.ToString("#,##0");
                    return;
                }
            }
            TotalMoney_int = TotalMoney_int - int.Parse(xoadau(p.TotalMoney.Text));
            k = int.Parse(tg);
            int cr = p.money.SelectionStart;
            p.money.Text = k.ToString("#,##0");
            p.money.Select(cr, 0);
            k = k * int.Parse(p.NumericSpinner.tb_soluong.Text);
            p.TotalMoney.Text = k.ToString("#,##0");
            TotalMoney_int = TotalMoney_int + k;
            TotalMoney_string = TotalMoney_int.ToString("#,##0");
            whview.total_money.Text = totalMoney_string;
        }
 
        [Command]
        public void btn_thanhtoan(WarehouseView p)
        {
            long k = 0;
            foreach (object child in p.stp_SelectedProduct.Children)
            {
                string childid = null;
                if (child is ProductDetailUC)
                {
                    childid = (child as ProductDetailUC).TotalMoney.Text;
                    k = k + int.Parse(xoadau(childid));
                }
            }
            // xuat bill
            ImportBill billimport = new ImportBill();
            billimport.tbl_mahd.Text = '#' + p.tbl_mahd.Text;
            billimport.tbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            billimport.total_money.Text = k.ToString("#,##0");
            int d = 0;
            foreach (object child in p.stp_SelectedProduct.Children)
            {
                string chilid = null;
                ImportProductDetailUC aa = new ImportProductDetailUC();
                if (child is ProductDetailUC)
                {
                    d++;
                    chilid = (child as ProductDetailUC).id.Text;
                    Product Product = DataProvider.Instance.DB.Products.Single(x => x.idProduct == chilid);
                    aa.STT.Text = d.ToString();
                    aa.ProductName.Text = (child as ProductDetailUC).ProductName.Text;
                    aa.Price.Text = (child as ProductDetailUC).money.Text;
                    aa.quantity.Text = (child as ProductDetailUC).NumericSpinner.tb_soluong.Text;
                    aa.total_money.Text = (child as ProductDetailUC).TotalMoney.Text;
                    aa.unit.Text = Product.unit.ToString();
                    billimport.stp_listview.Children.Add(aa);
                }
            }
            billimport.ShowDialog();
            //---
            ProductImport newProductImport = new ProductImport();
            newProductImport.idImport = p.tbl_mahd.Text;
            newProductImport.idEmployee = 13;
            newProductImport.importPrice = k;
            newProductImport.dateImport = DateTime.Now;
            DataProvider.Instance.DB.ProductImports.Add(newProductImport);
            DataProvider.Instance.DB.SaveChanges();
            foreach (object child in p.stp_SelectedProduct.Children)
            {
                string childid = null;
                int sl = 0;
                string childmoney = null;
                if (child is ProductDetailUC)
                { 
                    childid = (child as ProductDetailUC).id.Text;
                    childmoney = (child as ProductDetailUC).TotalMoney.Text;
                    sl = int.Parse((child as ProductDetailUC).NumericSpinner.tb_soluong.Text);
                    Product UpdateProduct = DataProvider.Instance.DB.Products.Single(x => x.idProduct == childid);
                    UpdateProduct.quantity = UpdateProduct.quantity + sl;
                    DataProvider.Instance.DB.SaveChanges();
                    ProductImportInfo a = new ProductImportInfo();
                    a.idImport = p.tbl_mahd.Text;
                    a.idProduct = childid;
                    a.quantity = sl;
                    a.price = decimal.Parse(xoadau(childmoney));
                    DataProvider.Instance.DB.ProductImportInfoes.Add(a);
                    DataProvider.Instance.DB.SaveChanges();
                }
            }
        }

        [Command] 
        public void PickProduct(ImportProductUC p)
        {
            pp = p;
        }
        [Command]
        public void ChangeQuantity(ProductDetailUC p)
        {
            TotalMoney_int = TotalMoney_int - int.Parse(xoadau(p.TotalMoney.Text));
            string tg = "";
            int l = p.money.Text.Length;
            for (int i = 0; i < l; i++)
            {
                if (p.money.Text[i] != ',') tg = tg + p.money.Text[i];
            }
            int k= (int.Parse(p.NumericSpinner.tb_soluong.Text) * int.Parse(tg));
            p.TotalMoney.Text = k.ToString("#,##0");
            TotalMoney_int = TotalMoney_int + k;
            totalMoney_string = TotalMoney_int.ToString("#,##0");
            whview.total_money.Text = totalMoney_string;
        }
        [Command]
        public void LoadDB(WarehouseView p)
        {
            p.tbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ListProductImport = new ObservableCollection<ProductImport>(DataProvider.Instance.DB.ProductImports);
            string mahd = "0";
            for (int i=0;i<ListProductImport.Count;i++)
            {
                string tg = ListProductImport[i].idImport.ToString();
                if (int.Parse(tg) > int.Parse(mahd)) mahd = tg;
            }
            p.tbl_mahd.Text = (int.Parse(mahd)+1).ToString();
            ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products.Where(x => x.isDelete == false));
             for (int i = 0; i < ListProduct.Count; i++)
             {
                 ImportProductUC product = new ImportProductUC();
                product.amount.Text = ListProduct[i].quantity.ToString();
                 product.id.Text = ListProduct[i].idProduct.ToString();
                 product.name.Text = ListProduct[i].displayName.ToString();
                 product.money.Text = ListProduct[i].pricePerUnit.ToString("#,##0");
                 product.img.Source = (ImageSource)new Utility.ImageToByteConverter().ConvertBack(ListProduct[i].image as object,null,null,null);
                 p.stp_ListProduct.Children.Add(product);
             }
            whview = p;
        }
        [Command]
        public void DeleteBillInfo(ProductDetailUC p)
        {
            TotalMoney_int = TotalMoney_int- int.Parse(xoadau(p.TotalMoney.Text));
            totalMoney_string = TotalMoney_int.ToString("#,##0");
            whview.total_money.Text = totalMoney_string;
            ((StackPanel)p.Parent).Children.Remove(p);

        }
        */
    }
}
