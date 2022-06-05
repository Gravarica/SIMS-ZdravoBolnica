using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class GenericFileHandler<T> : IHandleData<T>
    {
        protected string path;

        protected static string CSV_DELIMITER = "|";

        public GenericFileHandler(string path)
        {
            this.path = path;
        }
        public void AppendLineToFile(T entity)
        {
            string line = ConvertEntityToCSV(entity);
            File.AppendAllText(path, line + Environment.NewLine);
        }

        public IEnumerable<T> ReadAll()
        {
            return File.ReadAllLines(path)                 
                .Select(ConvertCSVToEntity)    
                .ToList();
        }

        protected virtual T ConvertCSVToEntity(string csv)
        {
            throw new NotImplementedException();
        }

        protected virtual string ConvertEntityToCSV(T entity)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<T> entities)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (T entity in entities)
                {
                    file.WriteLine(ConvertEntityToCSV(entity));
                }
            }
        }

        public void SaveOneEntity(T entity)
        {
            AppendLineToFile(entity);
        }
    }
}
