namespace HospitalProject.Model
{
    public class Alergen : ViewModelBase
    {
        private int id;
        private string name;
        
        
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

        public Alergen(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Alergen(int id)
        {
            this.id = id;
        }
    }
}

