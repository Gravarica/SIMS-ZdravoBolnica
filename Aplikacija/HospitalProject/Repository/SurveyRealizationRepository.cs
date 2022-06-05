using HospitalProject.FileHandler;
using HospitalProject.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class SurveyRealizationRepository
    {
        private IHandleData<SurveyRealization> surveyRealizationFileHandler;
        private PatientRepository patientRepository;
        private SurveyRepository surveyRepository;
        private AnswerRepository answerRepository;
        private DoctorRepository doctorRepository;

        private List<SurveyRealization> surveyRealizations;
        private int maxId;



        public SurveyRealizationRepository(PatientRepository patientRepository, SurveyRepository surveyRepository, AnswerRepository answerRepository)
        {
            surveyRealizationFileHandler = new SurveyRealizationFileHandler(FilePathStorage.SURVEY_REALIZATIONS_FILE);

            InstantiateRepositories(patientRepository, surveyRepository, answerRepository);
            InstantiateSurveyRealizationList();
        }

        private void InstantiateRepositories(PatientRepository _patientRepository,SurveyRepository _surveyRepository, AnswerRepository _answerRepository)
        {
            this.patientRepository = _patientRepository;
            this.surveyRepository = _surveyRepository;
            this.answerRepository = _answerRepository;

        }

        private void InstantiateSurveyRealizationList()
        {
            surveyRealizations = GetAll();
            maxId = GetMaxId();
            BindIdForPatient();
            BindSurveyWithRealization();
        }

        private void BindIdForPatient()
        {
            foreach(SurveyRealization surveyRealization in surveyRealizations)
            {
                surveyRealization.Patient = patientRepository.GetById(surveyRealization.Patient.Id);
            }
        }

        private void BindSurveyWithRealization()
        {
            surveyRealizations.ForEach(SetSurveyForRealization);
        }

        private void SetSurveyForRealization(SurveyRealization surveyRealization)
        {
            surveyRealization.Survey = surveyRepository.GetById(surveyRealization.Survey.Id);
        }

        public List<SurveyRealization> GetAll()
        {
           return surveyRealizationFileHandler.ReadAll().ToList();
        }

        public void Create(SurveyRealization surveyRealization)
        {
            surveyRealization.Id = ++maxId;
            surveyRealizationFileHandler.SaveOneEntity(surveyRealization);
        }

        public int GetMaxId()
        {
            return surveyRealizations.Count() == 0 ? 0 : surveyRealizations.Max(surveyRealization => surveyRealization.Id);
        }

    }
}
