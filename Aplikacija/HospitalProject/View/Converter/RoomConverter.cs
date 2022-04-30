using System;
using System.Collections.Generic;
using HospitalProject.View.Model;
using Model;

namespace HospitalProject.View.Converter
{
    public class RoomConverter : AbstractConverter
    {
        public static RoomViewModel ConvertRoomToRoomView(Room room)
            => new RoomViewModel
            {
                RoomId = room._id,
                RoomFloor = room._floor,
                RoomNumber = room._number,
                TypeRoom = room._roomType.ToString(),
                Equipment = room.Equipment
            };

        public static IList<RoomViewModel> ConvertRoomListTORoomViewList(IList<Room> rooms)
            => ConvertEntityListToViewList(rooms, ConvertRoomToRoomView);


        public static Room ConvertRoomViewtoRoom(RoomViewModel rvm)
            => new Room
            {
                _id = rvm.RoomId,
                _floor = rvm.RoomFloor,
                _number = rvm.RoomNumber,
                _roomType = (RoomType) Enum.Parse(typeof(RoomType), rvm.TypeRoom, true),
                Equipment = rvm.Equipment
            };
    }
}

