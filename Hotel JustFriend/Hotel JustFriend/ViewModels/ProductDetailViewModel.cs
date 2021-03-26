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

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
  
    class ProductDetailViewModel: ViewModelBase
    {
        private string _IdProduct;
        private ObservableCollection<Product> _ListProduct;
        private string _DisplayName;
        private string _Unit;
        byte[] tg;
        private decimal _PricePerUnit;
        private byte[] _Image;
        public string IdProduct { get => _IdProduct; set => _IdProduct = value; }
        public string DisplayName { get => _DisplayName; set => _DisplayName = value; }
        public string Unit { get => _Unit; set => _Unit = value; }
        public decimal PricePerUnit { get => _PricePerUnit; set => _PricePerUnit = value; }
        public byte[] Image { get => _Image; set => _Image = value; }
        public ObservableCollection<Product> ListProduct { get => _ListProduct; set => _ListProduct = value; }

        [Command]
        public void SelectImage(ProductDetailView p)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    tg = ImageHelper.Instance.ConvertBitMapImageToByteArray(new BitmapImage(fileUri));
                }
            }
            catch
            {
                return;
            }
        }
        [Command]
        public void View()
        {
            try
            {
                ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products.Where(x => x.isDelete == false));
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
            { return; }
        }
        [Command]
        public void Delete(Window p)
        {
        }
        [Command]
        public void Save(Window p)
        {
            try 
            {
                string a = "0";
                View();
                Image = tg;
                for (int i=0;i<ListProduct.Count;i++)
                {
                    string b = ListProduct[i].idProduct.ToString();
                    if (int.Parse(a) < int.Parse(b)) a = b;
                }
                IdProduct = (int.Parse(a) + 1).ToString();
                Product newProduct = new Product() { idProduct = IdProduct, displayName = DisplayName, unit = Unit, pricePerUnit = PricePerUnit, image = Image, quantity = 0, status = "Hết hàng", isDelete = false };
                DataProvider.Instance.DB.Products.Add(newProduct);
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
