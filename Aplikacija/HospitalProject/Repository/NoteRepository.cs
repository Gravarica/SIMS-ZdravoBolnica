using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class NoteRepository
    {
        private NoteFileHandler noteFileHandler;
        private AnamnesisRepository anamnesisRepository;
        private List<Note> notes;
        private int noteMaxId;

        public NoteRepository(NoteFileHandler noteFileHandler, AnamnesisRepository anamnesisRepository)
        {
            this.noteFileHandler = noteFileHandler;
            this.anamnesisRepository = anamnesisRepository;
            InstantiateNoteList();
        }

        private int GetMaxId()
        {
            return notes.Count() == 0 ? 0 : notes.Max(note => note.Id);
        }

        private void SetAnamnesisForNote(Note note)
        {
            note.Anamnesis = anamnesisRepository.GetById(note.Anamnesis.Id);
        }

        private void InstantiateNoteList()
        {
            notes = noteFileHandler.ReadAll().ToList();
            notes.ForEach(note => SetAnamnesisForNote(note));
            noteMaxId = GetMaxId();
        }

        public List<Note> GetAll()
        {
            return notes;
        }

        public List<Note> GetNotesByPatient(int patientId)
        {
            return notes.Where(note => note.Anamnesis.App.Patient.Id == patientId).ToList();
        }

        public void Insert(Note note)
        {
            note.Id = ++noteMaxId;
            notes.Add(note);
            noteFileHandler.AppendLineToFile(note);
        }


    }
}
