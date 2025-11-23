using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp
{
    // Shared in-memory store cho toàn project
    public static class RoomStore
    {
        public static List<Room> Rooms { get; private set; } = new List<Room>();

        public static void LoadSampleRooms()
        {
            Rooms.Clear();

            Rooms.Add(new Room { Id = 1, Name = "101", Price = 150000, Description = "Phòng 101", ResourceName = "room101", ImageResourceNames = new List<string> { "room101","room101_2", "room101_3" } });
            Rooms.Add(new Room { Id = 2, Name = "102", Price = 160000, Description = "Phòng 102", ResourceName = "room102", ImageResourceNames = new List<string> { "room102", "room102_1", "room102_2" } });
            Rooms.Add(new Room { Id = 3, Name = "103", Price = 170000, Description = "Phòng 103", ResourceName = "room103" , ImageResourceNames = new List<string> { "room103", "room103_2", "room103_3","room103_4" } });
            Rooms.Add(new Room { Id = 4, Name = "104", Price = 180000, Description = "Phòng 104", ResourceName = "room104" , ImageResourceNames = new List<string> { "room104", "room104_1", "room104_2" } });
            Rooms.Add(new Room { Id = 5, Name = "105", Price = 190000, Description = "Phòng 105", ResourceName = "room105" , ImageResourceNames = new List<string> { "room105", "room105_1", "room105_2" } });
            Rooms.Add(new Room { Id = 6, Name = "106", Price = 200000, Description = "Phòng 106", ResourceName = "room106" , ImageResourceNames = new List<string> { "room106", "room106_1", "room106_2" } });
            Rooms.Add(new Room { Id = 7, Name = "107", Price = 210000, Description = "Phòng 107", ResourceName = "room107" , ImageResourceNames = new List<string> { "room107", "room107_1", "room107_2","room107_3" } });
            Rooms.Add(new Room { Id = 8, Name = "108", Price = 220000, Description = "Phòng 108", ResourceName = "room108" , ImageResourceNames = new List<string> { "room108", "room108_1", "room108_2","room108_3" } });
            Rooms.Add(new Room { Id = 9, Name = "109", Price = 230000, Description = "Phòng 109", ResourceName = "room109" , ImageResourceNames = new List<string> { "room109", "room109_1", "room109_2" } });
            Rooms.Add(new Room { Id = 10, Name = "110", Price = 240000, Description = "Phòng 110", ResourceName = "room110" , ImageResourceNames = new List<string> { "room110", "room110_1", "room110_2","room110_3" } });

            // ví dụ reservation: phòng 103 đã được đặt
            if (Rooms.Count >= 3)
            {
                if (Rooms[2].Reservations == null) Rooms[2].Reservations = new List<Reservation>();
                Rooms[2].Reservations.Add(new Reservation
                {
                    RoomId = Rooms[2].Id,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(2),
                    CustomerName = "Nguyen Van A"
                });
            }
        }
    }
}
