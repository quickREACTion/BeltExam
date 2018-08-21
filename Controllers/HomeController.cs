using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;

        public HomeController(UserContext context) {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        //Registration Post Route
        [HttpPost("Register")]
        public IActionResult Register(UserValidator userV) {
            Users newUser = new Users();
            if (userV.First_Name.All(Char.IsLetter)) {
                newUser.First_Name = userV.First_Name;
            } else {
                @ViewBag.notLetter = "The first name must contain only letters!";
                return View("Index");
            }
            if (userV.Last_Name.All(Char.IsLetter)) {
                newUser.Last_Name = userV.Last_Name;
            } else {
                @ViewBag.notLetterLast = "The last name must contain only letters!";
                return View("Index");
            }
            newUser.Email = userV.Email;
            newUser.Password = userV.Password;

            List<Users> existingEmail = _context.users.Where(p => p.Email == newUser.Email).ToList();
            if (existingEmail.Count > 0) {
                @ViewBag.exist = "That email already exists!";
            }

            if(ModelState.IsValid)
                {
                    newUser.created_at = DateTime.Now;
                    newUser.updated_at = DateTime.Now;
                    PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    if (existingEmail.Count == 0) {
                        _context.users.Add(newUser);
                        _context.SaveChanges();
                        HttpContext.Session.SetInt32("user_id", newUser.usersId);
                        return RedirectToAction("Home");
                    } 
                    else 
                    {
                        @ViewBag.exist = "That email already exists!";
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
        }

        //Login Validation / Post Route
        [HttpPost("Login")]
        public IActionResult Login(string Email, string Password)
        {
            Users currentUser = _context.users.Where(cu => cu.Email == Email).SingleOrDefault();
            // Attempt to retrieve a user from your database based on the Email submitted
            if(currentUser != null && Password != null)
            {
                var Hasher = new PasswordHasher<Users>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(currentUser, currentUser.Password, Password))
                {
                    HttpContext.Session.SetInt32("user_id", currentUser.usersId);
                    return RedirectToAction("Home");
                } else {
                    ViewBag.error = "The Email/Password doesn't match!";
                    return View("Index");
                }
            }
            ViewBag.Email = "Email is a required field!";
            ViewBag.Pass = "Password is a required field!";
            return View("Index");
        }

        [Route("Home")]
        public IActionResult Home() {
            var curUser = _context.users.SingleOrDefault(name => name.usersId == HttpContext.Session.GetInt32("user_id"));
            if(curUser == null) {
                return View("Index");
            } else {
                ViewBag.user = curUser;
                List<Activities> allActivities = _context.activities.Include(ag => ag.Guests).ThenInclude(u => u.User).Include(cu => cu.User).OrderBy(p => p.Date).Where(e => e.created_at < DateTime.Now).ToList();
                ViewBag.activity = allActivities;
                return View("Home");
            }
            
        }

        [HttpPost("Join")]
        public IActionResult Join(int JoinHidden) {
            Activities curActivity = _context.activities.SingleOrDefault(cw => cw.activitiesId == JoinHidden);
            Users curUser = _context.users.SingleOrDefault(p => p.usersId == HttpContext.Session.GetInt32("user_id"));

            bool isGuest = false;
            for(int i=0; i < curActivity.Guests.Count; i++) {
                if(curActivity.Guests[i].usersId == curUser.usersId) {
                    isGuest = true;
                }
            }

            if(!isGuest) {
                Participants curPart = new Participants();
                curPart.usersId = curUser.usersId;
                curPart.activitiesId = JoinHidden;
                _context.participants.Add(curPart);
                _context.SaveChanges();
            }

            return RedirectToAction("Home");                
        }

        [HttpPost("ActJoin")]
        public IActionResult ActJoin(int JoinHidden) {

            return RedirectToAction("Home");
        }

        [HttpPost("Leave")]
        public IActionResult Leave(int LeaveHidden) {
            Activities curWed = _context.activities.SingleOrDefault(cw => cw.activitiesId == LeaveHidden);
            Users curUser = _context.users.SingleOrDefault(p => p.usersId == HttpContext.Session.GetInt32("user_id"));

            Participants nonParticipant = _context.participants.Where(rvid => rvid.activitiesId == LeaveHidden).SingleOrDefault(id => id.usersId == curUser.usersId);
            _context.participants.Remove(nonParticipant);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [Route("New")]
        public IActionResult New() {
            var curUser = _context.users.SingleOrDefault(name => name.usersId == HttpContext.Session.GetInt32("user_id"));
            if(curUser == null) {
            return View("Index");
            } else {
                return View("New");
            }
        }

        [HttpPost("Create")]
        public IActionResult Create(AcivitiesValidator actV) {
            Activities newActivitiy = new Activities();
            Users curUser = _context.users.SingleOrDefault(p => p.usersId == HttpContext.Session.GetInt32("user_id"));

            newActivitiy.Title = actV.Title;
            newActivitiy.Description = actV.Description;

            if(ModelState.IsValid)
                {
                    newActivitiy.Duration = (TimeSpan)actV.Duration;
                    newActivitiy.Date = (DateTime)actV.Date;
                    newActivitiy.Time = (TimeSpan)actV.Time;
                    newActivitiy.usersId = curUser.usersId;
                    newActivitiy.created_at = DateTime.Now;
                    newActivitiy.updated_at = DateTime.Now;
                    _context.activities.Add(newActivitiy);
                    _context.SaveChanges();

                    int actId = newActivitiy.activitiesId;

                    return RedirectToAction("Activities", new {actId = actId});
                }
                else
                {
                    return View("New");
                }

        }

        [HttpPost("Activities")]
        public IActionResult ActivitiesDisplay(int activityId) {

            return RedirectToAction("Activities", new {actId = activityId});
        }

        [Route("Activities/{actId}")]
        public IActionResult Activities(int actId) {
            var curUser = _context.users.SingleOrDefault(name => name.usersId == HttpContext.Session.GetInt32("user_id"));
            if(curUser == null) {
                return RedirectToAction("Index");
            } else {
                ViewBag.user = curUser;

                Activities specificActivity = _context.activities.Where(cw => cw.activitiesId == actId).SingleOrDefault(sw => sw.activitiesId == actId);
                ViewBag.specAct = specificActivity;

                List<Activities> allActivities = _context.activities.Include(ag => ag.Guests).ThenInclude(u => u.User).Include(cu => cu.User).OrderBy(p => p.Date).ToList();
                ViewBag.activity = allActivities;

                Activities allParticipants = _context.activities.Include(ag => ag.Guests).ThenInclude(u => u.User).SingleOrDefault(cc => cc.activitiesId == actId);
                ViewBag.actPart = allParticipants;

                return View("Activities");
            }

        }
        


        [HttpPost("Delete")]
        public IActionResult Delete(int deleteAct) {
            Activities removeActivity = _context.activities.SingleOrDefault(ca => ca.activitiesId == deleteAct);
            _context.Remove(removeActivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
