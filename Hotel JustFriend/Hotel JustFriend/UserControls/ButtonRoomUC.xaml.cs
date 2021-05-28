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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotel_JustFriend.UserControls;
using Hotel_JustFriend.Views;
namespace Hotel_JustFriend.UserControls
{
    /// <summary>
    /// Interaction logic for ButtonRoomUC.xaml
    /// </summary>
    public partial class ButtonRoomUC : UserControl
    {
        public ButtonRoomUC()
        {
            InitializeComponent();
        }

        private void btn_room_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow p = new BookingWindow();
            p.idroom = this.id_room;
            p.ShowDialog();

        }
    }
}
