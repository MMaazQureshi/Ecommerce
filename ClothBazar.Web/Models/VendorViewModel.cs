using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.Models
{
    public class VendorViewModel
    {
        public string ID { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Town { get; set; }

    }
    public class VendorSearchViewModel
    {
        public List<ApplicationUser> Vendors { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
}