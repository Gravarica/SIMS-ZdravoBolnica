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

        public void UpdateRoomsEquipment(int source, int destination, int equipmentId,int quantity)
        {
            Room selectedRoom = new Room(source);
            Room selectedDestinationRoom = new Room(destination);
            Room oldSource = _roomRepository.Get(source);
            Room oldDestination = _roomRepository.Get((destination));

            selectedRoom._number = oldSource._number;
            selectedRoom._floor = oldSource._floor;
            selectedRoom._roomType = oldSource._roomType;

            selectedDestinationRoom._number = oldDestination._number;
           selectedDestinationRoom._floor = oldDestination._floor;
            selectedDestinationRoom._roomType = oldDestination._roomType;



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

                if (eq.Quantity != 0)
                {
                    selectedRoom.Equipment.Add(eq);
                }
                
            }

            
            
             
            _roomRepository.Update(selectedRoom);
            _roomRepository.Update(selectedDestinationRoom);

        }

    }
}