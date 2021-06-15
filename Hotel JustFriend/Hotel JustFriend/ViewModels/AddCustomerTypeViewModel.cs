using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class AddCustomerTypeViewModel : ViewModelBase
    {
        private string _type;

        public string Type { get => _type; set { _type = value; RaisePropertiesChanged(); } }

        [Command]
        public void MouseMoveWindow(Window p)
        {
            try
            {
                p.DragMove();
            }
            catch { return; }
        }

        [Command]
        public void CloseWindow(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        [Command]
        public void Add()
        {

        }
    }
}
