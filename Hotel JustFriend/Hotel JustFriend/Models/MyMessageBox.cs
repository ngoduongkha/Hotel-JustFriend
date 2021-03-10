using Hotel_JustFriend.Views.MyMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_JustFriend.Models
{
    class MyMessageBox
    {
        public static MessageBoxResult Show(string message, string caption, MessageBoxButton button)
        {
            MessageBoxResult result = MessageBoxResult.None;
            switch (button)
            {
                case MessageBoxButton.YesNo:
                    MMBYesNoWindow popup = new MMBYesNoWindow();                   
                    popup.message.Text = message;
                    popup.caption.Content = caption;
                    popup.ShowDialog();
                    popup.Close();
                break;
            }
            return result;
        }
    }
}
