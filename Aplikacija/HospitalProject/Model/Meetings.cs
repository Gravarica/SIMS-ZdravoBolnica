using System;
using System.Collections.Generic;
using Model;

namespace HospitalProject.Model;

public class Meetings : ViewModelBase
{
    private int _id;
    private int _roomId;
    private int _duration;
    private List<Doctor> _doctors;
    private DateTime _datetime;
    private bool _addedWarden;
    
    public Meetings(int id)
    {
        _doctors = new List<Doctor>();
        Id = id;
    }
    
    public Meetings(int id, int room, List<Doctor> users, DateTime datetime, int duration,  bool addedWarden)
    {
        Id = id;
        RoomId = room;
        DateTime = datetime;
        Duration = duration;
        Users = users;
        AddedWarden = addedWarden;
    }
    public int Id 
    { 
        get { return _id; }
        set {  _id = value;
                OnPropertyChanged(nameof(Id)); }
    }
    
    public bool AddedWarden 
    { 
        get { return _addedWarden; }
        set {  _addedWarden = value;
            OnPropertyChanged(nameof(AddedWarden)); }
    }
    public List<Doctor> Users
    {
        get { return _doctors; }
        set { _doctors = value;
                OnPropertyChanged(nameof(Users));}
    }
    
    

    public DateTime DateTime 
    { 
        get { return _datetime; }
        set { _datetime = value;
            OnPropertyChanged(nameof(DateTime)); }
    }
    public int Duration 
    { 
        get {  return _duration; }
        set { _duration = value;
                OnPropertyChanged(nameof(Duration)); }
    }
    public int RoomId
    {
        get { return _roomId;  }
        set { _roomId = value;
            OnPropertyChanged(nameof(RoomId)); }
    }
}