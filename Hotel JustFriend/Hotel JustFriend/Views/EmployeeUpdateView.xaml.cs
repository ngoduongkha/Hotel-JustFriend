using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.ViewModels;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for EmployeeUpdateView.xaml
    /// </summary>
    public partial class EmployeeUpdateView : Window
    {
        public EmployeeUpdateView(Employee a)
        {
            InitializeComponent();
            this.DataContext = new EmployeeDetailViewModel(a);
        }
    }
}
