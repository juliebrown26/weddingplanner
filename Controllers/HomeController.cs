using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weddingplanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace weddingplanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext DbContext;

        public HomeController(WeddingContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (DbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use! Please log in or use a different email address.");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    DbContext.Add(newUser);
                    DbContext.SaveChanges();
                    HttpContext.Session.SetString("User", newUser.FirstName);
                    HttpContext.Session.SetInt32("UserId", newUser.UserId);
                    System.Console.WriteLine("************* Session User:" + newUser.FirstName + "***************");
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login userSubmission)
        {
            if (ModelState.IsValid)
            {
                User userInDb = DbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email and/or Password");
                    return View("Index");
                }
                PasswordHasher<Login> hasher = new PasswordHasher<Login>();

                PasswordVerificationResult result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);

                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email and/or Password");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetString("User", userInDb.FirstName);
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return View("login");
            }
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? currentUser = HttpContext.Session.GetInt32("UserId");

            DashboardViewModel model = new DashboardViewModel()
            {
                User = DbContext.Users.Where(u => u.UserId == (int)currentUser).FirstOrDefault(),
                Weddings = DbContext.Weddings.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Route("rsvp")]
        public IActionResult AddRSVP(DashboardViewModel model)
        {
            DbContext.Add(model.Rsvp);
            DbContext.SaveChanges();
            return Redirect($"details/{model.Rsvp.WeddingId}");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("plan")]
        public IActionResult Plan()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            if (ModelState.IsValid)
            {
                DbContext.Add(newWedding);
                DbContext.SaveChanges();
                return Redirect($"details/{newWedding.WeddingId}");
            }
            return View("Plan");
        }

        [HttpGet]
        [Route("details/{WeddingId}")]
        public IActionResult WeddingDetails(int WeddingId)
        {
            DetailsViewModel model = new DetailsViewModel()
            {
                Wedding = DbContext.Weddings
                .Include(w => w.Rsvps)
                .ThenInclude(r => r.User)
                .FirstOrDefault(w => w.WeddingId == WeddingId),

                Users = DbContext.Users.Where(u => u.Rsvps.All(a => a.WeddingId == WeddingId))
                .ToList()
            };
            return View(model);


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
