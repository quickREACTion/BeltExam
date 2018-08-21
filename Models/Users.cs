using System;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Users
    {
        public int usersId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Participants> Guests { get; set;}
        public List<Activities> Activities { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Users() {
            Guests = new List<Participants>();
            Activities = new List<Activities>();
        }
    }
}