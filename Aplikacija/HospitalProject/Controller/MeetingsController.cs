using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Service;

namespace HospitalProject.Controller;

public class MeetingsController
{
    private MeetingsService _meetingsService;
    public MeetingsController(MeetingsService meetingsService)
    {
        this._meetingsService = meetingsService;
    }
    
    public Meetings Create(Meetings meeting)
    {
        return _meetingsService.Create(meeting);
    }

    public List<Meetings> GetAll()
    {
        return _meetingsService.GetAll();
    }
    
}