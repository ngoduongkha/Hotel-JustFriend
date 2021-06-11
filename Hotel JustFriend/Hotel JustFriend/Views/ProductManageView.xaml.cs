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
            //this.DataContext = new ViewModels.ProductManageViewModel();
        }

        private void grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as Grid).Children[1].Visibility = System.Windows.Visibility.Visible;
            (sender as Grid).Children[2].Visibility = System.Windows.Visibility.Visible;
        }

        private void grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as Grid).Children[1].Visibility = System.Windows.Visibility.Hidden;
            (sender as Grid).Children[2].Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
