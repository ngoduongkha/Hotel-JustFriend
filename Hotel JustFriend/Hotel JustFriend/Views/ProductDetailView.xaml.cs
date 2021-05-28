using Hotel_JustFriend.ViewModels;
using System.Windows;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for ProductDetailView.xaml
    /// </summary>
    public partial class ProductDetailView : Window
    {
        public ProductDetailView()
        {
            InitializeComponent();
            this.DataContext = new ProductDetailViewModel();
        }
    }
}
