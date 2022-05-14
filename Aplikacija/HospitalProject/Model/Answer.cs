using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Answer : ViewModelBase
    {
        private int rating;
        private int id;
        private int questionId;

        public Answer(int _id, int _rating, int _questionId) 
        { 
            rating = _rating;
            id = _id;
            questionId = _questionId;
        
        }

        public Answer(int _questionId, int _rating)
        {
            QuestionId = _questionId;
            Rating = _rating;
        }

        public Answer(int _id)
        {
            id = _id;
        }
        

        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                if (value >= 1 || value <= 5)
                {
                    rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
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
                
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public int QuestionId
        {
            get
            {
                return questionId;
            }
            set
            {
                
                {
                    questionId = value;
                    OnPropertyChanged(nameof(QuestionId));
                }
            }
        }
    }
}
