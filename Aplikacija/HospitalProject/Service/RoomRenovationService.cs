using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
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
        
        public List<RoomRenovation> GenerateAvailableRenovationAppointments(DateOnly searchStartDate, DateOnly
            searchEndDate, Room room,int duration)
        {
            List<RoomRenovation> availableRenovationAppointments = new List<RoomRenovation>();
            List<RoomRenovation> allRenovations = GenerateAllRenovationAppointments(searchStartDate, searchEndDate, room, duration);
            foreach (RoomRenovation rr in allRenovations)
            {
                if (_appointmentService.RoomHasAppointmentByDay(rr.StartDate, rr.EndDate, rr.Room)
                    && RoomHasReservationsByDay(rr.StartDate, rr.EndDate, rr.Room))
                {
                    availableRenovationAppointments.Add(rr);
                }
            }
            return availableRenovationAppointments;
        }
        

        private bool RoomHasReservationsByDay(DateOnly startDate, DateOnly endDate, Room room)
        {
            var allReservations = getAll();
            foreach (RoomRenovation roomRenovation in allReservations)
            {
                if ((startDate >= roomRenovation.StartDate && startDate <= roomRenovation.EndDate && roomRenovation.Room.Id == room.Id))
                {
                    return false;
                }
                if (endDate >= roomRenovation.StartDate && endDate <= roomRenovation.EndDate && roomRenovation.Room.Id == room.Id)
                {
                    return false;
                }
            }

            return true;
        }
        
        
        private List<RoomRenovation> GenerateAllRenovationAppointments(DateOnly searchStartDate, DateOnly
            searchEndDate, Room room, int duration)
        {
            List<RoomRenovation> generatedRenovations = new List<RoomRenovation>();
            while (searchStartDate.AddDays(duration) <= searchEndDate)
            {
                RoomRenovation renovation = new RoomRenovation(searchStartDate, searchStartDate.AddDays(duration), room);
                generatedRenovations.Add(renovation);
                searchStartDate = searchStartDate.AddDays(1);
            }

            return generatedRenovations;
        }
        
        
    }
    
}

