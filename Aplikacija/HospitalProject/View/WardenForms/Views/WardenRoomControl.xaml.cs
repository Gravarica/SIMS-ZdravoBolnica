using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Controller;
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
    public partial class WardenRoomControl : UserControl
    {

        private IList<Room> _rooms;
        private int _floor;
        private int _number;
        private int _id;
        private string _roomType;
        private List<String> roomTypes;
        private List<Equipement> roomsEquipement;
        private RoomViewModel selectedRoom1;
        public RelayCommand RenovationCommand { get; set; }

        public RelayCommand RoomRelocationCommand { get; set; }

        public ObservableCollection<RoomViewModel> RoomItems { get; set; }
        private RoomControoler _roomControoler;
        private RoomRenovationController _roomRenovationController;
        private AppointmentController _appointmentController;

        public RoomViewModel SelectedRoom1
        {
            get
            {
                return selectedRoom1;
            }
            set
            {
                selectedRoom1 = value;
                OnPropertyChanged(nameof(SelectedRoom1));
            }
        }

        public WardenRoomControl()
        {
            InitializeComponent();
            DataContext = this;
            InitialiseControllers();
            RoomItems = new ObservableCollection<RoomViewModel>(
                RoomConverter.ConvertRoomListTORoomViewList(_roomControoler.GetAll().ToList()));
            InitialiseCommands();

        }

        public WardenRoomControl(ObservableCollection<RoomViewModel> rooms)
        {
            InitializeComponent();
            DataContext = this;
            InitialiseControllers();
            RoomItems = rooms;
            InitialiseCommands();

        }

        private void InitialiseCommands()
        {
            RenovationCommand = new RelayCommand(param => ExecuteRenovationComand(), param => CanExecuteRenovation());
            RoomRelocationCommand = new RelayCommand(param => ExecuteRoomRelocationCommand(), param => true);

        }

        private void InitialiseControllers()
        {
            var app = Application.Current as App;
            _roomControoler = app.RoomController;
            _appointmentController = app.AppointmentController;
            _roomRenovationController = app.RenovationController;
        }

        private void ExecuteRoomRelocationCommand()
        {
            RoomsReorganisation roomsReorganisation = new RoomsReorganisation(RoomItems);
            MainViewModel.Instance.MomentalView = roomsReorganisation;
        }
        private void ExecuteRenovationComand()
        {
            
            RoomRenovationViewModel roomRenovationViewModel = new RoomRenovationViewModel(RoomConverter.ConvertRoomViewtoRoom((RoomViewModel)Rooms.SelectedItem));
            MainViewModel.Instance.MomentalView = roomRenovationViewModel;
        }
        
        private bool CanExecuteRenovation()
        {
            return Rooms.SelectedItem != null;
        }
        
        public RoomType StringToRoomType(string str)
        {
            switch (str)
            {
                case "operation":
                    return global::Model.RoomType.operation;
                case "stationary":
                    return global::Model.RoomType.stationary;
                case "relaxation":
                    return global::Model.RoomType.relaxation;
                case "meeting":
                    return global::Model.RoomType.meeting;
                case "examination":
                    return global::Model.RoomType.examination;
                default:
                    return global::Model.RoomType.stockroom;
            }
        }

        private IList<String> FindAllRoomTypes()
        {
            IList<String> allRoomTypes = new List<string>();
            allRoomTypes.Append("operation");
            allRoomTypes.Append("stationary");
            allRoomTypes.Append("relaxation");
            allRoomTypes.Append("meeting");
            allRoomTypes.Append("examination");
            return allRoomTypes;
        }

        private IList<int> FindRoomIdFromRooms()
        {
            return _rooms
                .Select(room => room._id)
                .ToList();
        }

        public int RoomFloor
        {
            get { return _floor; }
            set
            {
                if (value != _floor)
                {
                    _floor = value;
                    OnPropertyChanged(nameof(RoomFloor));
                }
            }
        }

        public int RoomNumber
        {
            get { return _number; }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    OnPropertyChanged(nameof(RoomNumber));
                }
            }
        }

        public string TypeRoom
        {
            get { return _roomType; }
            set
            {
                if (value != _roomType)
                {
                    _roomType = value;
                    OnPropertyChanged(nameof(TypeRoom));
                }
            }
        }

        private Room FindRoomFromRoomId(int id)
        {
            return _roomControoler.Get(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddEvent_Handler(object sender, RoutedEventArgs e)
        {

            UpdateDataViewAdd(CreateRoom());
        }
        
        private void DeleteEvent_Handler(object sender, RoutedEventArgs e)
        {

            DeleteRoom();
        }
        
        

        private void UpdateDataViewAdd(Room room)
        {
            RoomItems.Add(RoomConverter.ConvertRoomToRoomView(room));
        }

        private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {
            RoomViewModel selectedRoom = (RoomViewModel) Rooms.SelectedItem;
            
            if (selectedRoom.TypeRoom.ToString() == "stockroom")
            {
                MessageBox.Show("Stockroom cant be edited", "Warning", MessageBoxButton.OK);
            }
            else
            if (Rooms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an room", "Warning", MessageBoxButton.OK);
            }
            else
            {
                EditRoom();
            }
        }
        
        private void EditRoom()
        {
            try
            {
                RoomViewModel updateRVM = (RoomViewModel) Rooms.SelectedItem;
                _roomControoler.Update(RoomConverter.ConvertRoomViewtoRoom(updateRVM));
            }

            catch (InvalidDateException)
            {
                throw;
            }
        }

        private void DeleteRoom()
        {
            RoomViewModel selectedRoom = (RoomViewModel) Rooms.SelectedItem;
            
            if (selectedRoom.TypeRoom.ToString() == "stockroom")
            {
                MessageBox.Show("Stockroom cant be deleted", "Warning", MessageBoxButton.OK);
            }
            else if (!_appointmentController.DeleteApointmentsByRoomId(selectedRoom.RoomId))
            {
                MessageBox.Show("This Room has scheduled appointments", "Warning", MessageBoxButton.OK);
            }
            else
            {
                
                _roomControoler.Delete(selectedRoom.RoomId);
                RoomItems.Remove(selectedRoom);
            }
            
        }
        
        private Room CreateRoom()
        {
            try
            {
                return _roomControoler.Create(new Room(roomsEquipement,_id, _number, _floor, StringToRoomType(_roomType)));
            }
            catch (InvalidDateException)
            {
                throw;
            }
        }
    }


}

