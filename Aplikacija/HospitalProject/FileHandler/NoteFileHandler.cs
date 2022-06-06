using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class NoteFileHandler : GenericFileHandler<Note>
    {

        public NoteFileHandler(string path) : base(path) { }

        protected override string ConvertEntityToCSV(Note note)
        {
            return string.Join(CSV_DELIMITER,
                note.Id,
                note.Anamnesis.Id,
                note.Text
            );
        }

        protected override Note ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);
            return new Note(int.Parse(tokens[0]),
                new Anamnesis(int.Parse(tokens[1])),
                tokens[2]
            );
        }
    }
}
