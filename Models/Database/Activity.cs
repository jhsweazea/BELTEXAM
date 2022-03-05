using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BELTEXAM.Extensions;
using System.Linq;

namespace BELTEXAM.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId{get;set;}
        [Required(ErrorMessage = "You must have a name for your activity.")]
        [Display(Name = "Activity Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$")]
        public string ActivityName{get;set;}
        [Required(ErrorMessage = "Please provide a date and time for your activity.")]
        [Display(Name = "Date and time of Activity: ")]
        [FutureDateOnly(ErrorMessage = "The activity must be scheduled in the future.")]
        public DateTime Date {get;set;}
        [Required(ErrorMessage = "Please provide a duration for your activity.")]
        [Display(Name = "Duration: ")]
        [Range(1, 100)]
        public int Duration{get;set;}
        [Display(Name = "Duration Type: ")]
        [Required(ErrorMessage = "Please provide a duration parameter.")]
        public string DurationType{get;set;}
        [Required(ErrorMessage = "Please provide a description for your activity.")]
        [Display(Name = "Description: ")]
        public string Description {get;set;}
        //*******associations go here******
        public int? CreatorId{get;set;}
        public User Creator{get;set;}
        public List<RSVP> Attendees{get;set;}
        //*********************************
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}