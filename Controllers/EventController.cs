using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class EventController : Controller
    {
        private UserContext dbContext;
        public EventController(UserContext context)
        {
            dbContext = context;
        }

        [HttpGet("home")]
        public IActionResult Index()
        {
            int UserId = LoggedIn.GetUserID(HttpContext);

            if(UserId == 0)
                return RedirectToAction("Index", "Login");

            User user = GetCurrentUser(UserId);
            
            List<Event> allevents = dbContext.Events
                .Where(a => a.Date > DateTime.Now)
                .Include(e => e.Participants)
                .ThenInclude(prt => prt.User)
                .OrderBy(evt => evt.Date)
                .ToList();

            Home Home = new Home(user, allevents);
         
            return View(Home);
        }

        [HttpGet("new")]
        public IActionResult ActivityForm()
        {
            int UserId = LoggedIn.GetUserID(HttpContext);

            if(UserId == 0)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateEvent(CreateActivity activity)
        {
            int UserId = LoggedIn.GetUserID(HttpContext);

            if(UserId == 0)
                return RedirectToAction("Index", "Login");

            if(ModelState.IsValid)
            {
                User user = GetCurrentUser(UserId);
                Event NewEvent = new Event(activity);
                NewEvent.CreatorId = UserId;
                NewEvent.CreatorFirstName = user.FirstName;
                NewEvent.CreatorLastName = user.LastName;
                dbContext.Events.Add(NewEvent);
                dbContext.SaveChanges();

                
                ViewEvent ToReturn = new ViewEvent(user, NewEvent);

                return View("ViewActivity", ToReturn);
            }
            else
            {
                return View("ActivityForm");
            }
        }

        [HttpGet("activity/{id}")]
        public IActionResult ViewActivity(int id)
        {
            int UserId = LoggedIn.GetUserID(HttpContext);

            if(UserId == 0)
                return RedirectToAction("Index", "Login");
            
            User user = GetCurrentUser(UserId);
            Event Current = dbContext.Events
                .Where(e => e.EventId == id)
                .Include(evt => evt.Participants)
                .ThenInclude(p => p.User)
                .FirstOrDefault();
            
            ViewEvent ToView = new ViewEvent(user, Current);

            return View(ToView);

        }

        [HttpGet("join/{id}")]
        public IActionResult Join(int id)
        {
            int uid = LoggedIn.GetUserID(HttpContext);

            Participant part = new Participant
            {
                UserId = uid,
                EventId = id
            };

            dbContext.Participants.Add(part);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("leave/{id}")]
        public IActionResult Leave(int id)
        {
            int uid = LoggedIn.GetUserID(HttpContext);

            Participant part = dbContext.Participants
                .FirstOrDefault(p => p.EventId == id && p.UserId == uid);
            
            dbContext.Participants.Remove(part);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int uid = LoggedIn.GetUserID(HttpContext);
            Event ToRemove = dbContext.Events
                .FirstOrDefault(e => e.EventId == id);
            
            if(ToRemove.CreatorId == uid)
            {
                dbContext.Events.Remove(ToRemove);
                dbContext.SaveChanges();
            }
        
            return RedirectToAction("Index");
        }

        [HttpGet("vleave/{id}")]
        public IActionResult VLeave(int id)
        {
            int uid = LoggedIn.GetUserID(HttpContext);

            Participant part = dbContext.Participants
                .FirstOrDefault(p => p.EventId == id && p.UserId == uid);
            
            dbContext.Participants.Remove(part);
            dbContext.SaveChanges();

            return RedirectToAction("ViewActivity");
        }

        [HttpGet("vjoin/{id}")]
        public IActionResult VJoin(int id)
        {
            int uid = LoggedIn.GetUserID(HttpContext);

            Participant part = new Participant
            {
                UserId = uid,
                EventId = id
            };

            dbContext.Participants.Add(part);
            dbContext.SaveChanges();

            return RedirectToAction("ViewActivity");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public User GetCurrentUser(int id)
        {
            User user = dbContext.Users
                .Where(u => u.UserID == id)
                .Include(usr => usr.Events)
                .ThenInclude(p => p.Event)
                .FirstOrDefault();
            return user;
        }
    }

}
