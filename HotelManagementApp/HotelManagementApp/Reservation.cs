using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp
{
    public class Reservation
    {
        public int UserId { get; set; }       // ⭐ THÊM DÒNG NÀY
        public int Id { get; set; }           // optional
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; } // inclusive
        public DateTime EndDate { get; set; }   // exclusive (quy ước)
        public string CustomerName { get; set; }
    }
}
