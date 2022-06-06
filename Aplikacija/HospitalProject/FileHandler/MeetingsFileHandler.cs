using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Model;
using Model;
using Syncfusion.Windows.Controls;

namespace HospitalProject.FileHandler;

public class MeetingsFileHandler
{
    private string _path;
    private string _delimiter;
    private string _dateTimeFormat;
    
    private const string DOCTOR_CSV = "-";
    
    public MeetingsFileHandler(string path, string delimiter, string dateTimeFormat)
    {
        _path = path;
        _delimiter = delimiter;
        _dateTimeFormat = dateTimeFormat;
    }
    
    private Meetings ConvertCSVFormatToMeetings(string csvString)
    {
        string[] tokens = csvString.Split(_delimiter);

        return new Meetings(int.Parse(tokens[0]),
            int.Parse(tokens[1]), 
            GetDoctorsFromCSV(tokens[2]),
            tokens[3].ToDateTime(),
            int.Parse(tokens[4]),
            bool.Parse(tokens[5]));
            

    }
    
    private List<Doctor> GetDoctorsFromCSV(string CSVToken)
    {
        string[] doctorIds = CSVToken.Split(DOCTOR_CSV);
        List<Doctor> doctors = new List<Doctor>();
        int lenght = doctorIds.Length;

        for(int i = 0; i < lenght; i++)
        {
            AddDoctorToList(doctors, int.Parse(doctorIds[i]));
        }

        return doctors;
    }
    
    private void AddDoctorToList(List<Doctor> doctors, int doctorIds)
    {
        Doctor doctor = new Doctor(doctorIds);
        doctors.Add(doctor);
    }
    public IEnumerable<Meetings> ReadAll()
    {
        return File.ReadAllLines(_path)                 
            .Select(ConvertCSVFormatToMeetings)   
            .ToList();
    }
    private string ConvertMeetingsToCSVFormat(Meetings meeting)
    {
        return string.Join(_delimiter,
            meeting.Id,
            meeting.RoomId,
            ConvertDoctorsToCSV(meeting),
            meeting.DateTime.ToString(),
            meeting.Duration,
            meeting.AddedWarden.ToString());
    }
    public void AppendLineToFile(Meetings meeting)
    {
        string line = ConvertMeetingsToCSVFormat(meeting);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    public void Save(IEnumerable<Meetings> _meetings)
    {
        using (StreamWriter file = new StreamWriter(_path))
        {
            foreach (Meetings meeting in _meetings)
            {
                file.WriteLine(ConvertMeetingsToCSVFormat(meeting));
            }
        }
    }
    private string ConvertDoctorsToCSV(Meetings meeting)
    {
        List<Doctor> doctors = meeting.Users;
        string CSVOutput = "";

            
            foreach (Doctor doctor in doctors)
            {
                string id = doctor.Id.ToString();
                CSVOutput = CSVOutput + id + "-";
            }
            
            CSVOutput = CSVOutput.Remove(CSVOutput.Length - 1,1);
            
        return CSVOutput;
    }
}