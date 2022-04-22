// File:    RoomRepository.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:57:46
// Purpose: Definition of Class RoomRepository

using System;

using HospitalProject.Exception;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repository;

namespace Repository
{
   public class RoomRepository
   {
      private const string NOT_FOUND_ERROR = "Account with {0}:{1} can not be found!";
      private string _path; // Putanja do fajla 
      private string _delimiter; // Delimiter za CSV format
      private int _roomMaxId;
      private List<Room> _rooms = new List<Room>();
   

      public RoomRepository(string path, string delimiter)
      {
         _path = path;
         _delimiter = delimiter;
         _rooms = (List<Room>) GetAll();
         _roomMaxId = GetMaxId(ReadAll());
      }
      private int GetMaxId(IEnumerable<Room> rooms) {
         return rooms.Count() == 0 ? 0 : rooms.Max(room => room._id);
      }
   
      public IEnumerable<Room> GetAll()
      {
         return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
            .Select(ConvertCSVFormatToRoom)
            .ToList();
      }
      
      public Room Insert(Room room)
      {
         room._id = ++_roomMaxId;
         RoomLineToFile(_path, ConvertRoomToCSVFormat(room));
         return room;
      }
      
      private string ConvertRoomToCSVFormat(Room room)
      {
         return string.Join(_delimiter,
            room._id.ToString(),
            room._number.ToString(),
            room._floor.ToString(),
            room._roomType.ToString());
      }
      
      private void RoomLineToFile(string path, string line)
      {
         File.AppendAllText(path, line + Environment.NewLine);
      }
      
      
      private Room ConvertCSVFormatToRoom(string roomCSVFormat)                   // Ovo prebacuje iz CSV formata i kreira objekat
      {
         var tokens = roomCSVFormat.Split(_delimiter.ToCharArray());
         return new Room(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]),(RoomType) Enum.Parse(typeof(RoomType), tokens[3], true));
      }
      
      public Room Get(int id)
      {
         try
         {
            {                                                                           // Vraca ili onaj entitet koji je jedinstven, ili vraca default vrednost
               return _rooms.FirstOrDefault(x => x._id == id);           // Daj mi onaj account za koji je account.Id == id
            }
         }
         catch (ArgumentException)
         {
            {
               throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
         }
      }
      
      public IEnumerable<Room> ReadAll()
      {
         return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
            .Select(ConvertCSVFormatToRoom)   // 1 | 20.01.2000 12:15| 20 | 2 | 3 => app1(...) 
            .ToList();
      }
      
      public void Delete(int id)
      {
         Room removeRoom = Get(id);
         _rooms.Remove(removeRoom);
         Save();
      }
      
      public void Save()
      {
         using (StreamWriter file = new StreamWriter(_path))
         {
            foreach (Room room in _rooms)
            {
               file.WriteLine(ConvertRoomToCSVFormat(room));
            }
         }
      }
      
      public void Update(Room room)
      {
         foreach (Room r in _rooms)
         {
            if (room._id == r._id)
            {
               r._floor = room._floor;
               r._number = room._number;
               r._roomType = room._roomType;
               break;
            }
         }

         Save();
      }
   }
}