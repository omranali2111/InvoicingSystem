using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    public class ShopItem
    {

        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ShopItem otherItem = (ShopItem)obj;
            return ItemId == otherItem.ItemId;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }


    }
}
