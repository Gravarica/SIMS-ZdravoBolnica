using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.DataUtility;

namespace HospitalProject.FileHandler
{
    public class DoctorFileHandler : GenericFileHandler<Doctor>
    {

        public DoctorFileHandler(string path) : base(path)
        {
        }

        protected override Doctor ConvertCSVToEntity(string csv)
        {
            var tokens = csv.Split(CSV_DELIMITER.ToCharArray());
            return new Doctor(int.Parse(tokens[0]),
                tokens[1],
                tokens[2],
                tokens[3],
                TimeOnly.Parse(tokens[4]),
                TimeOnly.Parse(tokens[5]),
                EnumConverter.ConvertTokenToSpecialization(tokens[6]),
                int.Parse(tokens[7]),
                int.Parse(tokens[8]));
        }


    }
}
