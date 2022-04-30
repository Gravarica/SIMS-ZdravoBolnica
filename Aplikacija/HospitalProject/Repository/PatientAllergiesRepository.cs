using System.Collections.Generic;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;

namespace HospitalProject.Repository;

public class PatientAllergiesRepository
{
    private List<PatientAllergies> _patientAllergies;
    private IEnumerable<Allergies> _allergies;
    private AllergiesFileHandler _allergiesFileHandler; //fajl sa alergijama
    private MedicalRecordRepository _medicalRecordRepository; 
    private AllergiesRepository _allergiesRepository;//za ubacivanje  karton
    private PatientAllergiesFileHandler _patientAllergiesFileHandler; //fajl koji povezuje pacijenta i alergije
    
    public PatientAllergiesRepository( AllergiesFileHandler allergiesFileHandler, AllergiesRepository allergiesRepository)
    {
        _allergiesFileHandler = allergiesFileHandler;
        _allergies = _allergiesFileHandler.ReadAll();
        _allergiesRepository = allergiesRepository;
        //LinkAnamnesisWithAppointments();
    }
    
    public List<PatientAllergies> GetAll()
    {
        return _patientAllergies;
    }
    //1 34
    //1 45
    //2 9

    public List<Allergies> OnePatientAllergies(int id)
    {
        List<int> IDalergije;
        List<PatientAllergies> p = GetAll();
        foreach (id in p)
        {
            if (id == p.patientID)
            {
                int a = p.allergyID;
                IDalergije.Add(a);
            }

        }
       

        
        return 
    }
        
    public PatientAllergies Insert(PatientAllergies patientAllergies)
    {
        _patientAllergiesFileHandler.AppendLineToFile(patientAllergies);
        return patientAllergies;
    }

    public void Delete(int id)
    {
        PatientAllergies deletePatientAllergies = OnePatientAllergies(id);
        _patientAllergies.Remove(deletePatientAllergies);
        _patientAllergiesFileHandler.Save(_patientAllergies);
    }

    public void Update(PatientAllergies allergyID)
    {
        PatientAllergies updatePatientAllergies = OnePatientAllergies(PatientAllergies._allergyID);
        updatePatientAllergies._allergyID = PatientAllergies.allergyID;
    }

   /* public void AddNewAllergiesToMedicalRecord(Allergies allergy)
    {
        MedicalRecord updateMedicalRecord = OnePatientAllergies(allergy.App.Patient.MedicalRecordId);
        updateMedicalRecord.Allergies.Add(allergy);
        
    } */

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}