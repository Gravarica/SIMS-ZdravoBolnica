using HospitalProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Question : ViewModelBase
    {
        private string question;
        private Category category;
        private int id;
        

        public Question(string _question,Category _category, int _id)
        {
            question = _question;
            category = _category;
            id = _id;
        }

        public Question(int _id)
        {
            id = _id;
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


        public string Text
        {
            get 
            {
                return question;
            }
            set
            {
                question = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public  Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

    }
}
