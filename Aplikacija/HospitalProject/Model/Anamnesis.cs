using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Anamnesis : ViewModelBase
    {

        private Appointment _app;
        private DateTime _date;
        private string _description;
        private int _id;

        public Anamnesis(int id, int appointmentId, DateTime date, string description)
        {
            _id = id;
            App = new Appointment();
            App.Id = appointmentId;
            InstantiateFields(date, description);
        }

        public Anamnesis(Appointment appointment, string description)
        {
            App = appointment;
            InstantiateFields(appointment.Date, description);
        }

        private void InstantiateFields(DateTime date, string description)
        {
            Date = date;
            Description= description;
        }

        public Appointment App
        {
            get
            {
                return _app;
            }
            set
            {
                _app = value;
                OnPropertyChanged(nameof(App));
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
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
    }
}
