using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class MedicineReport : ViewModelBase
    {
        private int id;
        private DateTime submissionDate;
        private Equipement medicine;
        private Doctor doctor;
        private string description;

        public MedicineReport(int id, int doctorId, DateTime submissionDate, int medicineId, string description)
        {
            Id = id;
            Medicine = new Equipement(medicineId);
            Doctor = new Doctor(doctorId);
            InstantiateFields(submissionDate, description);
        }

        public MedicineReport(Equipement medicine, string description, Doctor doctor)
        {
            Medicine = medicine;
            Doctor = doctor;
            InstantiateFields(DateTime.Now, description);
        }

        private void InstantiateFields(DateTime submissionDate, string description)
        {
            SubmissionDate = submissionDate;
            Description = description;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime SubmissionDate
        {
            get
            {
                return submissionDate;
            }
            set 
            { 
                submissionDate = value;
                OnPropertyChanged(nameof(SubmissionDate));
            }
        }

        public Equipement Medicine
        {
            get
            {
                return medicine;
            }
            set 
            { 
                medicine = value;
                OnPropertyChanged(nameof(Medicine));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set 
            { 
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }
    }
}
