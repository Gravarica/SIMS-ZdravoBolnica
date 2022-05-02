using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HospitalProject.Model;

public class Allergies  : ViewModelBase
{
    
        private int id;
        private String name;
        public System.Collections.Generic.List<int> PatientsID { get; set; }

  //  public Dictionary<Allergies,List<Patient>> AllergyMap { get; set;};
  //      private var groupbyAllergyID = AllergyMap.Keys.GroupBy(x => x.Id);
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

       
        public Allergies(int id, string name)
        {
            Name = name;
            Id = id;
        }
        public Allergies(string name)
        {
            Name = name;
        }
    
}