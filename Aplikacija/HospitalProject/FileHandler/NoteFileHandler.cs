using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class NoteFileHandler
    {

        private string path;
        private string delimiter;

        public NoteFileHandler(string path, string delimiter)
        {
            this.path = path;
            this.delimiter = delimiter;
        }

        public IEnumerable<Note> ReadAll()
        {
            return File.ReadAllLines(path).Select(ConvertCSVToNote).ToList();
        }

        private string ConvertNoteToCSV(Note note)
        {
            return string.Join(delimiter,
                               note.Id,
                               note.Anamnesis.Id,
                               note.Text
                               );
        }

        private Note ConvertCSVToNote(string csv)
        {
            string[] tokens = csv.Split(delimiter);
            return new Note(int.Parse(tokens[0]),
                                    new Anamnesis(int.Parse(tokens[1])),
                                    tokens[2]
                                    );
        }

        public void AppendLineToFile(Note note)
        {
            string line = ConvertNoteToCSV(note);
            File.AppendAllText(path, line + Environment.NewLine);
        }

        public void Save(List<Note> notes)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (Note note in notes)
                {
                    file.WriteLine(ConvertNoteToCSV(note));
                }
            }
        }



    }
}
