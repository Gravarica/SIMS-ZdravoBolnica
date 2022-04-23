using HospitalProject.Core;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class PreviousExaminationsViewModel : BaseViewModel
    {

        public ObservableCollection<Anamnesis> Anamneses { get; set; }

        public PreviousExaminationsViewModel(MedicalRecord medicalRecord)
        {
            Anamneses = new ObservableCollection<Anamnesis>(medicalRecord.Anamneses);
        }

    }
}
