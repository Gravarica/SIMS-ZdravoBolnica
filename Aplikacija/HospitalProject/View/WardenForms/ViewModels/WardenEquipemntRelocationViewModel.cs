using System.Collections.ObjectModel;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.ViewModels;
using Model;

namespace HospitalProject.View.WardenForms;

public class WardenEquipemntRelocationViewModel : BaseViewModel
{
    private RoomControoler roomControoler;
    private EquipementController equipementController;
    private Room selectedRoom;
    private Room destinationRoom;
    private int quantity;
    private ObservableCollection<EquipmentRoomModel> _generatedRooms;
    private int roomNumber;
    private int roomId;
    private int equipmentQuantity;
    
    
    public int RoomId
    {
        get { return roomId; }
        set
        {
            if (value != roomId)
            {
                roomId = value;
                OnPropertyChanged(nameof(RoomId));
            }
        }
    }
    
    public int RoomNumber
    {
        get { return roomNumber; }
        set
        {
            if (value != roomNumber)
            {
                roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }
    }
    
    public int EquipmentQuantity
    {
        get { return equipmentQuantity; }
        set
        {
            if (value != equipmentQuantity)
            {
                equipmentQuantity = value;
                OnPropertyChanged(nameof(EquipmentQuantity));
            }
        }
    }

    public ObservableCollection<EquipmentRoomModel> GeneratedRooms
    {
        get
        {
            return _generatedRooms;
        }
        set
        {
            _generatedRooms = value;
            OnPropertyChanged(nameof(GeneratedRooms));
        }
    }

    public WardenEquipemntRelocationViewModel(Equipement selectedEquipment)
    {
        InitializeControllers();
        int equipmentsId = selectedEquipment.Id;
        GeneratedRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateEquipementRooms(equipmentsId));
    }
    public WardenEquipemntRelocationViewModel()
    {
        
    }

    private void InitializeControllers()
    {
        var app = System.Windows.Application.Current as App;
        roomControoler = app.RoomController;
        equipementController = app.EquipementController;
    }


}