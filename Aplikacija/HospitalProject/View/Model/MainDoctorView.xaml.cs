using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using HospitalProject;
using HospitalProject.Controller;
using HospitalProject.Exception;
using HospitalProject.View.Converter;
using HospitalProject.View.DoctorView.Model;
using Model;

namespace HospitalProject.View.Model
{

    public partial class MainDoctorView : Window
    {

        public MainDoctorView()
        {
            InitializeComponent();
            DataContext = new MainDoctorViewModel();
        }
        
        
    }
}
