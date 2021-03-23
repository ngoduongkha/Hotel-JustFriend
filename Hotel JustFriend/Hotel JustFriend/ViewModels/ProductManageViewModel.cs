using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class ProductManageViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _ListProduct;

        public ObservableCollection<Product> ListProduct { get => _ListProduct; set => _ListProduct = value; }

        public ProductManageViewModel()
        {
            LoadDB();
        }

        private void LoadDB()
        {
            ListProduct = new ObservableCollection<Product>(DataProvider.Instance.DB.Products.Where(x => x.isDelete == false));
        }

        [Command]
        public void AddProduct()
        {
            try
            {
                ProductDetailView addproduct = new ProductDetailView();
                addproduct.ShowDialog();
            }
            catch { return; }
        }

    }
}
