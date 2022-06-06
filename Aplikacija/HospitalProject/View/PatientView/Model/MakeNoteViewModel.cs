using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class MakeNoteViewModel : ViewModelBase
    {

        private string noteText;
        private NoteController noteController;
        private RelayCommand confirmNoteCommand;
        private Anamnesis insertAnamnesis;
        private Window window;
        

        public MakeNoteViewModel(Anamnesis anamnesis, Window _window)
        {
            var app = System.Windows.Application.Current as App;
            window = _window;
            noteController = app.NoteController;
            InsertAnamnesis = anamnesis;

        }

        public string NoteText
        {
            get
            {
                return noteText;
            }
            set
            {
                noteText = value;
                OnPropertyChanged(nameof(NoteText));
            }
        }

        public Anamnesis InsertAnamnesis
        {
            get
            {
                return insertAnamnesis;
            }
            set
            {
                insertAnamnesis = value;
                OnPropertyChanged(nameof(InsertAnamnesis));
            }
        }

        public RelayCommand ConfirmNoteCommand
        {

            get
            {
                return confirmNoteCommand ?? (confirmNoteCommand = new RelayCommand(param => ConfirmNoteCommandExecute(), param => CanConfirmNoteCommandExecute()));
            }

        }

        private bool CanConfirmNoteCommandExecute()
        {
            return NoteText != "";
        }

        private void ConfirmNoteCommandExecute()
        {
            Note insertNote = new Note(InsertAnamnesis, NoteText);
            noteController.Insert(insertNote);
            window.Close();
        }
    }
}
