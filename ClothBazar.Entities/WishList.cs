using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class WishList
    {

        public int Id { get; set; }

        public string UserID { get; set; }

        public DateTime AddedOn { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

    }
}
