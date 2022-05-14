using HospitalProject.Model;
using HospitalProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class SurveyRealizationService
    {
        private SurveyRealizationRepository surveyRealizationRepository;

        public SurveyRealizationService(SurveyRealizationRepository _surveyRealisationRepository)
        {
            surveyRealizationRepository = _surveyRealisationRepository;
        }

        public List<SurveyRealization> GetAll()
        {
            return surveyRealizationRepository.GetAll().ToList();
        }

        public void Create(SurveyRealization surveyRealization)
        {
            surveyRealizationRepository.Create(surveyRealization);
        }
        public int GetMaxId()
        {
            return surveyRealizationRepository.GetMaxId();
        }

    }
}
