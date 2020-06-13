using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WishListTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();

            WishListSearchViewModel model = new WishListSearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = WishListService.Instance.GetProductsCount(search);

            model.WishList  = WishListService.Instance.GetWishlists(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }

        

        [HttpPost]
        public ActionResult Create(int productId)
        {
            var newWishlistItem = new WishList();
            newWishlistItem.ProductID = productId;
            newWishlistItem.UserID = User.Identity.GetUserId();
            newWishlistItem.AddedOn = DateTime.Now;
            
            if (WishListService.Instance.GetWishByProductId(productId) == null)
            {
            WishListService.Instance.SaveWishlist(newWishlistItem);

            }


            return RedirectToAction("WishListTable");
           
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            WishListService.Instance.DeleteWishlist(ID);

            return RedirectToAction("WishListTable");
        }

    }
}