using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class TranslationManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private static readonly TranslationManager instance = new TranslationManager();

        public static TranslationManager Instance
        {
            get { return instance; }
        }

    }
}
