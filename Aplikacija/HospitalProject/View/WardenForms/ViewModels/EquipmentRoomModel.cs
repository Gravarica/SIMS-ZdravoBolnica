using HospitalProject.Core;

namespace HospitalProject.View.WardenForms.ViewModels;

public class EquipmentRoomModel : BaseViewModel
{
    private int id;
    private int number;
    private int quantity;
    
    
    public int RoomId
    {
        get { return id; }
        set
        {
            if (value != id)
            {
                id = value;
                OnPropertyChanged(nameof(RoomId));
            }
        }
    }
    
    public int RoomNumber
    {
        get { return number; }
        set
        {
            if (value != number)
            {
                number = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }
    }
    
    public int EquipmentQuantity
    {
        get { return quantity; }
        set
        {
            if (value != quantity)
            {
                quantity = value;
                OnPropertyChanged(nameof(EquipmentQuantity));
            }
        }
    }
}