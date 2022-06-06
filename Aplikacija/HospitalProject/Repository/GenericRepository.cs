using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.FileHandler;

namespace HospitalProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IHandleData<T> entityFileHandler;
        private List<T> entities;

        public GenericRepository(IHandleData<T> dataHandler)
        {
            entityFileHandler = dataHandler;
        }

        public List<T> GetAll()
        {
            return entities;
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
