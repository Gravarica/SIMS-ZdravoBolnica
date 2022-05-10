using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.DataTransferObjects
{
    public class NewRequestDTO
    {
        private DateTime submissionDate;
        private Doctor doctor;
        private DateTime startDate;
        private DateTime endDate;
        private string description;
        private bool isUrgent;

        public NewRequestDTO(DateTime submissionDate, Doctor doctor, DateTime startDate, DateTime endDate, string description, bool isUrgent)
        {
            this.submissionDate=submissionDate;
            this.doctor=doctor;
            this.startDate=startDate;
            this.endDate=endDate;
            this.description=description;
            this.isUrgent=isUrgent;
        }

        public DateTime SubmissionDate
        {
            get
            {
                return submissionDate;
            }
        }
        public Doctor Doctor
        { 
            get
            {
                return doctor;
            }
        }  
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
        public bool IsUrgent
        {
            get
            {
                return isUrgent;
            }
        }


    }
}
