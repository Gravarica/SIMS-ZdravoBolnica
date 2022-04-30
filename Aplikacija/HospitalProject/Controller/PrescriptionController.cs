﻿using HospitalProject.Model;
using HospitalProject.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class PrescriptionController
    {

        PrescriptionService prescriptionService;

        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.prescriptionService=prescriptionService;
        }

        public List<Prescription> GetAll()
        {
            return prescriptionService.GetAll();
        }

        public List<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            return prescriptionService.GetPrescriptionsForPatient(patientId);
        }

        public void Create(Appointment appointment, DateOnly startDate, DateOnly endDate, int interval, string description)
        {
            Prescription prescription = new Prescription(appointment, startDate, endDate, interval, description);
            prescriptionService.Create(prescription);
        }

        public void Delete(int prescriptionId)
        {
            prescriptionService.Delete(prescriptionId);
        }
    }
}
