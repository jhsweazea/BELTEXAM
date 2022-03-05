using System.Collections.Generic;

namespace BELTEXAM.Models
{
    public class DashboardView
    {
        public List<Activity> AllActivities {get;set;}
        public int LoggedUserId {get;set;}
        public User LoggedInUser{get;set;}
        public int CreatorId{get;set;}
        public User Creator{get;set;}
    }
}