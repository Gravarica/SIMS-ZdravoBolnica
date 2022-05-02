using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler
{
    public class RoomRenovationFileHandler
    {
        private readonly string _path;

        private readonly string _delimiter;

        private readonly string _datetimeFormat;

        public RoomRenovationFileHandler(string path, string delimiter, string datetimeFormat)
        {
            _path = path;
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
        }
        
        public IEnumerable<RoomRenovation> ReadAll()
        {
            return File.ReadAllLines(_path)                 
                .Select(ConvertCSVFormatToRoomRenovation)   
                .ToList();
        }

        private RoomRenovation ConvertCSVFormatToRoomRenovation(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new RoomRenovation(DateTime.Parse(tokens[0]),
                DateTime.Parse(tokens[1]),
                int.Parse(tokens[2]),
                bool.Parse(tokens[3]),
                int.Parse(tokens[4]));
        }

        private string ConvertRoomRenovatoinToCSVFormat(RoomRenovation roomRenovation)
        {
            return string.Join(_delimiter,
                roomRenovation.StartDate.ToString(_datetimeFormat),
                roomRenovation.EndDate.ToString(_datetimeFormat),
                roomRenovation.Room.Id,
                roomRenovation.IsDone.ToString(),
                roomRenovation.Id
            );
        }

        public void AppandLineToFile(RoomRenovation roomRenovation)
        {
            string line = ConvertRoomRenovatoinToCSVFormat(roomRenovation);
            File.AppendAllText(_path, line + Environment.NewLine);
        }
        
        public void Save(IEnumerable<RoomRenovation> _renovations)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (RoomRenovation roomRenovation in _renovations)
                {
                    file.WriteLine(ConvertRoomRenovatoinToCSVFormat(roomRenovation));
                }
            }
        }
    }
}

