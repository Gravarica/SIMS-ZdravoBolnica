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
        private int _anamnesesMaxId;
        private AppointmentRepository _appointmentRepository;

        public AnamnesisRepository(AnamnesisFileHandler anamnesisFileHandler, AppointmentRepository appointmentRepository)
        {
            _fileHandler = anamnesisFileHandler;
            _anamneses = _fileHandler.ReadAll();
            _appointmentRepository = appointmentRepository;
            _anamnesesMaxId = GetMaxId();
            LinkAnamnesisWithAppointments();
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
            _anamneses.ToList().Add(anamnesis);
            _fileHandler.AppendLineToFile(anamnesis);
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
                anamnesis.App = _appointmentRepository.GetById(id);
            }
        }

        public List<Anamnesis> GetAnamnesesByMedicalRecord(int patientId)
        {
            List<Anamnesis> anamnesisReturnList = new List<Anamnesis>();

            foreach(Anamnesis anamnesis in _anamneses)
            {
                if(patientId == anamnesis.App.Patient.Id)
                {
                    anamnesisReturnList.Add(anamnesis);
                }
            }

            return anamnesisReturnList;
        }
    }
}
