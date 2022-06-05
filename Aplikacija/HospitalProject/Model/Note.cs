using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Note : ViewModelBase
    {
        private int id;
        private Anamnesis anamnesis;
        private String text;



        public Note(int _id, Anamnesis _anamnesis, string _text)
        {
            Id = _id;
            Anamnesis = _anamnesis;
            Text = _text;
        }

        public Note(Anamnesis _anamnesis, string _text)
        {
            Anamnesis = _anamnesis;
            Text = _text;
        }

        public Note(int _id)
        {
            Id = _id;
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

        public Anamnesis Anamnesis
        {
            get
            {
                return anamnesis;
            }
            set
            {
                anamnesis = value;
                OnPropertyChanged(nameof(Anamnesis));
            }
        }



        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

    }
}
