using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class NoteService
    {
        private NoteRepository noteRepository;
        private AnamnesisService anamnesisService;

        public NoteService(NoteRepository noteRepository, AnamnesisService anamnesisService)
        {
            this.noteRepository = noteRepository;
            this.anamnesisService = anamnesisService;
        }
        public void Insert(Note note)
        {
            noteRepository.Insert(note);

        }

        private void SetAnamnesisForNote(Note note)
        {
            note.Anamnesis = anamnesisService.GetById(note.Anamnesis.Id);
        }

        private void BindAnamnesesWithNotes(List<Note> notes)
        {
            notes.ForEach(note => SetAnamnesisForNote(note));
        }

        public List<Note> GetAll()
        {
            var notes = noteRepository.GetAll();
            BindAnamnesesWithNotes(notes);
            return notes;
        }

        public List<Note> GetNotesByPatient(int patientId)
        {
            var notes = noteRepository.GetNotesByPatient(patientId);
            BindAnamnesesWithNotes(notes);
            return notes;
        }

    }
}
