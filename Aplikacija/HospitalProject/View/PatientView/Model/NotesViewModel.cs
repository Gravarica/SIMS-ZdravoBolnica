using Controller;
using HospitalProject.Controller;
using HospitalProject.Model;
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
    public class NotesViewModel : ViewModelBase
    {
        public ObservableCollection<Note> Notes { get; set; }
        private NoteController noteController;
        private PatientController patientController;
        private UserController userController;
        

        public NotesViewModel() {

            var app = System.Windows.Application.Current as App;
            noteController = app.NoteController;
            patientController = app.PatientController;
            userController = app.UserController;
            User loggedUser = userController.GetLoggedUser();
            Patient loggedPatient = patientController.GetLoggedPatient(loggedUser.Username);
            Notes = new ObservableCollection<Note>(noteController.GetNotesByPatient(loggedPatient.Id));
        }

        

        
    }

}
