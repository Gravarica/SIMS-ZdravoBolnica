using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospitalProject.Core;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.DoctorView.Model
{
    public class MyProfileViewModel : BaseViewModel
    {
        private Doctor logDoctor;

        private string currentLanguage;

        private RelayCommand changeLanguageCommand;

        private List<ComboBoxData<String>> languageComboBox;

        public MyProfileViewModel(Doctor loggedDoctor)
        {
            LoggedDoctor = loggedDoctor;
            FillComboData();
            this.CurrentLanguage = "en-US";
        }

        public List<ComboBoxData<String>> LanguageComboBox
        {
            get
            {
                return languageComboBox;
            }
            set
            {
                languageComboBox = value;
                OnPropertyChanged(nameof(LanguageComboBox));
            }
        }

        public string CurrentLanguage
        {
            get
            {
                return currentLanguage;
            }
            set
            {
                var app = (App)Application.Current;
                currentLanguage = value;
                app.ChangeLanguage(CurrentLanguage);
            }
        }

        public Doctor LoggedDoctor
        {
            get
            {
                return logDoctor;
            }
            set
            {
                logDoctor = value;
                OnPropertyChanged(nameof(LoggedDoctor));
            }
        }

        private void FillComboData()
        {
            languageComboBox = new List<ComboBoxData<string>>();
            languageComboBox.Add(new ComboBoxData<string>{ Name = "English(US)", Value = "en-US"});
            languageComboBox.Add(new ComboBoxData<string> { Name = "Srpski(LAT)", Value = "sr-LATN" });
        }
    }

    
}
