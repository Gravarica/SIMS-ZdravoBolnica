using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Service;

namespace HospitalProject.Controller;

public class AllergiesController
{
    private AllergiesService _allergiesService;

    public AllergiesController(AllergiesService allergyService)
    {

        _allergiesService = allergyService;

    }

    public void Create(Allergies allergy)
    {
        _allergiesService.Create(allergy);
    }

    public void Update(Allergies allergy)
    {
        _allergiesService.Update(allergy);
    }
    
    public void Delete(int id)
    {
        _allergiesService.Delete(id);
    }
    
    public Allergies Get(int id)
    {
        return _allergiesService.GetById(id);
    }
      
    public IEnumerable<Allergies> GetAll()
    {
        return _allergiesService.GetAll();
    }
}
