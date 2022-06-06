using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class NoteController
    {
        private NoteService noteService;

        public NoteController(NoteService noteService)
        {
            this.noteService = noteService;
        }

        public void Insert(Note note)
        {
            noteService.Insert(note);
        }

        public List<Note> GetNotesByPatient(int id)
        {
            return noteService.GetNotesByPatient(id);
        }

        public List<Note> GetAll()
        {
            return noteService.GetAll().ToList();
        }
    }
}
