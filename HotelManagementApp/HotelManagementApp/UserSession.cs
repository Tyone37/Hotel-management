using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp
{
    public static class UserSession
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUsername { get; set; }
    }

}
