using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class UserFileHandler
    {
        private string _path;

        private string _delimiter;

        public UserFileHandler(String path, String delimiter)
        {
            _path = path;
            _delimiter = delimiter;
        }

        public IEnumerable<User> ReadAll()
        {
            return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                   .Select(ConvertCSVFormatToUser)   // 1 | 20.01.2000 12:15| 20 | 2 | 3 => app1(...) 
                   .ToList();
        }

        public User ConvertCSVFormatToUser(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new User(tokens[0],
                            tokens[1],
                            convertStringToUserType(tokens[2]));

        }

        public string ConvertUserToCSVFormat(User user)
        {
            return string.Join(_delimiter,
            user.Username,
            user.Password,
            user.UserType.ToString());
        }

        public void AppendLineToFile(User user)
        {
            string line = ConvertUserToCSVFormat(user);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<User> users)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (User user in users)
                {
                    file.WriteLine(ConvertUserToCSVFormat(user));
                }
            }
        }

        private UserType convertStringToUserType(string userType)
        {
            if(userType.Equals("PATIENT"))
            {
                return UserType.PATIENT;
            } 
            else if(userType.Equals("DOCTOR"))
            {
                return UserType.DOCTOR;
            }
            else if(userType.Equals("SECRETARY"))
            {
                return UserType.SECRETARY;
            }
            else
            {
                return UserType.WARDEN;
            }
        }
    }
}
