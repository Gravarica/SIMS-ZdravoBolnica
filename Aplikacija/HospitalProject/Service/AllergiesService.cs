using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Repository;
using Model;

namespace HospitalProject.Service;

public class AllergiesService
{
    private AllergiesRepository _allergiesRepository;
    private MedicalRecordService _medicalRecordService;

    public AllergiesService(AllergiesRepository allergiesRepository)
    {
        _allergiesRepository = allergiesRepository;
    }

    public MedicalRecordService MRService
    {
        set
        {
            _medicalRecordService = value;
        }
    }

    public IEnumerable<Allergies> GetAll()
    {
        return _allergiesRepository.GetAll();
    }

    public Allergies GetById(int id)
    {
        return _allergiesRepository.GetById(id);
    }

    public void Create(Allergies allergies)
    {   
        _allergiesRepository.Insert(allergies);
    }

    public void Delete(int id)
    {
        _allergiesRepository.Delete(id);
    }

    public void Update(Allergies allergies)
    {
        _allergiesRepository.Update(allergies);
    }

    public List<Allergies> GetAllergiesByMedicalRecord(int patientId)
    {
        return _allergiesRepository.GetAllergiesByMedicalRecord(patientId);
    }
    public List<Patient> GetPatientsByAllergy(int allergyID)
    {
        return _allergiesRepository.GetPatientsByAllergy(allergyID);
    }
    
}
