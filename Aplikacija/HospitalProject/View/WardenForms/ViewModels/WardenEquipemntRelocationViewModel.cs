using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    private Equipement selectedEquipement;
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

    private bool wasZero;
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
    
    public bool WasZero
    {
        get
        {
            return wasZero;
        }
        set
        {
            wasZero = value;
            OnPropertyChanged(nameof(WasZero));
        }
    }
    
    public Equipement SelectedEquipment
    {
        get
        {
            return selectedEquipement;
        }
        set
        {
            selectedEquipement = value;
            OnPropertyChanged(nameof(SelectedEquipment));
        }
    }
    
    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
            OnPropertyChanged(nameof(Quantity));
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
        WasZero = false;
        SelectedEquipment = selectedEquipment;
        int equipmentsId = selectedEquipment.Id;
        GeneratedRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateEquipementRooms(equipmentsId));
        AllRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateAllEquipementRooms(equipmentsId));
        RelocateEquipmentCommand = new RelayCommand( parm=> ExecuteEquipmentRelocation(selectedEquipment), param => CanExecuteRelocation());

    }
    public WardenEquipemntRelocationViewModel()
    {
        
    }

    private void InitializeControllers()
    {
        var app = System.Windows.Application.Current as App;
        roomControoler = app.RoomController;
        
    }

    private void ExecuteEquipmentRelocation(Equipement selectedEquipment)
    {
        roomControoler.UpdateRoomsEquipment(SelectedRoom,DestinationRoom,selectedEquipment.Id,Quantity);


        var foundSource = AllRooms.FirstOrDefault(x => x.RoomId == SelectedRoom.RoomId);

        int source = AllRooms.IndexOf(foundSource);
        int selectedQ = 0;


        if (selectedRoom.EquipmentQuantity == Quantity)
        {
            GeneratedRooms.Remove(SelectedRoom);
        }
        else
        {
            SelectedRoom.EquipmentQuantity -= Quantity;
            selectedQ = SelectedRoom.EquipmentQuantity;
        }

        if (DestinationRoom.EquipmentQuantity == 0)
        {
            WasZero = true;
            GeneratedRooms.Add(DestinationRoom);
            DestinationRoom.EquipmentQuantity += Quantity;
        }
        else
        {
            if (WasZero)
            {
                DestinationRoom.EquipmentQuantity += Quantity;
            }
            else
            {
            DestinationRoom.EquipmentQuantity += Quantity;
            var foundDestination = GeneratedRooms.FirstOrDefault(x => x.RoomId == DestinationRoom.RoomId);
            int destination = GeneratedRooms.IndexOf(foundDestination);

            GeneratedRooms[destination].EquipmentQuantity += Quantity;
        }

        //
        // var foundDestination1 = AllRooms.FirstOrDefault(x => x.RoomId == DestinationRoom.RoomId);
        // int destination1 = AllRooms.IndexOf(foundDestination1);
        //
        // AllRooms[destination1].EquipmentQuantity += Quantity;
        }
        
        
        AllRooms[source].EquipmentQuantity = selectedQ;
        
        





    }

    private bool CanExecuteRelocation()
    {

        return selectedRoom != null && destinationRoom != null && Quantity > 0 &&
               Quantity <= SelectedRoom.EquipmentQuantity && SelectedRoom.RoomId != DestinationRoom.RoomId;
    }
    


}