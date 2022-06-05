using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public interface IHandleData<T>
    {
        public IEnumerable<T> ReadAll();

        public void Save(IEnumerable<T> entities);

        public void SaveOneEntity(T entity);
    }
}
