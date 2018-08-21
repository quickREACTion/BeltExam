using System;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Participants
    {
        public int participantsId { get; set; }
        public int usersId { get; set; }
        public int activitiesId { get; set; }
        public Users User { get; set; }
        public Activities Activities { get; set; }

    }
}