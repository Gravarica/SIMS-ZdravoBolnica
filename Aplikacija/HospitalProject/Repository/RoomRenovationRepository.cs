using System.Collections.Generic;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;

namespace HospitalProject.Repository;

public class RoomRenovationRepository
{
    private RoomRenovationFileHandler _renovationFileHandler;
    private int _renovationsMaxId;
    private List<RoomRenovation> _renovations = new List<RoomRenovation>();

    public RoomRenovationRepository(RoomRenovationFileHandler roomRenovationFileHandler)
    {
        InitialiseData(roomRenovationFileHandler);
        _renovationsMaxId = GetMaxId();
    }

    private void InitialiseData(RoomRenovationFileHandler roomRenovationFileHandler)
    {
        _renovationFileHandler = roomRenovationFileHandler;
        _renovations = _renovationFileHandler.ReadAll().ToList();
    }
    private int GetMaxId() {
        return _renovations.Count() == 0 ? 0 : _renovations.Max(renovation => renovation.Id);
    }

    public RoomRenovation Insert(RoomRenovation roomRenovation)
    {
        roomRenovation.Id = ++_renovationsMaxId;
        _renovationFileHandler.AppandLineToFile(roomRenovation);
        _renovations.Add(roomRenovation);
        return roomRenovation;
    }

    public RoomRenovation GetById(int id)
    {
        return _renovations.FirstOrDefault(x => x.Id == id);
    }
    
    public IEnumerable<RoomRenovation> GetAllUnfinishedAppointments()
    {
        return _renovations.Where(x=> x.IsDone==false);  
    }
    
    public IEnumerable<RoomRenovation> GetAll()
    {
        return _renovations;
    }
    
    public void Delete(int id)
    {
        RoomRenovation roomRenovation = GetById(id);
        _renovations.Remove(roomRenovation);
        _renovationFileHandler.Save(_renovations);
    }
    
    public void Update(RoomRenovation renovation)
    {
        RoomRenovation updatedRenovation = GetById(renovation.Id);

        updatedRenovation.StartDate = renovation.StartDate;
        updatedRenovation.EndDate = renovation.EndDate;
        updatedRenovation.Room.Id = renovation.Room.Id;

        _renovationFileHandler.Save(_renovations);
    }
    
    public void SetRenovationFinished(RoomRenovation renovation)
    {
        RoomRenovation updatedrRenovation = GetById(renovation.Id);

        updatedrRenovation.IsDone = true;

        _renovationFileHandler.Save(_renovations);
    }
}