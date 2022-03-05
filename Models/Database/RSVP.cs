using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BELTEXAM.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPId{get;set;}
        public int UserId{get;set;}
        public User Attendee{get;set;}
        public int ActivityId{get;set;}
        public Activity ActivityAttending{get;set;}
        public DateTime CreatedAt {get;set;}        
        public DateTime UpdatedAt {get;set;}
    }
}