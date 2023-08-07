using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class Invoice
    {
        private static int _lastInvoiceNumber = 0;
        public string InvoiceNumber { get; }
        public string CustomerFullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<ShopItem> Items { get; set; } = new List<ShopItem>();
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);
        public decimal Balance => TotalAmount - PaidAmount;

        public Invoice(string CustomerFullName, string PhoneNumber)
        {
            this.CustomerFullName = CustomerFullName;
            this.PhoneNumber = PhoneNumber;
            InvoiceDate = DateTime.Now;
            _lastInvoiceNumber++;
            InvoiceNumber = $"INV{_lastInvoiceNumber:D6}"; // Format invoice number as INV000001, INV000002, etc.
        }


        public void AddItem(ShopItem item, int quantity = 1)
        {
            try
            {
                if (quantity <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.");
                }

                // Check if the item is already added to the invoice
                var existingItem = Items.FirstOrDefault(i => i.ItemId == item.ItemId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var newItem = new ShopItem
                    {
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        Price = item.Price,
                        Quantity = quantity
                    };
                    Items.Add(newItem);
                }
            }catch(Exception ex) {Console.WriteLine(ex.Message);}
        }











    }
}
