using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for EmployeeManageView.xaml
    /// </summary>
    public partial class EmployeeManageView : UserControl
    {
        public EmployeeManageView()
        {
            InitializeComponent();
            this.DataContext = new EmployeeManageViewModel();
        }
    }
}
