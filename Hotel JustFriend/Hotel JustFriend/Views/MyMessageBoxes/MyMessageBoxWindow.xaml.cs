using Hotel_JustFriend.Models;
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
    public partial class MyMessageBoxWindow : Window
    {
        public MessageBoxResult Result { get; set; }
        public MyMessageBoxWindow(string _text, string _caption, MessageBoxButton buttons)
        {
            InitializeComponent();
            caption.Content = _caption;
            message.Text = _text;
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    //ok.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    yes.Visibility = Visibility.Visible;
                    no.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }
    }
}
