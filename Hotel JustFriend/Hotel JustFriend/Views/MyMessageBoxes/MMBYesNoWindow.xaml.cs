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

namespace Hotel_JustFriend.Views.MyMessageBoxes
{
    /// <summary>
    /// Interaction logic for MMBYesNoWindow.xaml
    /// </summary>
    public partial class MMBYesNoWindow : Window
    {
        public MessageBoxResult Result { get; set; }
        public MMBYesNoWindow()
        {
            InitializeComponent();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
        }
    }
}
