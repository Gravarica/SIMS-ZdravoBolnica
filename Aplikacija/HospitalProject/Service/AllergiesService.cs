using System.Collections.Generic;
using System.Linq;
using HospitalProject.Model;
using HospitalProject.Repository;

namespace HospitalProject.Service;

public class AllergiesService
{
    private AllergiesRepository _allergiesRepository;
    private MedicalRecordRepository _medicalRecordRepository;
    
    private EquipementRepository _equipementRepository;

    public AllergiesService(AllergiesRepository allergiesRepository, MedicalRecordRepository medicalRecordRepository, EquipementRepository equipmentRepository)
    {
        _allergiesRepository = allergiesRepository;
        _medicalRecordRepository = medicalRecordRepository;
        _equipementRepository = equipmentRepository;
    }


    public IEnumerable<Allergies> GetAll()
    {
        return _allergiesRepository.GetAll();
    }

    public Allergies GetById(int id)
    {
        return _allergiesRepository.GetById(id);
    }

    public Allergies Create(Allergies allergies)
    {   
        _allergiesRepository.Insert(allergies);
        return allergies;
    }

    public void Delete(int id)
    {
        DeleteAllergenFromEquipment(GetById(id));
        DeleteAllergenFromMedicalRecords(id);
        _allergiesRepository.Delete(id);
    }

    private void DeleteAllergenFromMedicalRecords(int allergenID) {
        
        foreach (MedicalRecord medicalRecord in _medicalRecordRepository.GetAll())
        {
            _medicalRecordRepository.RemoveAllergiesFromMedicalRecord(GetById(allergenID), medicalRecord.Patient );
        }
    }

    private void DeleteAllergenFromEquipment(Allergies allergen)
    {
        foreach (Equipement medicine in _equipementRepository.GetAllMedicine().ToList())
        {
            _equipementRepository.RemoveAllergiesFromMedicine(allergen);
        }
    }
    

    public void Update(Allergies allergies)
    {
        _allergiesRepository.Update(allergies);
    }
    
}
