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
                ProductDetailView addProduct = new ProductDetailView();
                addProduct.ShowDialog();
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
}
