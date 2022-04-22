// File:    AppointmentRepository.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:02:45
// Purpose: Definition of Class AppointmentRepository

using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{

public class AppointmentRepository
{
    private readonly string _path;
    private readonly string _delimiter;
    private readonly string _datetimeFormat;
    private int _appointmentMaxId;
    private List<Appointment> _appointments = new List<Appointment>();

        public AppointmentRepository(string path, string delimiter, string datetimeFormat) {
            _path = path;
            _delimiter = delimiter;
            _datetimeFormat = datetimeFormat;
            _appointmentMaxId = GetMaxId(ReadAll());
            _appointments = ReadAll().ToList();
        }

        public List<Appointment> Appointments { get; set; }

        private int GetMaxId(IEnumerable<Appointment> appointments) {
            return appointments.Count() == 0 ? 0 : appointments.Max(appointment => appointment.Id);
        }

    public Appointment Insert(Appointment appointment)
    {
            appointment.Id = ++_appointmentMaxId;
            AppendLineToFile(_path, ConvertAppointmentToCSVFormat(appointment));
            return appointment;
    }

    public Appointment Get(int id)
    {
        return _appointments.FirstOrDefault(x => x.Id == id); // Daj mi prvi na koji naletis koji se poklapa ILI daj mi default(null)
    }

    public IEnumerable<Appointment> ReadAll()
    {
        return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
               .Select(ConvertCSVFormatToAppointment)   // 1 | 20.01.2000 12:15| 20 | 2 | 3 => app1(...) 
               .ToList();
    }

    public IEnumerable<Appointment> GetAll()
        {
            return _appointments;
        }

    public void Delete(int id)
    {
        Appointment removeAppointment = Get(id);
        _appointments.Remove(removeAppointment);
        Save();
    }

    public void Save()
    {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Appointment appointment in _appointments)
                {
                    file.WriteLine(ConvertAppointmentToCSVFormat(appointment));
                }
            }
    }

    public void Update(Appointment appointment)
    {

            Appointment updatedAppointment = new Appointment();

            foreach (Appointment app in _appointments)
            {
                if (appointment.Id == app.Id) {
                    updatedAppointment = app;
                    break;
                }
            }

            updatedAppointment.Date = appointment.Date;
            updatedAppointment.Duration = appointment.Duration;
            updatedAppointment.PatientId = appointment.PatientId;
            updatedAppointment.RoomID = appointment.RoomID;

            Save();

    }

    private Appointment ConvertCSVFormatToAppointment(string CSVFormat)
    {
        string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
        return new Appointment(int.Parse(tokens[0]), 
                               DateTime.Parse(tokens[1]),
                               int.Parse(tokens[2]),
                               int.Parse(tokens[3]), 
                               int.Parse(tokens[4]));
    }

    private string ConvertAppointmentToCSVFormat(Appointment appointment)
    {
        return string.Join(_delimiter,
            appointment.Id,
            appointment.Date.ToString(_datetimeFormat),
            appointment.Duration,
            appointment.Patient.Id,
            appointment.Doctor.Id);
    }

    private void AppendLineToFile(string path, string line)
    {
        File.AppendAllText(path, line + Environment.NewLine);
    }

}

}