using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HospitalProject.Model;
using HospitalProject.View.Model;
using HospitalProject.View.WardenForms;
using HospitalProject.View.WardenForms.ViewModels;
using Model;

namespace HospitalProject.View.Converter;

public class RoomEquipmentConverter 
{
    public static EquipmentRoomModel ConvertRoomToEquipementRoom(Room room,int equipmentId)
        => new EquipmentRoomModel
        {
            RoomId = room._id,
            RoomNumber = room._number,
            EquipmentQuantity = CountEquipement(equipmentId,room),
            WasZero = false,
            RoomType = room.RoomType,
            Floor = room._floor


        };

//public static IList<RoomViewModel> ConvertRoomListTORoomViewList(IList<Room> rooms)
     //   => ConvertEntityListToViewList(rooms, ConvertRoomToRoomView);


    public static Room ConvertRoomEquipmentToRoom(EquipmentRoomModel erm)
        => new Room
        {
            _id = erm.RoomId,
            _number = erm.RoomNumber,
            _roomType = erm.RoomType,
            _floor = erm.Floor
        };

    public static int CountEquipement(int id,Room room)
    {
        foreach (Equipement eq in room.Equipment)
        {
            if (id == eq.Id)
            {
                return eq.Quantity;
            }
        }

        return 0;
    }

}