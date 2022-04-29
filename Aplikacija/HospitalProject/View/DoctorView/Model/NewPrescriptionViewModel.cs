using HospitalProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class NewPrescriptionViewModel : BaseViewModel
    {
        private Window _window;

        private RelayCommand returnCommand;

        public NewPrescriptionViewModel(Window window)
        {
            _window = window;
        }

        public RelayCommand ReturnCommand
        {
            get
            {
                return returnCommand ?? (returnCommand = new RelayCommand(param => ReturnCommandExecute(), param => CanReturnCommandExecute()));
            }
        }

        private bool CanReturnCommandExecute()
        {
            return true;
        }

        private void ReturnCommandExecute()
        {
            _window.Close();
        }
    }
}
