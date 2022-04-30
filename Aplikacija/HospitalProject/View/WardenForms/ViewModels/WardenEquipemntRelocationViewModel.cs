using System.Collections.ObjectModel;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Converter;
using HospitalProject.View.WardenForms.ViewModels;
using Model;

namespace HospitalProject.View.WardenForms;

public class WardenEquipemntRelocationViewModel : BaseViewModel
{
    private RoomControoler roomControoler;
    private EquipementController equipementController;
    private EquipmentRoomModel selectedRoom;
    private EquipmentRoomModel destinationRoom;
    private int quantity;
    private ObservableCollection<EquipmentRoomModel> _generatedRooms;
    private ObservableCollection<EquipmentRoomModel> _allRooms;
    private int roomNumber;
    private int roomId;
    private int equipmentQuantity;

    private int destinationRoomNumber;
    private int destinationRoomId;
    private int destinationEquipmentQuantity;
    
    public RelayCommand RelocateEquipmentCommand { get; set; }
    
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
    
    public EquipmentRoomModel SelectedRoom
    {
        get
        {
            return selectedRoom;
        }
        set
        {
            selectedRoom = value;
            OnPropertyChanged(nameof(SelectedRoom));
        }
    }
    
    public EquipmentRoomModel DestinationRoom
    {
        get
        {
            return destinationRoom;
        }
        set
        {
            destinationRoom = value;
            OnPropertyChanged(nameof(DestinationRoom));
        }
    }
    public ObservableCollection<EquipmentRoomModel> AllRooms
    {
        get
        {
            return _allRooms;
        }
        set
        {
            _allRooms = value;
            OnPropertyChanged(nameof(AllRooms));
        }
    }

    public WardenEquipemntRelocationViewModel(Equipement selectedEquipment)
    {
        InitializeControllers();
        int equipmentsId = selectedEquipment.Id;
        GeneratedRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateEquipementRooms(equipmentsId));
        AllRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateAllEquipementRooms(equipmentsId));
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

    private void ExecuteEquipmentRelocation(Equipement selectedEquipment)
    {
        Room selected = RoomEquipmentConverter.ConvertRoomEquipmentToRoom(SelectedRoom);
        Room destination = RoomEquipmentConverter.ConvertRoomEquipmentToRoom(DestinationRoom);
        Room oldSelected = roomControoler.Get(selected.Id);
        Room oldDestination = roomControoler.Get(destination.Id);
        foreach (Equipement eq in oldSelected.Equipment)
        {
            //if(selectedEquipment.Id == )
        }
        {
            
        }
    }
    


}