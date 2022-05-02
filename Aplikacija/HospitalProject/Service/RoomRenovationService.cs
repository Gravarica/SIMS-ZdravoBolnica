using System.Collections.Generic;
using System.Linq;
using HospitalProject.Model;
using HospitalProject.Repository;
using Model;
using Service;

namespace HospitalProject.Service
{
    public class RoomRenovationService
    {
        private RoomRenovationRepository roomRenovationRepository;
        private AppointmentService _appointmentService;
        private RoomService _roomService;

        public RoomRenovationService(RoomRenovationRepository roomRenovationRepository, AppointmentService appointmentService, RoomService roomService)
        {
            this.roomRenovationRepository = roomRenovationRepository;
            _appointmentService = appointmentService;
            _roomService = roomService;
        }

        public RoomRenovation Create(RoomRenovation roomRenovation)
        {
            return roomRenovationRepository.Insert(roomRenovation);
        }
        
        public IEnumerable<RoomRenovation> getAll()
        {
            var renovations = roomRenovationRepository.GetAll();
            BindRoomsWithRenovations(renovations);
            return renovations;
        }
        
        public void Update(RoomRenovation renovation)
        {
            roomRenovationRepository.Update(renovation);
        }

        public RoomRenovation GetById(int id)
        {
            var renovation = roomRenovationRepository.GetById(id);
            BindRoomWithRenovation(renovation);
            return renovation;
        }
        public void Delete(int id)
        {
            roomRenovationRepository.Delete(id);
        }
        

        private void BindRoomWithRenovation(RoomRenovation renovation)
        {
            SetRoom(renovation);
        }

        private void BindRoomsWithRenovations(IEnumerable<RoomRenovation> renovations)
        {
            renovations.ToList().ForEach(renovation =>SetRoom(renovation));
        }

        private void SetRoom(RoomRenovation renovation)
        {
            renovation.Room = FindRoomById(renovation.Room.Id);
        }

        private Room FindRoomById(int roomId)
        {
            return _roomService.Get(roomId);
        }
        
        
    }
    
}

