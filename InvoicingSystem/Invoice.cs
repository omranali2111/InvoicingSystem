using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class Invoice
    {
        public string CustomerFullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<ShopItem> Items { get; set; } = new List<ShopItem>();
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);
        public decimal Balance => TotalAmount - PaidAmount;











    }
}
