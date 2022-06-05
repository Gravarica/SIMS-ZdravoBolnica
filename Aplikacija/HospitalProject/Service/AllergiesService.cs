using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Repository;

namespace HospitalProject.Service;

public class AllergiesService
{
    private AllergiesRepository _allergiesRepository;

    public AllergiesService(AllergiesRepository allergiesRepository)
    {
        _allergiesRepository = allergiesRepository;
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
        _allergiesRepository.Delete(id);
    }

    public void Update(Allergies allergies)
    {
        _allergiesRepository.Update(allergies);
    }
    
}
