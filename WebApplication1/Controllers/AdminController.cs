using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    [Authorize(Roles ="Admin,CalisanUye")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _appDbContext;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminController(SignInManager<AppUser> signInManager = null, RoleManager<AppRole> roleManager = null, IFileProvider fileProvider = null, INotyfService notyfService = null, IConfiguration configuration = null, AppDbContext appDbContext = null, UserManager<AppUser> userManager = null)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _fileProvider = fileProvider;
            _notyfService = notyfService;
            _configuration = configuration;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult City()
        {
            return View();
        }

        public async Task<IActionResult> CityList(CityViewModel model)
        {

            var citylist = _appDbContext.Cities.Select(x => new CityViewModel()
            {
                Id = x.Id,

                SehirAdi = x.SehirAdi,
            }).ToList();
            return Json(citylist);
        }

        public async Task<IActionResult> CityUpdate(CityViewModel model)
        {
            var CityUpdate = _appDbContext.Cities.SingleOrDefault(x => x.Id == model.Id);
            CityUpdate.SehirAdi = model.SehirAdi;

            _appDbContext.Cities.Update(CityUpdate);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> CityDelete(CityViewModel model)
        {

            var citydelete = _appDbContext.Cities.FirstOrDefault(x => x.Id == model.Id);
            _appDbContext.Cities.Remove(citydelete);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });

        }
        [HttpPost]
        public async Task<IActionResult> CityInsert(CityViewModel model)
        {
            var CityInsert = new City();
            CityInsert.SehirAdi = model.SehirAdi;

            _appDbContext.Cities.Add(CityInsert);
            await _appDbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        public IActionResult Home(HouseViewModel model)
        {
            var house = _appDbContext.House.Select(x => new HouseViewModel()
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
        [HttpGet]
        public IActionResult HomeAdd()
        {
            List<SelectListItem> cityList = _appDbContext.Cities
             .Select(x => new SelectListItem
               {
                  Text = x.SehirAdi,
                  Value = x.Id.ToString()
               }).ToList();

            ViewBag.CityList = cityList;

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> HomeAdd(HouseViewModel model)
        {

            var rootfolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.Resim1.Length > 0 && model.Resim1 != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim1.FileName);
                var photoPath = Path.Combine(rootfolder.First(x => x.Name == "House").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.Resim1.CopyTo(stream);
                photoUrl = filename;
            }
            var photoUrl2 = "-";
            if (model.Resim2.Length > 0 && model.Resim2 != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim2.FileName);
                var photoPath = Path.Combine(rootfolder.First(x => x.Name == "House").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.Resim2.CopyTo(stream);
                photoUrl2 = filename;
            }
            var evekle = new House();
            evekle.Name=model.Name;
            evekle.Description=model.Description;
            evekle.Resim1 = photoUrl;
            evekle.Resim2 = photoUrl2;
            evekle.PersonPhoneNumber = model.PersonPhoneNumber;
            evekle.Adress = model.Adress;
            evekle.Status = model.Status;
            evekle.CityId = model.CityId;
            await _appDbContext.House.AddAsync(evekle);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Home", "Admin");
        }

        public IActionResult HomeDelete(int id)
        {
            var delete = _appDbContext.House.FirstOrDefault(x => x.Id == id);
            _appDbContext.House.Remove(delete);
            _appDbContext.SaveChanges();
            return RedirectToAction("Home", "Admin");
        }
        [HttpGet]
        public IActionResult HomeSearch(int id)
        {
            List<SelectListItem> cityList = _appDbContext.Cities
              .Select(x => new SelectListItem
              {
                  Text = x.SehirAdi,
                  Value = x.Id.ToString()
              }).ToList();

            ViewBag.CityList = cityList;
            var homegetir = _appDbContext.House.Where(x => x.Id == id).Select(y => new HouseViewModel()
            {
                Name = y.Name,
                Description = y.Description,
                PersonPhoneNumber = y.PersonPhoneNumber,
                Status = y.Status,
                CityId = y.CityId,
                Adress = y.Adress,
            }).FirstOrDefault();
            return View(homegetir);
        }
        [HttpPost]
        public async Task<IActionResult> HomeSearch(HouseViewModel model)
        {
            var evguncelle = _appDbContext.House.Where(x => x.Id == model.Id).FirstOrDefault();
            evguncelle.Name = model.Name;
            evguncelle.Description = model.Description;
            evguncelle.PersonPhoneNumber = model.PersonPhoneNumber;
            evguncelle.Status = model.Status;
            evguncelle.CityId = model.CityId;
            evguncelle.Adress = model.Adress;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Home", "Admin");
        }
        public async Task<IActionResult> GetUserList()
        {
            var userModels = await _userManager.Users.Select(x => new UserModelViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                UserName = x.UserName,
            }).ToListAsync();
            return View(userModels);
        }
        public async Task<IActionResult> GetRoleList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            var iletisim = _appDbContext.Contacts.Select(x => new ContactViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                Message = x.Message,

            }).ToList();
            return View(iletisim);
        }

        public IActionResult ContactsDelete(int ıd)
        {
            var contactsdelete = _appDbContext.Contacts.Where(x => x.Id == ıd).FirstOrDefault();
            _appDbContext.Contacts.Remove(contactsdelete);
            _appDbContext.SaveChanges();
            return RedirectToAction("Contacts", "Admin");
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
