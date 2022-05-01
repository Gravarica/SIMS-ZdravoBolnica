using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Model;
using HospitalProject.View.Converter;
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

        public void UpdateRoomsEquipment(EquipmentRoomModel source, EquipmentRoomModel destination, int equipmentId,int quantity)
        {
            Room selectedRoom = RoomEquipmentConverter.ConvertRoomEquipmentToRoom(source);
            Room selectedDestinationRoom = RoomEquipmentConverter.ConvertRoomEquipmentToRoom(destination);
            Room oldSource = _roomRepository.Get(source.RoomId);
            Room oldDestination = _roomRepository.Get((destination.RoomId));
            
            selectedRoom.Floor = oldSource.Floor;
            selectedRoom.RoomType = oldSource.RoomType;

            selectedDestinationRoom.Floor = oldDestination.Floor;
            selectedDestinationRoom.RoomType = oldDestination.RoomType;



            Boolean contains = false;
            foreach (Equipement eq in oldDestination.Equipment)
            {
                
                if (equipmentId == eq.Id)
                {

                    contains = true;
                }
                
            }

            if (contains)
            {
                foreach (Equipement eq in oldDestination.Equipment)
                {
                    if (equipmentId == eq.Id)
                    {

                        eq.Quantity += quantity;
                    }
                    selectedDestinationRoom.Equipment.Add(eq);
                }
            }
            else
            {
                selectedDestinationRoom.Equipment.Add(new Equipement(equipmentId, quantity));
            }
            
            foreach (Equipement eq in oldSource.Equipment)
            
            {
                if (equipmentId == eq.Id)
                {
                    eq.Quantity -= quantity;
                }
                selectedRoom.Equipment.Add(eq);
            }

            
            
             
            _roomRepository.Update(selectedRoom);
            _roomRepository.Update(selectedDestinationRoom);

        }

    }
}