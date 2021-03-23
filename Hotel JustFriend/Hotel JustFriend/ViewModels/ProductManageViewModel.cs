using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    class ProductManageViewModel: ViewModelBase
    { 

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
