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
    public class UserFileHandler : GenericFileHandler<User>
    {
 

        public UserFileHandler(string path) : base(path) {}

        protected override User ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER.ToCharArray());
            return new User(tokens[0],
                tokens[1],
                EnumConverter.convertStringToUserType(tokens[2]),
                bool.Parse(tokens[3]),
                int.Parse(tokens[4]));
        }

        protected override string ConvertEntityToCSV(User user)
        {
            return string.Join(CSV_DELIMITER,
                user.Username,
                user.Password,
                user.UserType.ToString(),
                user.IsBlocked.ToString(),
                user.MovedAppointmentsCount.ToString());
        }

    }
}
