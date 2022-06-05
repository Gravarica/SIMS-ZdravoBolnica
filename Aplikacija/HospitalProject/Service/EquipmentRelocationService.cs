using System;
using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Repository;
using HospitalProject.View.Converter;
using HospitalProject.View.WardenForms.ViewModels;
using Model;

namespace HospitalProject.Service
{
    public class EquipmentRelocationService
    {

        private EquipmentRelocationRepository equipmentRelocationRepository;
        private RoomService roomService;

        public EquipmentRelocationService(EquipmentRelocationRepository equipmentRelocationRepository,RoomService rs)
        {
            this.equipmentRelocationRepository = equipmentRelocationRepository;
            roomService = rs;
        }
        
        public List<EquipmentRelocation> GetAll()
        {
            var relocations = equipmentRelocationRepository.GetAll();
            return relocations;
        }

        public void Delete(int id)
        {
            equipmentRelocationRepository.Delete(id);
        }

        public EquipmentRelocation Create(EquipmentRelocation relocation)
        {
            return equipmentRelocationRepository.Insert(relocation);
        }

        public List <EquipmentRelocation> ExecuteRelocations()
        {
            IEnumerable<EquipmentRelocation> allRelocations = GetAll();
            List<EquipmentRelocation> todaysRelocations = new List<EquipmentRelocation>();
            foreach (EquipmentRelocation er in allRelocations)
            {
                if (er.Date == new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day))
                {
                    todaysRelocations.Add(er);
                    roomService.UpdateRoomsEquipment(er.SourceRoom.Id,er.DestinationRoom.Id,er.Equipement.Id,er.Quantity);
                }
                
            }
            return todaysRelocations;
        }
    }
}

