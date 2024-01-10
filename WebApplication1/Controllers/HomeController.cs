using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext = null)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var house = _appDbContext.House.Where(y=>y.Status==true).Select(x => new HouseViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Resim1imgsrc = x.Resim1,
                Resim2imgsrc = x.Resim2,
                PersonPhoneNumber = x.PersonPhoneNumber,
                Adress = x.Adress,
                Status = x.Status,
            }).ToList();

            return View(house);
           

        }

        public IActionResult HomeFind(int id)
        {
            var homegetir = _appDbContext.House.
                Include(c=>c.City)
                .Where(x => x.Id == id)
              
                .Select(y => new HouseViewModel()
            {
                Name = y.Name,
                Description = y.Description,
                PersonPhoneNumber = y.PersonPhoneNumber,
                Resim1imgsrc = y.Resim1,
                Resim2imgsrc = y.Resim2,
                Status = y.Status,
                
                Adress = y.Adress,
            }).ToList();
            ViewBag.resim1 = _appDbContext.House.Where(y => y.Id == id).Select(m => m.Resim1).FirstOrDefault();
            ViewBag.resim2 = _appDbContext.House.Where(y => y.Id == id).Select(m => m.Resim2).FirstOrDefault();
            ViewBag.sehir = _appDbContext.House.Where(y => y.Id == id).Select(m => m.City.SehirAdi).FirstOrDefault();
            return View(homegetir);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            var kaydet = new Contact();
            kaydet.Name = model.Name;
            kaydet.Email = model.Email;
            kaydet.Surname = model.Surname;
            kaydet.Message = model.Message;
            _appDbContext.Contacts.Add(kaydet);
            _appDbContext.SaveChanges();
            return Json(new { success = true });
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