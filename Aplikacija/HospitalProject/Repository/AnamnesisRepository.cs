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
        private AnamnesisFileHandler _fileHandler;
        private IEnumerable<Anamnesis> _anamneses;
        private AppointmentRepository _appointmentRepository;

        public AnamnesisRepository(AnamnesisFileHandler anamnesisFileHandler, AppointmentRepository appointmentRepository)
        {
            _fileHandler = anamnesisFileHandler;
            _anamneses = _fileHandler.ReadAll();
            _appointmentRepository = appointmentRepository;
            LinkAnamnesisWithAppointments();
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamneses;
        }

        public Anamnesis GetById(int id)
        {
            return _anamneses.FirstOrDefault(x => x.Id == id);
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

            updateAnamnesis.App = anamnesis.App;
            updateAnamnesis.Date = anamnesis.Date;
            updateAnamnesis.Description = anamnesis.Description;

            _fileHandler.Save(_anamneses);
        }

        private void LinkAnamnesisWithAppointments()
        {
            int id;

            foreach(Anamnesis anamnesis in _anamneses)
            {
                id = anamnesis.App.Id;
                anamnesis.App = _appointmentRepository.Get(id);
            }
        }
    }
}
