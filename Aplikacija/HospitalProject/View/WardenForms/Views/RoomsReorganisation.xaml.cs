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
        public ObservableCollection<RoomCheckBoxModel> DestinationRooms { get; set; }
        
        public ObservableCollection<RoomCheckBoxModel> AllRooms { get; set; }

        private RoomControoler _roomControoler;
        private RoomRenovationController _roomRenovationController;

        private int destinationRoomsQuantity;
        
        public RelayCommand InsertDestinationQuantityCommand { get; set; }
        public RelayCommand CommitReorganisationCommand { get; set; }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private DateTime reorganisatoionStartDate;
        private DateTime reorganisationEndDate;
        
        
        
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
        } 
        
        private void InstantiateData(ObservableCollection<RoomViewModel> rooms)
        {
            InitializeComponent();
            DataContext = this;
            SourceRooms = new ObservableCollection<Room>();
            DestinationRooms = new ObservableCollection<RoomCheckBoxModel>();
            AllRooms = new ObservableCollection<RoomCheckBoxModel>();
            RoomItems = rooms;
            foreach (Room room in _roomControoler.GetAll())
            {
                if (room._roomType != RoomType.stockroom)
                {
                    AllRooms.Add(new RoomCheckBoxModel(room));
                }
                
            }

            InsertDestinationQuantityCommand = new RelayCommand(o => ExecuteInsertDestinationQuantityCommand(),
                o => CanExecuteInsertDestinationQuantityCommand());
            
            CommitReorganisationCommand = new RelayCommand(o => ExecuteCommitReorganisationCommand(),
                o => CanExecuteInsertDestinationQuantityCommand());

        }

        private void ExecuteCommitReorganisationCommand()
        {
            foreach (RoomCheckBoxModel room in DestinationRooms)
            {
                Room newRoom = new Room(room);
                newRoom._floor = SourceRooms[0]._floor;
                newRoom = CreateRoom(newRoom);
                UpdateDataViewAdd(newRoom);
                _roomRenovationController.Create( new RoomRenovation(
                    new DateOnly(ReorganisationStartDate.Year, ReorganisationStartDate.Month,
                        ReorganisationStartDate.Day),
                    new DateOnly(ReorganisationEndDate.Year, ReorganisationEndDate.Month, ReorganisationEndDate.Day),
                    newRoom));
            }

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
            bool valid = true;
            int floor = SourceRooms[0]._floor;
            foreach (Room room in SourceRooms)
            {
                if (floor != room._floor)
                {
                    valid = false;
                   
                }
            }

            if (valid)
            {
                DestinationRooms.Clear();
                for(int i = 0; i < DestinationRoomsQuantity; i++)
                {
                    string name = "newRoom" + i.ToString();
                    DestinationRooms.Add(new RoomCheckBoxModel(name));
                }
            }
            else
            {
                MessageBox.Show("Rooms must be on the same floor", "Warning", MessageBoxButton.OK);
            }
            
            
        }

        private bool CanExecuteInsertDestinationQuantityCommand()
        {
            return true;
        }
        

        public void destinationRoomsQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            for(int i = 0; i < DestinationRoomsQuantity; i++)
            {
                string name = "newRoom" + i.ToString();
                DestinationRooms.Add(new RoomCheckBoxModel(name));
            }
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

        private void DestinationRoomCheckbox_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void BindListBoxToDestinationRoom()
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
