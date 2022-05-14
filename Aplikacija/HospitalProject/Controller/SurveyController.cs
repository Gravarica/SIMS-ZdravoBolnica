using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class SurveyController
    {
        private SurveyService surveyService;


        public SurveyController(SurveyService _surveyService)
        {
            surveyService = _surveyService;
        }

        public List<Survey> GetAll()
        {
            return surveyService.GetAll();
        }

        public int GetMaxId()
        {
            return surveyService.GetMaxId();
        }


    }
}
