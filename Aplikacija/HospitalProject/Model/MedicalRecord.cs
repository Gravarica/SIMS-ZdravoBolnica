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
        private List<Allergies> _allergies;
        public MedicalRecord(int id, int patientId)
        {
            _anamneses = new List<Anamnesis>();
            _allergies= new List<Allergies>();
            InstantiateData(patientId, id);
        }

        public MedicalRecord(int id, int patientId, List<Allergies> allergies)
        {
            InstantiateData(patientId, id);
            Allergies = allergies;
            _anamneses = new List<Anamnesis>();
        }

        private void InstantiateData(int patientId, int id)
        {
            Id = id;
            Patient = new Patient(patientId);
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

        public List<Allergies> Allergies
        {
            get
            {
                return _allergies;
            }
            set
            {
                _allergies = value;
                OnPropertyChanged(nameof(Allergies));
            }
        }

    }
}
