using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class MedicalRecord : ViewModelBase
    {

        private int _id;
        private Patient _patient;
        private List<Anamnesis> _anamneses;

        public MedicalRecord(int id, int patientId)
        {
            _anamneses = new List<Anamnesis>();
            _id = id;
           _patient = new Patient(patientId);
        }

        public int Id 
        { 
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public List<Anamnesis> Anamneses
        {
            get
            {
                return _anamneses;
            }
            set
            {
                _anamneses = value;
                OnPropertyChanged(nameof(Anamneses));
            }
        }


    }
}
