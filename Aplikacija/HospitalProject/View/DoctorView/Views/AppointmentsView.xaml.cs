﻿using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalProject.View.DoctorView.Views
{
    /// <summary>
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsView : UserControl
    {
        public AppointmentsView()
        {
            InitializeComponent();
            DataContext = new MainDoctorViewModel();
        }
    }
}
