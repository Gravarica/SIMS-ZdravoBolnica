using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Survey : ViewModelBase
    {
        private string name;
        private List<Question> questions;
        private int id;

        public Survey(int _id,string _name, List<Question> _questions)
        { 
            name = _name;
            questions = _questions;
            Id = _id;
        }

        public Survey(int _id, List<Question> _questions)
        {
            id= _id;
            questions = _questions;

        }

        public Survey(int _id)
        {
            id = _id;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public List<Question> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

    }
}
