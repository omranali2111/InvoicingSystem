﻿using System;
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
        public string InvoiceHeader { get; private set; }
        public string Telephone { get; private set; }
        public string Fax { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }

        public void SetInvoiceHeader(string header)
        {
            InvoiceHeader = header;
        }

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
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("ShopSettings.json", json);
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
    }


}

