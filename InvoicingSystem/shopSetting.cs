using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class shopSetting
    {
        public string ShopName { set; get; }
        public string InvoiceHeader { set; get; }

        public static List<Invoice> LoadAllInvoices()
        {
            List<Invoice> loadedInvoices = new List<Invoice>();
            try
            {
                string[] invoiceFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "INV*.json");
                foreach (var file in invoiceFiles)
                {
                    string json = File.ReadAllText(file);
                    Invoice invoice = JsonSerializer.Deserialize<Invoice>(json);
                    loadedInvoices.Add(invoice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading invoices: {ex.Message}");
            }
            return loadedInvoices;
        }
    }
}
