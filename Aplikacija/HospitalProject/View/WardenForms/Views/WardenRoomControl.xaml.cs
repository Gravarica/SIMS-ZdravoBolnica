﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using HospitalProject.Controller;
using HospitalProject.Exception;
using HospitalProject.Model;
using HospitalProject.View.Converter;
using HospitalProject.View.Model;
using Model;

namespace HospitalProject.View.WardenForms
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
        public ObservableCollection<RoomViewModel> RoomItems { get; set; }
        private RoomControoler _roomControoler;

        public WardenRoomControl()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            _roomControoler = app.RoomController;
            RoomItems = new ObservableCollection<RoomViewModel>(
                RoomConverter.ConvertRoomListTORoomViewList(_roomControoler.GetAll().ToList()));
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
                default:
                    return global::Model.RoomType.examination;
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

        private void UpdateDataViewAdd(Room room)
        {
            RoomItems.Add(RoomConverter.ConvertRoomToRoomView(room));
        }

        private void EditEvent_Handler(object sender, RoutedEventArgs e)
        {
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
            RoomViewModel rvm = (RoomViewModel) Rooms.SelectedItem;
            _roomControoler.Delete(rvm.RoomId);
            RoomItems.Remove(rvm);
        }



        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (Rooms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment", "Warning", MessageBoxButton.OK);
            }
            else
            {
                DeleteRoom();
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

