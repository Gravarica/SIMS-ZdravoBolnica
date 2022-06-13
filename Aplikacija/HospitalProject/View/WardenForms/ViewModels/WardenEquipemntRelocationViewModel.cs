using System;
using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;

namespace HospitalProject.View.WardenForms.ViewModels;

public class WardenEquipemntRelocationViewModel : BaseViewModel
{

    private EquipmentRelocationController equipmentRelocationController;
    private RoomControoler roomControoler;
    private Equipement selectedEquipement;
    private EquipmentRoomModel selectedRoom;
    private EquipmentRoomModel destinationRoom;
    private int quantity;

    private DateTime relocationDate;
    private ObservableCollection<EquipmentRoomModel> _generatedRooms;
    private ObservableCollection<EquipmentRoomModel> _allGeneratedRooms;
    private ObservableCollection<EquipmentRoomModel> _allRooms;
    private int roomNumber;
    private int roomId;
    private int equipmentQuantity;
    private int searchQuantity;

    private int destinationRoomNumber;
    private int destinationRoomId;
    private int destinationEquipmentQuantity;

    private bool wasZero;
    public RelayCommand RelocateEquipmentCommand { get; set; }
    public RelayCommand SearchQuantityCommand { get; set; }
    
    public ObservableCollection<EquipmentRoomModel> AllGeneratedRooms
    {
        get
        {
            return _allGeneratedRooms;
        }
        set
        {
            _allGeneratedRooms = value;
            OnPropertyChanged(nameof(AllGeneratedRooms));
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

    public DateTime RelocationDate
    {
        get
        {
            return relocationDate;
        }
        set
        {
            relocationDate = value;
            OnPropertyChanged(nameof(RelocationDate));
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
    
    public int SearchQuantity
    {
        get
        {
            return searchQuantity;
        }
        set
        {
            searchQuantity = value;
            OnPropertyChanged(nameof(SearchQuantity));
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
        int equipmentsId = selectedEquipment.Id;
        InstatiateData(equipmentsId);
        InitialiseCommands(selectedEquipment);
    }

    private void InstatiateData( int equipmentsId)
    {
        GeneratedRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateEquipementRooms(equipmentsId));
        AllRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateAllEquipementRooms(equipmentsId));
        AllGeneratedRooms = new ObservableCollection<EquipmentRoomModel>(roomControoler.GenerateAllEquipementRooms(equipmentsId));
        //RelocationDate = DateTime.Today;
    }

    private void InitialiseCommands(Equipement selectedEquipment)
    {
        SelectedEquipment = selectedEquipment;
        RelocateEquipmentCommand = new RelayCommand( parm=> ExecuteEquipmentRelocation(selectedEquipment), param => CanExecuteRelocation());
        SearchQuantityCommand = new RelayCommand(o => ExecuteSearchQuantityCommand(), o => true);
    }
    public WardenEquipemntRelocationViewModel() {}

    private void InitializeControllers()
    {
        var app = System.Windows.Application.Current as App;
        roomControoler = app.RoomController;
        equipmentRelocationController = app.EquipmentRelocationController;

    }

    private void ExecuteSearchQuantityCommand()
    {
        GeneratedRooms.Clear();
        foreach (EquipmentRoomModel room in AllGeneratedRooms)
        {
            if (room.EquipmentQuantity >= SearchQuantity)
            {
                GeneratedRooms.Add(room);
            }
        }
    }

    private void ScheduleEquipmentRelocation(Equipement selectedEquipment)
    {
        equipmentRelocationController.Create(new EquipmentRelocation(new Room(SelectedRoom.RoomId),new Room(DestinationRoom.RoomId),selectedEquipment,Quantity,ConvertDateTimeToDateOnly(RelocationDate)));

    }
    
    private DateOnly ConvertDateTimeToDateOnly(DateTime date )
    {
        return new DateOnly(date.Year, date.Month,
            date.Day);
    }

    private void SetRoomEmpty()
    {
        SelectedRoom.WasZero = false;
        GeneratedRooms.Remove(SelectedRoom);
    }

    private void SubstractEquipmenFromSourceRoom(int selectedQuantity)
    {
        SelectedRoom.EquipmentQuantity -= Quantity;
        selectedQuantity = SelectedRoom.EquipmentQuantity;
    }

    private void RemoveEquipmentFromSourceRoom(int selectedQuantity)
    {
        if (selectedRoom.EquipmentQuantity == Quantity)
        {
            SetRoomEmpty();
        }
        else
        {
            SubstractEquipmenFromSourceRoom(selectedQuantity);
        }
    }

    private void BringEqupmentToEmptyRoom()
    {
        DestinationRoom.WasZero = true;
        GeneratedRooms.Add(DestinationRoom);
        DestinationRoom.EquipmentQuantity += Quantity;
    }

    private void AddEquipmentToRoom(EquipmentRoomModel room)
    {
        room.EquipmentQuantity += Quantity;
    }

    private void BringEquipmentToRoomThatIsNotEmpty()
    {
        AddEquipmentToRoom(DestinationRoom);
        if (!DestinationRoom.WasZero)
        {
            EquipmentRoomModel foundDestination = GeneratedRooms.FirstOrDefault(x => x.RoomId == DestinationRoom.RoomId);
            AddEquipmentToRoom(foundDestination);
        }
       
    }

    private void AddEquipmentToDestinationRoom()
    {
        if (DestinationRoom.EquipmentQuantity == 0)
        {
            BringEqupmentToEmptyRoom();
        }
        else
        {
            BringEquipmentToRoomThatIsNotEmpty();
        }
    }

    private void ExecuteEquipmentRelocationNow(Equipement selectedEquipment)
    {
        roomControoler.UpdateRoomsEquipment(SelectedRoom.RoomId,DestinationRoom.RoomId,selectedEquipment.Id,Quantity);
        var foundSource = AllRooms.FirstOrDefault(x => x.RoomId == SelectedRoom.RoomId);
        int selectedQuantity = 0;
        
        RemoveEquipmentFromSourceRoom(selectedQuantity);
        AddEquipmentToDestinationRoom();
        
        //AllRooms[source].EquipmentQuantity = selectedQuantity;
        foundSource.EquipmentQuantity = selectedQuantity;
    }
    private void ExecuteEquipmentRelocation(Equipement selectedEquipment)
    {
        if (RelocationDate != DateTime.Today)
        {
            ScheduleEquipmentRelocation(selectedEquipment);
            ChangeMainViewToEquipmentView();
        }
        else
        {
            ExecuteEquipmentRelocationNow(selectedEquipment);
        }
        
    }

    private void ChangeMainViewToEquipmentView()
    {
        EquipementViewModel wardenEquipemntViewModel = new EquipementViewModel();
        MainViewModel.Instance.MomentalView = wardenEquipemntViewModel;
    }

    private bool CanExecuteRelocation()
    {

        return selectedRoom != null && destinationRoom != null && Quantity > 0 &&
               Quantity <= SelectedRoom.EquipmentQuantity && SelectedRoom.RoomId != DestinationRoom.RoomId;
    }
    


}