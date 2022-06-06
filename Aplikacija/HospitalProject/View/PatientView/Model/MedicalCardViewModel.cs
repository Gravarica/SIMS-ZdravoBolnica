using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.PatientView.View;
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
    public class MedicalCardViewModel : ViewModelBase
    {


        private Anamnesis selectedItem;
        private string description;
        private DateTime date;
        private string doctor;
        private NoteController _noteController;
        private AnamnesisController _anamnesisController;
        private UserController _userController;
        private PatientController _patientController;
        private Window _window;
        public ObservableCollection<Anamnesis> Anamneses { get; set; }
        private RelayCommand makeNoteCommand;

        public MedicalCardViewModel()
        {
            var app = System.Windows.Application.Current as App;


            _anamnesisController = app.AnamnesisController;
            _patientController = app.PatientController;
            _userController = app.UserController;
            _noteController = app.NoteController;
            Anamneses = new ObservableCollection<Anamnesis>(_anamnesisController.GetAnamnesisByMedicalRecord(_patientController.GetLoggedPatient(_userController.GetLoggedUser().Username).Id));
        }

        

        public Anamnesis SelectedItem
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

        private string Doctor
        {
            get
            {
                return Doctor;
            }
        
        }

        private DateTime Date
        {
            get
            {
                return Date;
            }
        }

        private string Description
        {
            get
            {
                return Description;
            }
        }

        public RelayCommand MakeNoteCommand
        {

            get
            {
                return makeNoteCommand ?? (makeNoteCommand = new RelayCommand(param => MakeNoteCommandExecute(), param => CanMakeNoteCommandExecute()));
            }

        }

        private bool CanMakeNoteCommandExecute()
        {
           return SelectedItem !=null && DoesNoteExist();
        }

        private bool DoesNoteExist()
        {
            List<Note> notes = _noteController.GetAll().ToList();
            foreach(Note note in notes)
            {
                if (SelectedItem.Id == note.Anamnesis.Id)
                    return false;
            }
            return true;
        }

        private void MakeNoteCommandExecute()
        {
            MakeNoteView view = new MakeNoteView();
            view.DataContext = new MakeNoteViewModel(SelectedItem, view);
            view.ShowDialog();
        }
    }
}
