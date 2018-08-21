using System;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Activities
    {
        public int activitiesId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int usersId { get; set; }
        public Users User { get; set; }
        public List<Participants> Guests { get; set;}

        public Activities() {
            Guests = new List<Participants>();
        }
    }
}