using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class ProductDetailViewModel : ViewModelBase
    {
        /*private ObservableCollection<Product> _ListProduct;
        private int _ProductNumber;
        private string _ProductID;
        private string _DisplayName;
        private string _Unit;
        private decimal _PricePerUnit;
        private byte[] _Image;

        public int ProductNumber { get => _ProductNumber; set { _ProductNumber = value; RaisePropertyChanged(); } }
        public string ProductID { get => _ProductID; set => _ProductID = value; }
        public string DisplayName { get => _DisplayName; set => _DisplayName = value; }
        public string Unit { get => _Unit; set => _Unit = value; }
        public decimal PricePerUnit { get => _PricePerUnit; set => _PricePerUnit = value; }
        public byte[] Image { get => _Image; set { _Image = value; RaisePropertyChanged(); } }

        public ProductDetailViewModel()
        {
            _ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products.Where(x => x.isDelete == false));
            ProductNumber = _ListProduct.Count() + 1;

            do
            {
                ProductID = Guid.NewGuid().ToString("N").ToUpper();
            } while (_ListProduct.Select(x => x.idProduct).Contains(ProductID));
        }

        [Command]
        public void SeparateThousands(System.Windows.Controls.TextBox obj)
        {
            try
            {
                decimal money = Utility.Converter.Instance.ConvertCurrencyToDecimal(obj.Text);
                obj.Text = Utility.Converter.Instance.ConvertDecimalToCurrency(money);
                obj.Select(obj.Text.Length, 0);
            }
            catch { return; }
        }

        [Command]
        public void SelectImage()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));
                    Image = new Utility.ImageToByteConverter().Convert(image, null, null, null) as byte[];
                }
            }
            catch { return; }
        }

        [Command]
        public void Save(Window p)
        {
            try
            {
                if (Image == null)
                {
                    MyMessageBox.Show("Vui lòng chọn hình ảnh", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                Product newProduct = new Product()
                {
                    idProduct = ProductID,
                    displayName = DisplayName,
                    unit = Unit,
                    pricePerUnit = PricePerUnit,
                    image = Image
                };

                DataProvider.Instance.DB.Products.Add(newProduct);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                Close(p);
            }
            catch { return; }
        }

        [Command]
        public void MouseMoveWindow(Window p)
        {
            try
            {
                p.DragMove();
            }
            catch { return; }
        }

        [Command]
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        */
    }
}
