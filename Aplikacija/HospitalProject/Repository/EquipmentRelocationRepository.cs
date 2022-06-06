using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;

namespace HospitalProject.Repository;

public class EquipmentRelocationRepository
{
    private EquipmentRelocationFileHandler relocationFileHandler;
    private List<EquipmentRelocation> relocations;
    private int relocationsMaxId;

    public EquipmentRelocationRepository()
    {
        relocationFileHandler = new EquipmentRelocationFileHandler(FilePathStorage.EQUIPMENT_RELOCATION_FILE);
        InstantiateRelocationList();
    }

    private void InstantiateRelocationList()
    {
        relocations = relocationFileHandler.ReadAll().ToList();
        relocationsMaxId = GetMaxId();
    }
    
    private int GetMaxId()
    {
        return relocations.Count() == 0 ? 0 : relocations.Max(relocation => relocation.Id);
    }
    
    public List<EquipmentRelocation> GetAll()
    {
        return relocations;
    }
    
    public EquipmentRelocation Insert(EquipmentRelocation relocation)
    {
        relocation.Id = ++relocationsMaxId;
        relocationFileHandler.AppendLineToFile(relocation);
        relocations.Add(relocation);
        return relocation;
    }
    
    public void Delete(int id)
    {
        EquipmentRelocation removeRoom = GetById(id);
        relocations.Remove(removeRoom);
        relocationFileHandler.Save(relocations);
    }
      
    
    
    public EquipmentRelocation GetById(int id)
    {
        return relocations.FirstOrDefault(x => x.Id == id); 
    }
    
    
}