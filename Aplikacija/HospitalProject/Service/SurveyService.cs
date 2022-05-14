using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class SurveyService
    {
        private SurveyRepository surveyRepository;
        
        

        


        public SurveyService(SurveyRepository _surveyRepository)
        {
            surveyRepository = _surveyRepository;
            
        }

        public int GetMaxId()
        { 
            return surveyRepository.GetMaxId();    
        }

       public List<Survey> GetAll()
        {
            return surveyRepository.GetAll().ToList();

        }



        
    }
}
