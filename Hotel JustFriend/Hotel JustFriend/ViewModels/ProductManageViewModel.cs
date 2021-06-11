using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    /*
    [POCOViewModel]
    class ProductManageViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _ListProduct;
        private ObservableCollection<string> _ListUnit;
        private ObservableCollection<string> _ListStatus;

        public ObservableCollection<Product> ListProduct { get => _ListProduct; set { _ListProduct = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListUnit { get => _ListUnit; set { _ListUnit = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListStatus { get => _ListStatus; set { _ListStatus = value; RaisePropertyChanged(); } }

        public ProductManageViewModel()
        {
            LoadDB();
            ListStatus = new ObservableCollection<string>() { "Hết hàng", "Còn hàng", "Sắp hết" };
        }

        private void LoadDB()
        {
            ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products
                .Where(x => x.isDelete == false)
                .OrderBy(x => x.displayName)
                .ThenBy(x => x.unit));
            ListUnit = new ObservableCollection<string>(DataProvider.Instance.DB.Products
                .Select(x => x.unit)
                .Distinct());
        }

        [Command]
        public void AddProduct()
        {
            try
            {
                ProductDetailView addProduct = new ProductDetailView();
                addProduct.ShowDialog();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void SearchProduct(ProductManageView p)
        {
            try
            {
                ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products
                    .Where(x => x.isDelete == false)
                    .OrderBy(x => x.displayName)
                    .ThenBy(x => x.unit));

                if (string.IsNullOrEmpty(p.tbSearch.Text))
                    return;

                ListProduct = new ObservableCollection<Product>(ListProduct
                    .Where(x => x.displayName.Contains(p.tbSearch.Text))
                    .OrderBy(x => x.displayName)
                    .ThenBy(x => x.unit)
                    .ToList());

                p.tbSearch.Text = string.Empty;
            }
            catch { return; }
        }

        [Command]
        public void MouseEnter(Grid p)
        {
            try
            {
                p.Children[1].Visibility = System.Windows.Visibility.Visible;
                p.Children[2].Visibility = System.Windows.Visibility.Visible;
            }
            catch { return; }
        }

        [Command]
        public void MouseLeave(Grid p)
        {
            try
            {
                p.Children[1].Visibility = System.Windows.Visibility.Hidden;
                p.Children[2].Visibility = System.Windows.Visibility.Hidden;
            }
            catch { return; }
        }
    }
    */
}
