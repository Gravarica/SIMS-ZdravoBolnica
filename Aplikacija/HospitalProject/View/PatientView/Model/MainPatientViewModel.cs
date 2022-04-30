using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.PatientView.View;
using HospitalProject.View.Util;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class MainPatientViewModel : BaseViewModel

    {

        private Appointment selectedItem;

        private IList<Doctor> _doctors;
        private DateTime _date;
        private int _duration;
        private String _time;
        private Doctor _doctor;
        private Patient _patient;

        private RelayCommand addCommand;
        private RelayCommand deleteCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand editAppointmentCommand;

        public ObservableCollection<Appointment> AppointmentItems { get; set; }
        public ObservableCollection<int> DoctorIds { get; set; }

        AppointmentController _appointmentController;
        PatientController _patientController;
        DoctorController _doctorController;

        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();

        public List<ComboBoxData<Doctor>> DoctorComboBox
        {

            get
            {
                return doctorComboBox;
            }
            set
            {
                doctorComboBox = value;
                OnPropertyChanged(nameof(DoctorComboBox));
            }
        }

        public Appointment SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public Doctor DoctorData
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged(nameof(DoctorData));
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

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public String Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public MainPatientViewModel()
        {

            InstantiateControllers();
            InstantiateData();
            FillComboData();

        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
        }

        private void InstantiateData()
        {
            AppointmentItems = new ObservableCollection<Appointment>(_appointmentController.GetAll().ToList());
            _doctors = _doctorController.GetAll().ToList();
            _patient = _patientController.Get(3);
        }

        private void FillComboData()
        {

            foreach (Doctor d in _doctors)
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = d.FirstName + " " + d.LastName, Value = d });
            }

        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => DeleteCommandExecute(), param => CanDeleteCommandExecute()));
            }
        }

        private bool CanDeleteCommandExecute()
        {
            return SelectedItem != null;
        }

        private void DeleteCommandExecute()
        {
            if (MessageBox.Show("Are you sure you want to cancel an appointment?", "Cancel an appointment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _appointmentController.Delete(SelectedItem.Id);
                AppointmentItems.Remove(SelectedItem);
            }

        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => AddCommandExecute(), param => CanAddCommandExecute()));
            }

        }

        private bool CanAddCommandExecute()
        {
            return true;
        }

        private void AddCommandExecute()
        {
            if (!CanCreate())
            {
                MessageBox.Show("Appointment is already taken");
                return;
            }

            Appointment appointment = new Appointment(parseTime(), _duration, DoctorData, _patient);
            _appointmentController.Create(appointment);
            AppointmentItems.Add(appointment);
        }



         public RelayCommand NewAppointmentCommand
         {
             get
             {
                 return newAppointmentCommand ?? (newAppointmentCommand = new RelayCommand(param => NewAppointmentCommandExecute(), param => CanNewAppointmentCommandExecute()));
             }
         }

         private bool CanNewAppointmentCommandExecute()
         {
             return true;
         }

         private void NewAppointmentCommandExecute()
         {
             NewAppointmentPatientView view = new NewAppointmentPatientView();
             view.DataContext = new NewAppointmentPatientViewModel(AppointmentItems);
             view.ShowDialog();
         }


       /* public RelayCommand EditAppointmentCommand
        {
            get
            {
                return editAppointmentCommand ?? (editAppointmentCommand = new RelayCommand(param => EditAppointmentCommandExecute(),
                                                                                            param => CanEditAppointmentCommandExecute()));
            }
        }

        private bool CanEditAppointmentCommandExecute()
        {
            return SelectedItem != null;
        }*/

       /* private void EditAppointmentCommandExecute()
        {
            EditAppointmentview view = new EditAppointmentview();
            view.DataContext = new EditAppointmentViewModel(SelectedItem, AppointmentItems);
            view.ShowDialog();
        }*/





        private DateTime parseTime()
        {
            String[] hoursAndMinutes = _time.Split(':');
            int hours = int.Parse(hoursAndMinutes[0]);
            int minutes = int.Parse(hoursAndMinutes[1]);
            return new DateTime(_date.Year, _date.Month, _date.Day, hours, minutes, 0);
        }

        private bool CanCreate()
        {
            TimeSpan timeSpan = new TimeSpan(0, _duration, 0); ;
            DateTime newAppEndDate = _date + timeSpan;
            DateTime existingAppointmentEndDate;
            foreach (Appointment appointment in AppointmentItems)
            {
                existingAppointmentEndDate = appointment.Date + new TimeSpan(0, Duration, 0);
                if (_date.Year == appointment.Date.Year && _date.Month == appointment.Date.Month && _date.Day == appointment.Date.Day)
                {
                    if (_date <= appointment.Date && newAppEndDate >= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date >= appointment.Date && newAppEndDate <= existingAppointmentEndDate)
                    {
                        return false;
                    }
                    else if (_date < appointment.Date && newAppEndDate < existingAppointmentEndDate && newAppEndDate > appointment.Date)
                    {
                        return false;
                    }
                    else if (_date > appointment.Date && _date < existingAppointmentEndDate && newAppEndDate > existingAppointmentEndDate)
                    {
                        return false;
                    }


                }

            }

            return true;
        }
    }

}


