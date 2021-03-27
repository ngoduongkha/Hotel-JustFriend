using Hotel_JustFriend.ViewModels;
using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for ProductManageWindow.xaml
    /// </summary>
    public partial class ProductManageView : UserControl
    {
        public ProductManageView()
        {
            InitializeComponent();
            this.DataContext = new ProductManageViewModel();
        }
    }
}
