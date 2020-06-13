using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
   public class WishListService
    {
        #region Singleton
        public static WishListService Instance
        {
            get
            {
                if (instance == null) instance = new WishListService();

                return instance;
            }
        }
        private static WishListService instance { get; set; }

        

        #endregion




       

        public WishList GetWishlist(int ID)
        {
            using (var context = new CBContext())
            {
                return context.wishlist.Where(x => x.Id == ID).Include(x => x.Product).FirstOrDefault();
            }
        }
        public WishList GetWishByProductId(int ProductId)
        {
            using (var context = new CBContext())
            {
                return context.wishlist.Where(x => x.ProductID == ProductId).Include(x => x.Product).FirstOrDefault();
            }
        }
        public int GetProductsCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.wishlist.Where(item => item.Product.Name != null &&
                         item.Product.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.wishlist.Count();
                }
            }
        }

        public List<WishList> GetWishlists(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.wishlist.Where(Wishlist => IDs.Contains(Wishlist.Id)).ToList();
            }
        }

        public List<WishList> GetWishlists(int pageNo)
        {
            int pageSize = 5;// int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new CBContext())
            {
                return context.wishlist.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Product).ToList();

                //return context.Wishlists.Include(x => x.Category).ToList();
            }
        }

        public List<WishList> GetWishlists(int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.wishlist.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<WishList> GetWishlists(string search, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.wishlist.Where(Wishlist => Wishlist.Product.Name != null &&
                         Wishlist.Product.Name.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.Id)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                        .Include(x => x.Product.Category)
                         .ToList();
                }
                else
                {
                    return context.wishlist
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                         .Include(x => x.Product.Category)
                        .ToList();
                }
            }
        }

       

        public List<WishList> GetLatestWishlists(int numberOfWishlists)
        {
            using (var context = new CBContext())
            {
                return context.wishlist.OrderByDescending(x => x.Id).Take(numberOfWishlists).ToList();
            }
        }

        public void SaveWishlist(WishList Wishlist)
        {
            using (var context = new CBContext())
            {
                context.wishlist.Add(Wishlist);
                context.SaveChanges();
            }
        }

        public void UpdateWishlist(WishList Wishlist)
        {
            using (var context = new CBContext())
            {
                context.Entry(Wishlist).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteWishlist(int ID)
        {
            using (var context = new CBContext())
            {
                var Wishlist = context.wishlist.Find(ID);

                context.wishlist.Remove(Wishlist);
                context.SaveChanges();
            }
        }
    }
}
