using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class AnamnesisFileHandler : GenericFileHandler<Anamnesis>
    {

        public AnamnesisFileHandler(string path) : base(path)
        {
        }

        protected override string ConvertEntityToCSV(Anamnesis anamnesis)
        {
            return string.Join(CSV_DELIMITER,
                anamnesis.Id,
                anamnesis.App.Id,
                anamnesis.Date,
                anamnesis.Description);
        }

        protected override Anamnesis ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER.ToCharArray());
            return new Anamnesis(int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                DateTime.Parse(tokens[2]),
                tokens[3]);
        }

    }
}
