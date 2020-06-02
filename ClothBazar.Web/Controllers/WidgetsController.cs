using ClothBazar.Services;
using ClothBazar.Web.Models;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        private ApplicationDbContext context;
        public WidgetsController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryID = 0)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                List<ProductViewModel> productsList = new List<ProductViewModel>();
                var Products = ProductsService.Instance.GetLatestProducts(4);
                foreach(var item in Products)
                {
                    ProductViewModel VM = new ProductViewModel();
                    VM.Product = item;
                    VM.VendorName = item.Vendor!=null?context.Users.Where(x => x.Id == item.Vendor).FirstOrDefault().Name:"N/A";
                    productsList.Add(VM);

                }
                model.Products = productsList;
            }
            else if(CategoryID.HasValue && CategoryID.Value > 0)
            {
                List<ProductViewModel> productsList = new List<ProductViewModel>();
                var Products = ProductsService.Instance.GetLatestProducts(4);
                foreach (var item in Products)
                {
                    ProductViewModel VM = new ProductViewModel();
                    VM.Product = item;
                    VM.VendorName = context.Users.Where(x => x.Id == item.Vendor).FirstOrDefault().Name;
                    productsList.Add(VM);

                }
                model.Products = productsList;
            }
            else
            {
                List<ProductViewModel> productsList = new List<ProductViewModel>();
                var Products = ProductsService.Instance.GetLatestProducts(4);
                foreach (var item in Products)
                {
                    ProductViewModel VM = new ProductViewModel();
                    VM.Product = item;
                    VM.VendorName = item.Vendor!=null?context.Users.Where(x => x.Id == item.Vendor).FirstOrDefault().Name:"N/A";
                    productsList.Add(VM);

                }
                model.Products = productsList;
            }

            return PartialView(model);
        }
    }
}