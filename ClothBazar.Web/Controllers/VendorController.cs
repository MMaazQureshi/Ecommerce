using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ClothBazar.Services;
using System.Data.Entity;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class VendorController : Controller
    {
        private ApplicationDbContext context;

        public VendorController()
        {
            context = new ApplicationDbContext();
            
        }
        // GET: User
        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]

        public ActionResult UserTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            var totalRecords = 0;
            VendorSearchViewModel model = new VendorSearchViewModel();
            model.SearchTerm = search;
           
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            if (!string.IsNullOrEmpty(search))
            {
                 totalRecords = context.Users.Where(user => user.Name != null &&
                         user.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                model.Vendors = context.Users.Where(user => user.Name != null &&
                             user.Name.ToLower().Contains(search.ToLower()))
                             .OrderBy(x => x.Id)
                             .Skip((pageNo.Value - 1) * pageSize)
                             .Take(pageSize)
                                .ToList(); 
            }
            else
            {
                 totalRecords = context.Users
                         .Count();
                model.Vendors = context.Users
                             .OrderBy(x => x.Id)
                             .Skip((pageNo.Value - 1) * pageSize)
                             .Take(pageSize)
                                .ToList(); 
            }
            model.Pager = new Pager(totalRecords, pageNo, pageSize);
            
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult GetVendorByLocation(int? pageNo)
        {
            string TextBox = Request.Form["location"].ToLower();
            var Users = context.Roles.Where(x => x.Id == "2").FirstOrDefault();
            List<VendorViewModel> UserList = new List<VendorViewModel>();
            var UserUsers = Users.Users;
            foreach(var UserUser in UserUsers)
            {
                var user = context.Users.Where(x => x.Id == UserUser.UserId&&x.Town.ToLower()==TextBox).FirstOrDefault();
                if (user != null)
                {
                    VendorViewModel User = new VendorViewModel()
                    {
                        VendorName = user.UserName,
                        Email = user.Email,
                        Address = user.Address,
                        City = user.City
                    };
                    UserList.Add(User);
                }
                
                
            }
            
            return View(UserList.ToPagedList(pageNo??1,3));
        }
        
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            VendorViewModel model = new VendorViewModel();

            var User = context.Users.Where(user=>user.Id==ID).First();
            model.Email = User.Email;
            model.VendorName = User.UserName;
            model.ID = User.Id;
            model.Address = User.Address;
            model.City = User.City;
            model.Town = User.Town;
            
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(VendorViewModel model)
        {
            var existingvendor = context.Users.Where(x=>x.Id==model.ID).FirstOrDefault();
            existingvendor.UserName = model.VendorName;
            existingvendor.Town = model.Town;
            existingvendor.City = model.City;
            existingvendor.Address = model.Address;
            existingvendor.Email = model.Email;
            
            context.SaveChanges();
           
                return RedirectToAction("UserTable");
            
                
            
        }
        public ActionResult Delete(string id)
        {
            var user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("UserTable");
        }

    }
}