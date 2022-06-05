using HospitalProject.Model;
using HospitalProject.Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Linq;

namespace HospitalProject.Service
{
    public class AnamnesisService
    {

        private AnamnesisRepository _anamnesisRepository;
        private AppointmentService _appointmentService;
        public AnamnesisService(AnamnesisRepository anamnesisRepository, AppointmentService appointmentService)
        {
            _anamnesisRepository = anamnesisRepository;
            _appointmentService = appointmentService;
            LinkAnamnesisWithAppointments();
        }


        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisRepository.GetAll();
        }

        public Anamnesis GetById(int id)
        {
            return _anamnesisRepository.GetById(id);
        }

        public void Create(Anamnesis anamnesis)
        {
            _appointmentService.SetAppointmentFinished(anamnesis.App);
            _anamnesisRepository.Insert(anamnesis);
        }

        public void Delete(int id)
        {
            _anamnesisRepository.Delete(id);
        }

        public void Update(Anamnesis anamnesis)
        {
            _anamnesisRepository.Update(anamnesis);
        }

        public List<Anamnesis> GetAnamnesesByMedicalRecord(int patientId)
        {
            return _anamnesisRepository.GetAnamnesesByMedicalRecord(patientId);
        }

        private void LinkAnamnesisWithAppointments()
        {
            GetAll().ForEach(SetAppointmentForAnamnesis);
        }

        private void SetAppointmentForAnamnesis(Anamnesis anamnesis)
        {
            anamnesis.App = _appointmentService.GetById(anamnesis.App.Id);
        }

    }
}
