﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for EmployeeManageView.xaml
    /// </summary>
    public partial class EmployeeManageView : UserControl
    {
        public EmployeeManageView()
        {
            InitializeComponent();
            this.DataContext = new EmployeeManageViewModel();
        }
    }
}
