using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class SurveyRealizationController
    {
        private SurveyRealizationService surveyRealizationService;

        public SurveyRealizationController(SurveyRealizationService _surveyRealizationService)
        {
            surveyRealizationService = _surveyRealizationService;
        }

        public List<SurveyRealization> GetAll()
        {
            return surveyRealizationService.GetAll().ToList();
        }

        public void Create(SurveyRealization surveyRealization)
        {
            surveyRealizationService.Create(surveyRealization);
        }

        public int GetMaxId()
        {
            return surveyRealizationService.GetMaxId();
        }

    }
}
