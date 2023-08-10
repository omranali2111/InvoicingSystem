using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class ShopSetting
    {
        public string ShopName { get; set; }
        public string Telephone { get;  set; }
        public string Fax { get;  set; }
        public string Email { get;  set; }
        public string Website { get; set; }

        

        public void SetContactInformation(string telephone, string fax, string email, string website)
        {
            Telephone = telephone;
            Fax = fax;
            Email = email;
            Website = website;
            Save();
        }

        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("ShopSettings.json", json);
            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public static ShopSetting Load()
        {
            try
            {
                string json = File.ReadAllText("ShopSettings.json");
                return JsonSerializer.Deserialize<ShopSetting>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading shop settings: {ex.Message}");
                return new ShopSetting();
            }
        }

        public void ReportStatistics(List<Invoice> invoices)
        {
            int totalItems = 0;
            int totalInvoices = invoices.Count;
            decimal totalSale = 0;

            foreach (var invoice in invoices)
            {
                totalItems += invoice.Items.Sum(item => item.Quantity);
                totalSale += invoice.TotalAmount;
            }

            Console.WriteLine("Shop Statistics:");
            Console.WriteLine($"Number of Items: {totalItems}");
            Console.WriteLine($"Number of Invoices: {totalInvoices}");
            Console.WriteLine($"Total Sale: {totalSale:C}");
        }
    }


}

