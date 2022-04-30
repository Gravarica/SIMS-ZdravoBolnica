using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.View.WardenForms.ViewModels;

namespace HospitalProject.Service
{

    public class RoomService
    {
        
        private RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public Room Create(Room room)
        {
            return _roomRepository.Insert(room);
        }

        public IEnumerable<Room> getAll()
        {
            return _roomRepository.GetAll();
        }

        public Room getRoomById(IEnumerable<Room> rooms, int roomId)
        {
            return rooms.SingleOrDefault(room => room._id == roomId);
        }
        public Room Get(int id)

        {
            return _roomRepository.Get(id);
        }
        
        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }

        public IEnumerable<EquipmentRoomModel> GenerateEquipmentRooms(int equipmentId)
        {
            return _roomRepository.GetByEquipment(equipmentId);
        }
        public IEnumerable<EquipmentRoomModel> GenerateAllEquipmentRooms(int equipmentId)
        {
            return _roomRepository.GetAllWithEquipment(equipmentId);
        }

    }
}