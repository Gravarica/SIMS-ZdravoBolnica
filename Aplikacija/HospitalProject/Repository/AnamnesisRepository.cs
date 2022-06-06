using HospitalProject.Exception;
using HospitalProject.FileHandler;
using HospitalProject.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class AnamnesisRepository
    {
        private IHandleData<Anamnesis> _fileHandler;
        private List<Anamnesis> _anamneses;
        private int _anamnesesMaxId;
    

        public AnamnesisRepository()
        {
            _fileHandler = new AnamnesisFileHandler(FilePathStorage.ANAMNESIS_FILE);
            _anamneses = _fileHandler.ReadAll().ToList();
            _anamnesesMaxId = GetMaxId();
        }

        public int GetMaxId()
        {
            return _anamneses.Count() == 0 ? 0 : _anamneses.Max(appointment => appointment.Id);
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamneses;
        }

        public Anamnesis GetById(int id)
        {
            return _anamneses.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Anamnesis anamnesis)
        {
            anamnesis.Id = ++_anamnesesMaxId;
            _anamneses.Add(anamnesis);
            _fileHandler.SaveOneEntity(anamnesis);
        }

        public void Delete(int id)
        {
            Anamnesis deleteAnamnesis = GetById(id);
            _anamneses.ToList().Remove(deleteAnamnesis);
            _fileHandler.Save(_anamneses);
        }

        public void Update(Anamnesis anamnesis)
        {
            Anamnesis updateAnamnesis = GetById(anamnesis.Id);
            updateAnamnesis.Description = anamnesis.Description;

            _fileHandler.Save(_anamneses);
        }

        public List<Anamnesis> GetAnamnesesByMedicalRecord(int patientId)
        {
            return _anamneses.Where(anamnesis => patientId == anamnesis.App.Patient.Id).ToList();
        }
    }
}
