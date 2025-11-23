using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public bool IsOccupied { get; set; }
        public string Description { get; set; } = "";

        // Tên resource trong Properties.Resources (ví dụ "room101")
        public string ResourceName { get; set; } = "";


        // Danh sách tên resource chi tiết (thumbnails) — ex: "room101_jpg", "room101_2_jpg"
        public List<string> ImageResourceNames { get; set; } = new List<string>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
