using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Repository;
using Service;

namespace HospitalProject.Service;

public class MeetingsService
{
    private MeetingsRepository _meetingsRepository;
    
    public MeetingsService(MeetingsRepository meetingsRepository)
    {
        this._meetingsRepository = meetingsRepository;
    }
    
    public Meetings Create(Meetings meeting)
    {
         return _meetingsRepository.Insert(meeting); 
    }

    public List<Meetings> GetAll()
    {
        return _meetingsRepository.GetAll();
    }
}