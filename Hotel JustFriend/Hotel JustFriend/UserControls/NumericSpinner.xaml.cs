using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hotel_JustFriend.UserControls
{
    /// <summary>
    /// Interaction logic for NumericSpinner.xaml
    /// </summary>
    public partial class NumericSpinner : UserControl
    {
        string soluong = "";
        public event EventHandler ValueChanged;
        public NumericSpinner()
        {
            InitializeComponent();
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            int s = int.Parse(tb_soluong.Text);
            s++;
            if (s > 99) s = 99;
            tb_soluong.Text = s.ToString();
            ValueChanged(this, new EventArgs());
        }
        private void tb_soluong_TextChanged(object sender, RoutedEventArgs e)
        {
            soluong = "";
            foreach (char i in tb_soluong.Text)
            {
                if (i > '9' || i < '0')
                {
                    if (soluong == "") tb_soluong.Text = "0";
                    else tb_soluong.Text = soluong;
                    break;
                }
                soluong = soluong + i;
            }
            if (int.Parse(tb_soluong.Text) > 99) tb_soluong.Text = "99";
            if (int.Parse(tb_soluong.Text) < 0) tb_soluong.Text = "0";
        }
        private void DownClick(object sender, RoutedEventArgs e)
        {

            int s = int.Parse(tb_soluong.Text);
            s--;
            if (s < 0) s = 0;
            tb_soluong.Text = s.ToString();
            ValueChanged(this, new EventArgs());
        }
    }
}
