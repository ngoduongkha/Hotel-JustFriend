using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_JustFriend.Template;
using System.Windows.Controls;
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    class BilltemplateViewModel:ViewModelBase
    {
        [Command]
        public void PrintBill(BillTemplate parameter)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    parameter.btnPrint.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(parameter.grdPrint, "Bill");
                }
            }
            finally
            {
                parameter.btnPrint.Visibility = Visibility.Visible;
            }
        }
    }
}
