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
    public class PrescriptionRepository
    {

        private PrescriptionFileHandler prescriptionFileHandler;
        private AppointmentRepository appointmentRepository;
        private int prescriptionMaxId;
        private List<Prescription> prescriptions;

        public PrescriptionRepository(PrescriptionFileHandler prescriptionFileHandler, AppointmentRepository appointmentRepository)
        {
            this.prescriptionFileHandler = prescriptionFileHandler;
            this.appointmentRepository = appointmentRepository;
            InstantiatePrescriptionList();
        }

        private void InstantiatePrescriptionList()
        {
            prescriptions = prescriptionFileHandler.ReadAll().ToList();
            prescriptions.ForEach(prescription => SetAppointmentForPrescription(prescription));
            prescriptionMaxId = GetMaxId();
        }

        private int GetMaxId()
        {
            return prescriptions.Count() == 0 ? 0 : prescriptions.Max(prescription => prescription.Id);
        }        

        public IEnumerable<Prescription> GetAll()
        {
            return prescriptions;
        }

        public Prescription GetById(int prescriptionId)
        {
            return prescriptions.FirstOrDefault(prescription => prescription.Id == prescriptionId);
        }

        // Ovde je problem sto lista recepata nema instanciran appointment
        // Jedno resenje je da nemam vezu sa appointmentom nego sa pacijentom direktno
        // Drugo resenje je da nekako instanciram appointment, tj da povezem 
        public IEnumerable<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            return prescriptions.Where(prescription => prescription.Appointment.Patient.Id == patientId);
        }

        // Osiguraj se da je pre ovoga appointment bindovan sa svojim doktorom i pacijentom
        // Za pregled u njegovom repozitorijumu je setovan pacijent i samo njegov id sto je okej
        // Problem je sto cu opet morati da vrsim bind u servisu da bih pristupio pacijentu doktoru i datumu
        private void SetAppointmentForPrescription(Prescription prescription)
        {
            prescription.Appointment = appointmentRepository.GetById(prescription.Appointment.Id);
        }

        public void Insert(Prescription prescription)
        {
            prescription.Id = ++prescriptionMaxId;
            prescriptions.Add(prescription);
            prescriptionFileHandler.AppendLineToFile(prescription);
        }

        public void Delete(int prescriptionId)
        {
            Prescription deletePrescription = GetById(prescriptionId);
            prescriptions.Remove(deletePrescription);
            prescriptionFileHandler.Save(prescriptions);
        }

        
    }
}
