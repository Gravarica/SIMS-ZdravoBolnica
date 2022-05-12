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
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV
{
    /// <summary>
    /// Interaction logic for DashBoardV.xaml
    /// </summary>
    public partial class DashBoardV : UserControl
    {
        public DashBoardV()
        {
            InitializeComponent();
            this.DataContext = new DashBoardVM();
        }
    }
}
