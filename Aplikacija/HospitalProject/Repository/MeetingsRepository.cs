using System.Collections.Generic;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;
using HospitalProject.Service;
using Model;
using Repository;

namespace HospitalProject.Repository;

public class MeetingsRepository
{
        private MeetingsFileHandler _meetingsFileHandler;
        private DoctorService _doctorService;
        private List<Meetings> _meetings;
        private int _meetingMaxId;
    
         public MeetingsRepository(MeetingsFileHandler meetingsFileHandler, DoctorService doctorService)
        {
            this._meetingsFileHandler = meetingsFileHandler;
            this._doctorService = doctorService;
            InstantiateMeetingsList();
        }

        private int GetMaxId()
        {
            return !_meetings.Any() ? 0 : _meetings.Max(meeting => meeting.Id);
        }

        private void InstantiateMeetingsList()
        {
            _meetings = _meetingsFileHandler.ReadAll().ToList();
            BindDoctorsForMeetings();
            _meetingMaxId = GetMaxId();
        }

        

        private void BindDoctorsForMeetings()
        {
            _meetings.ForEach(SetDoctorsForMeeting);
        }
        private void SetDoctorsForMeeting(Meetings meeting)
        {
            List<Doctor> AllDoctorsForMeeting = meeting.Users;
            foreach (Doctor doctor in AllDoctorsForMeeting)
            {
                _doctorService.GetById(doctor.Id);
            }
        }
        public List<Meetings> GetAll()
        {
            return _meetings;
        }

        public Meetings GetById(int id)
        {
            return _meetings.FirstOrDefault(x => x.Id == id);
        }

        
        public Meetings Insert(Meetings meeting)
        {
            meeting.Id = ++_meetingMaxId;
            _meetings.Add(meeting);
            _meetingsFileHandler.AppendLineToFile(meeting);
            return meeting;
        }

       
    }

