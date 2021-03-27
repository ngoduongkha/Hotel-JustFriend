using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for ProductManageView.xaml
    /// </summary>
    public partial class ProductManageView : UserControl
    {
        public ProductManageView()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ProductManageViewModel();
        }
    }
}
