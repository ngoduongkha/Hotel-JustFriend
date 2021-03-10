using Hotel_JustFriend.Views.MyMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_JustFriend.Models
{
    public class MyMessageBox
    {
        public enum Buttons { YesNo, OK}
        public static string Show(string text, string caption)
        {
            return Show(text, caption, Buttons.OK);
        }
        public static string Show(string text, string caption, Buttons buttons)
        {
            MyMessageBoxWindow mb = new MyMessageBoxWindow(text, caption, buttons);
            mb.ShowDialog();
            return mb.ReturnString;
        }
    }
}
