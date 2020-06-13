using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entities;

namespace ClothBazar.Web.ViewModels
{
    public class WishListSearchViewModel
    {
        public List<WishList> WishList { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class NewWishViewModel
    {
        public int ProductId { get; set; }
      
    }
}