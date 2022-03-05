using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BELTEXAM.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}
        [Required(ErrorMessage = "Please enter a name")]
        [MinLength(2, ErrorMessage = "You must have at least two letters in your name")]
        [Display(Name = "Name: ")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$")]
        public string Name {get;set;}
        [Required(ErrorMessage = "Please enter an email")]
        [DataType(DataType.Text)]
        [Display(Name = "Email: ")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email {get;set;}
        [Required(ErrorMessage = "You must enter a password")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password {get;set;}
        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        public string ConfirmPass {get;set;}

        //*******associations go here******
        public List<RSVP> EventsAttending{get;set;}
        public List<Activity> ActivitiesCreated{get;set;}
        //*********************************
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}