using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BELTEXAM.Models;

namespace BELTEXAM.Controllers
{
    public class ActivityController : Controller
    {
        private MyContext _context {get;}
        public ActivityController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("Home")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            DashboardView ViewModel = new DashboardView
            {
                AllActivities = _context.Activities
                    .Include(a => a.Attendees)
                        .ThenInclude(a => a.Attendee)
                    .Include(u => u.Creator)
                    .OrderBy(d => d.Date)
                    .ToList(),
                LoggedUserId = (int)UserId,
                LoggedInUser = _context.Users
                    .FirstOrDefault(u => u.UserId == UserId)
            };
            return View(ViewModel);
        }
        [HttpGet("activity/{activityid}")]
        public IActionResult ActivityInfo(int activityid)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            ActivityView ViewModel = new ActivityView
            {
                Event = _context.Activities
                    .Include(a => a.Attendees)
                        .ThenInclude(rsvp => rsvp.Attendee)
                    .Include(u => u.Creator)
                    .FirstOrDefault(a => a.ActivityId == activityid),
                LoggedUserId = (int)UserId
            };
            if(ViewModel == null)
            {
                return RedirectToAction("Dashboard");
            }
            return View("ActivityInfo", ViewModel);
        }
        [HttpGet("new")]
        public IActionResult NewActivity()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            return View("NewActivity");
        }
        [HttpPost("create")]
        public IActionResult CreateActivity(Activity fromForm)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            if(ModelState.IsValid)
            {
                fromForm.CreatorId = (int)UserId;
                _context.Add(fromForm);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return NewActivity();
            }
        }
        [HttpGet("activity/{activityid}/RSVP")]
        public IActionResult RSVP(int activityid)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            RSVP attendee = _context.RSVPs
                .Where(a => a.ActivityId == activityid)
                .FirstOrDefault(u => u.UserId == UserId);
            if(attendee != null)
            {
                //Un-RSVP
                _context.RSVPs.Remove(attendee);
            }
            else
            {
                //yes RSVP
                _context.RSVPs.Add(new RSVP{ActivityId = activityid, UserId = (int)UserId});
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("activity/{activityid}/delete")]
        public IActionResult DeleteActivity(int activityid)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Dashboard");
            }
            Activity ToDelete = _context.Activities
                .FirstOrDefault(a => a.ActivityId == activityid);

            if(ToDelete.CreatorId != (int)UserId)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
            _context.Activities.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
            }
        }
    }
}