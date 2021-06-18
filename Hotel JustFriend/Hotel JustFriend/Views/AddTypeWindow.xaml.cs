﻿using Hotel_JustFriend.ViewModels;
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

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for AddCustomerTypeWindow.xaml
    /// </summary>
    public partial class AddTypeWindow : Window
    {
        public bool IsAddCustomerType { get; set; }
        public AddTypeWindow(bool isAddCustomerType)
        {
            InitializeComponent();
            IsAddCustomerType = isAddCustomerType;
            this.DataContext = new CustomizeParametersViewModel();
        }
    }
}