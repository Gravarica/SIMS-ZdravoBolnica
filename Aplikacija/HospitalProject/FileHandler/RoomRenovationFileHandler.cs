using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler
{
    public class RoomRenovationFileHandler : GenericFileHandler<RoomRenovation>
    {
        private readonly string _datetimeFormat;
        
        public RoomRenovationFileHandler(string path) :base(path)
        {
            _datetimeFormat=FormatStorage.ONLY_DATE_FORMAT;
        }
        


        protected override RoomRenovation ConvertCSVToEntity(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(CSV_DELIMITER.ToCharArray());
            return new RoomRenovation(DateOnly.Parse(tokens[0]),
                DateOnly.Parse(tokens[1]),
                int.Parse(tokens[2]),
                bool.Parse(tokens[3]),
                int.Parse(tokens[4]));
        }

        protected override string ConvertEntityToCSV(RoomRenovation roomRenovation)
        {
            return string.Join(CSV_DELIMITER,
                roomRenovation.StartDate.ToString(_datetimeFormat),
                roomRenovation.EndDate.ToString(_datetimeFormat),
                roomRenovation.Room.Id,
                roomRenovation.IsDone.ToString(),
                roomRenovation.Id
            );
        }
        
    }
}

