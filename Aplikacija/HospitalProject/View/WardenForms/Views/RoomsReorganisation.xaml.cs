using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Exception;
using HospitalProject.Model;
using HospitalProject.View.Converter;
using HospitalProject.View.Model;
using HospitalProject.View.WardenForms.ViewModels;
using Model;

namespace HospitalProject.View.WardenForms.Views
{
    /// <summary>
    /// Interaction logic for RoomsReorganisation.xaml
    /// </summary>
    public partial class RoomsReorganisation : UserControl
    {
        public ObservableCollection<RoomViewModel> RoomItems { get; set; }
        public ObservableCollection<Room> SourceRooms { get; set; }
        public ObservableCollection<Room> DestinationRooms { get; set; }
        
        public ObservableCollection<RoomCheckBoxModel> AllRooms { get; set; }

        private RoomControoler _roomControoler;
        private RoomRenovationController _roomRenovationController;
        private EquipmentRelocationController _equipmentRelocationController;

        private int destinationRoomsQuantity;
        private string roomName;
        
        public RelayCommand InsertDestinationQuantityCommand { get; set; }
        public RelayCommand CommitReorganisationCommand { get; set; }
        
        public RelayCommand NextRoomCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }

        private Room selectedRoom;

        private int selectedRoomNum;

        

        private int selectedRoomNumber;
        private RoomType selectedRoomType;

        public List<RoomType> RoomTypes { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private DateTime reorganisatoionStartDate;
        private DateTime reorganisationEndDate;


        public int SelectedRoomNumber
        {
            get
            {
                return selectedRoomNumber;
            }
            set
            {
                selectedRoomNumber = value;
                OnPropertyChanged(nameof(SelectedRoomNumber));
            }
        }
        
        public RoomType SelectedRoomType
        {
            get
            {
                return selectedRoomType;
            }
            set
            {
                selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
            }
        }

        public Room SelectedRoom
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
        
        
        public int SelectedRoomNum
        {
            get
            {
                return selectedRoomNum;
            }
            set
            {
                selectedRoomNum = value;
                OnPropertyChanged(nameof(SelectedRoomNum));
            }
        }
        public DateTime ReorganisationStartDate
        {
            get
            {
                return reorganisatoionStartDate;
            }
            set
            {
                reorganisatoionStartDate = value;
                OnPropertyChanged(nameof(ReorganisationStartDate));
            }
        }
        
        public string RoomName
        {
            get
            {
                return roomName;
            }
            set
            {
                roomName = value;
                OnPropertyChanged(nameof(RoomName));
            }
        }
        
        public DateTime ReorganisationEndDate
        {
            get
            {
                return reorganisationEndDate;
            }
            set
            {
                reorganisationEndDate = value;
                OnPropertyChanged(nameof(ReorganisationEndDate));
            }
        }
        
        public int DestinationRoomsQuantity
        {
            get { return destinationRoomsQuantity; }
            set
            {
                if (value != destinationRoomsQuantity)
                {
                    destinationRoomsQuantity = value;
                    OnPropertyChanged(nameof(DestinationRoomsQuantity));
                }
            }
        }

        public RoomsReorganisation()
        {
            
        }

        public RoomsReorganisation(ObservableCollection<RoomViewModel> rooms)
        {
            InstantiateControllers();
            InstantiateData(rooms);
        }
        
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _roomControoler = app.RoomController;
            _roomRenovationController = app.RenovationController;
            _equipmentRelocationController = app.EquipmentRelocationController;
        } 
        
        private void InstantiateData(ObservableCollection<RoomViewModel> rooms)
        {
            InitializeComponent();
            DataContext = this;
            InitializeCollections();
            LoadRooms(rooms);
            InstantiateCommands();
            SelectedRoomNum = 0;
            SetRoomTypes();
            

        }

        private void SetRoomTypes()
        {
            RoomTypes = new List<RoomType>();
            RoomTypes.Add(RoomType.examination);
            RoomTypes.Add(RoomType.meeting);
            RoomTypes.Add(RoomType.operation);
            RoomTypes.Add(RoomType.relaxation);
            RoomTypes.Add(RoomType.stationary);
        }

        private void InstantiateCommands()
        {
            InsertDestinationQuantityCommand = new RelayCommand(o => ExecuteInsertDestinationQuantityCommand(), o => CanExecuteInsertDestinationQuantityCommand());
            CommitReorganisationCommand = new RelayCommand(o => ExecuteCommitReorganisationCommand(), o => CanExecuteCommit());
            NextRoomCommand = new RelayCommand(o => ExecuteNextRoomCommand(),o => CanExecuteNextCommand());
            PreviousCommand = new RelayCommand(o => ExecutePreviousCommand(), o => CanExecuteReviousCommand());

        }

        private void ExecutePreviousCommand()
        {
            DestinationRooms[SelectedRoomNum].RoomType = SelectedRoomType;
            DestinationRooms[SelectedRoomNum].Number = SelectedRoomNumber;
            
            
            SelectedRoomNum -= 1;
            nmbr.Text = DestinationRooms[SelectedRoomNum].Number.ToString();
            RoomTypesBox.SelectedItem = DestinationRooms[SelectedRoomNum].RoomType;
            SelectedRoomType =  DestinationRooms[SelectedRoomNum].RoomType;
            SelectedRoomNumber = DestinationRooms[SelectedRoomNum].Number;
            int i = SelectedRoomNum + 1;
            NameLabel.Content = "New Room " + i ;
            
        }

        private void ExecuteNextRoomCommand()
        {
            DestinationRooms[SelectedRoomNum].RoomType = SelectedRoomType;
            DestinationRooms[SelectedRoomNum].Number = SelectedRoomNumber;
            
            
            
            SelectedRoomNum += 1;
            if (DestinationRooms[SelectedRoomNum].Number.ToString() != null)
            {
                nmbr.Text = DestinationRooms[SelectedRoomNum].Number.ToString();
                SelectedRoomNumber = DestinationRooms[SelectedRoomNum].Number;
            }
            else
            {
                nmbr.Text = ""; 
            }

            if (DestinationRooms[SelectedRoomNum].RoomType != null)
            {
                RoomTypesBox.SelectedItem = DestinationRooms[SelectedRoomNum].RoomType;
                SelectedRoomType =  DestinationRooms[SelectedRoomNum].RoomType;
            }
            
            int i = SelectedRoomNum + 1;
            NameLabel.Content = "New Room " + i ;
            
        }

        private bool CanExecuteNextCommand()
        {
            return SelectedRoomNum < (DestinationRooms.Count-1);

        }
        
        private bool CanExecuteReviousCommand()
        {
            return SelectedRoomNum > 0;

        }
        
        

        private void LoadRooms(ObservableCollection<RoomViewModel> rooms)
        {
            RoomItems = rooms;
            foreach (Room room in _roomControoler.GetAll())
            {
                if (room._roomType != RoomType.stockroom)
                {
                    AllRooms.Add(new RoomCheckBoxModel(room));
                }
                
            }
        }

        private void InitializeCollections()
        {
            SourceRooms = new ObservableCollection<Room>();
            DestinationRooms = new ObservableCollection<Room>();
            AllRooms = new ObservableCollection<RoomCheckBoxModel>();
        }

        private void ExecuteCommitReorganisationCommand()
        {
            DestinationRooms[SelectedRoomNum].RoomType = SelectedRoomType;
            DestinationRooms[SelectedRoomNum].Number = SelectedRoomNumber;
            foreach (Room room in DestinationRooms)
            {
                SaveRoom(room);
            }
            
            ScheduleEquipmentRelocation();
            SetViewToRoomControl();
        }

        private void ScheduleEquipmentRelocation()
        {
            foreach (Room room in SourceRooms)
            {
                foreach (Equipement equipement in room.Equipment)
                {
                    _equipmentRelocationController.Create(new EquipmentRelocation(room, new Room(1), equipement, equipement.Quantity,
                        ConvertDateTimeToDateOnly(ReorganisationEndDate)));
                }
            }
            
        }

        private DateOnly ConvertDateTimeToDateOnly(DateTime ReorganisationEndDate )
        {
            return new DateOnly(ReorganisationEndDate.Year, ReorganisationEndDate.Month,
                ReorganisationEndDate.Day);
        }

        private void SaveRoom(Room room)
        {
            Room newRoom = new Room(room);
            newRoom._floor = SourceRooms[0]._floor;
            newRoom = CreateRoom(newRoom);
            UpdateDataViewAdd(newRoom);
            _roomRenovationController.Create( new RoomRenovation(ConvertDateTimeToDateOnly(ReorganisationStartDate), 
                ConvertDateTimeToDateOnly(ReorganisationEndDate), newRoom));
        }

        private void SetViewToRoomControl()
        {
            WardenRoomControl wardenRoomControl = new WardenRoomControl(RoomItems);
            MainViewModel.Instance.MomentalView = wardenRoomControl;
        }
        private void UpdateDataViewAdd(Room room)
        {
            RoomItems.Add(RoomConverter.ConvertRoomToRoomView(room));
        }
        
        private Room CreateRoom(Room newRoom)
        {
            try
            {
                return _roomControoler.Create(newRoom);
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }
        
        private void ExecuteInsertDestinationQuantityCommand()

        {
            bool valid = CheckFloors();
            InitializeDestinationRooms(valid);
        }

        private void InitializeDestinationRooms(bool valid)
        {
            if (valid)
            {
                RefreshDestinationRooms();
            }
            else
            {
                MessageBox.Show("Rooms must be on the same floor", "Warning", MessageBoxButton.OK);
            }
        }

        private void RefreshDestinationRooms()
        {
            DestinationRooms.Clear();
            for(int i = 0; i < DestinationRoomsQuantity; i++)
            {
                
                DestinationRooms.Add(new Room());
            }
            int s = SelectedRoomNum + 1;
            NameLabel.Content = "New Room " + s ;
            SelectedRoom = DestinationRooms[0];

        }
        
        

        private bool CheckFloors()
        {
            bool valid = true;
            int floor = SourceRooms[0]._floor;
            foreach (Room room in SourceRooms)
            {
                if (floor != room._floor)
                {
                    valid = false;
                }
            }

            return valid;
        }

        private bool CanExecuteInsertDestinationQuantityCommand()
        {
           
            return DestinationRoomsQuantity>0;
        }

        private bool CanExecuteCommit()
        {
            bool valid = true;
            foreach (Room room in DestinationRooms)
            {
                valid = CheckRoomTypeValid(room);
                valid = CheckRoomNumberValid(room);
                valid = ChecDateValid(room);
            }

            return valid && CanExecuteInsertDestinationQuantityCommand();
        }

        private bool ChecDateValid(Room room)
        {
            if (reorganisationEndDate <= reorganisatoionStartDate)
            {
                return false;
            }

            return true;
        }
        
        private bool CheckRoomNumberValid(Room room)
        {
            if (room.Number <= 0)
            {
                return false;
            }

            return true;
        }
        
        private bool CheckRoomTypeValid(Room room)
        {
            if (room.RoomType == null)
            {
                return false;
            }

            return true;
        }
        
        
        private void AllSourceRoomCheckbox_CheckedAndUnchecked(object sender, RoutedEventArgs e)  
        {  
            BindListBoxToSourceRooms();  
        }  
        private void BindListBoxToSourceRooms()  
        {  
            SourceRooms.Clear();  
            foreach(RoomCheckBoxModel checkBoxRoom in AllRooms)  
            {
                if (checkBoxRoom.IsChecked)
                {
                    SourceRooms.Add(new Room(checkBoxRoom));
                }
            }  
        }

    }
}
