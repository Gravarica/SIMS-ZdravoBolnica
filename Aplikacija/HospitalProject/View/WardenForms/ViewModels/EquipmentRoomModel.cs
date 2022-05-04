using HospitalProject.Core;
using Model;

namespace HospitalProject.View.WardenForms.ViewModels;

public class EquipmentRoomModel : BaseViewModel
{
    private int id;
    private int number;
    private int quantity;
    private RoomType roomType;
    private int floor;
    private bool wasZero;
    
    
    public bool WasZero
    {
        get { return wasZero; }
        set
        {
            if (value != wasZero)
            {
                wasZero = value;
                OnPropertyChanged(nameof(WasZero));
            }
        }
    }
    
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
    
    public int Floor
    {
        get { return floor; }
        set
        {
            if (value != floor)
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }
    }
    
    public RoomType RoomType
    {
        get { return roomType; }
        set
        {
            if (value != roomType)
            {
                roomType = value;
                OnPropertyChanged(nameof(RoomType));
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