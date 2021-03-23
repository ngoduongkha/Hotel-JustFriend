using Hotel_JustFriend.Views.MyMessageBoxes;
using System.Windows;

namespace Hotel_JustFriend.Models
{
    public class MyMessageBox
    {
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons)
        {
            MyMessageBoxWindow mb = new MyMessageBoxWindow(text, caption, buttons);
            mb.ShowDialog();
            return mb.Result;
        }
    }
}
