using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;

namespace HospitalProject.FileHandler
{
    public class EquipmentRelocationFileHandler : GenericFileHandler<EquipmentRelocation>
    {
        
        public EquipmentRelocationFileHandler(string path) : base(path) {}
        

        protected override string ConvertEntityToCSV(EquipmentRelocation relocation)
        {
            return string.Join(CSV_DELIMITER,
                relocation.Id,
                relocation.SourceRoom.Id,
                relocation.DestinationRoom.Id,
                relocation.Equipement.Id,
                relocation.Quantity,
                relocation.Date.ToString()
            );
        }

        protected override EquipmentRelocation ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);
            return new EquipmentRelocation(int.Parse(tokens[0]),
                new Room(int.Parse(tokens[1])),
                new Room(int.Parse(tokens[2])),
                new Equipement(int.Parse(tokens[3])),
                int.Parse(tokens[4]),
                DateOnly.Parse(tokens[5])
            );
        }
        
    }
}

