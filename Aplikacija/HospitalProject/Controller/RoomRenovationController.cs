using System;
using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Service;
using Model;

namespace HospitalProject.Controller
{
    public class RoomRenovationController
    {
        private RoomRenovationService _roomRenovationService;

        public RoomRenovationController(RoomRenovationService roomRenovationService)
        {
            _roomRenovationService = roomRenovationService;
        }

        public RoomRenovation Create(RoomRenovation renovation)
        {
            return _roomRenovationService.Create(renovation);
        }

        public IEnumerable<RoomRenovation> GetAll()
        {
            return _roomRenovationService.getAll();
        }

        public void Update(RoomRenovation renovation)
        {
            _roomRenovationService.Update(renovation);
        }

        public void Delete(int id)
        {
            _roomRenovationService.Delete(id);
        }

        public List<RoomRenovation> GenerateAvailableRenovationAppointments(DateOnly searchStartDate, DateOnly
            searchEndDate, Room room, int duration)
        {
            return _roomRenovationService.GenerateAvailableRenovationAppointments(searchStartDate, searchEndDate, room,
                duration);
        }
    }
}

