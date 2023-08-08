﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        private static List<Invoice> _allInvoices = new List<Invoice>();

        public Invoice(string CustomerFullName, string PhoneNumber)
        {
            this.CustomerFullName = CustomerFullName;
            this.PhoneNumber = PhoneNumber;
            InvoiceDate = DateTime.Now;
            _lastInvoiceNumber++;
            _allInvoices.Add(this);
            InvoiceNumber = $"INV{_lastInvoiceNumber:D6}"; // Format invoice number as INV000001, INV000002, etc.
        }
        public static List<Invoice> GetAllInvoices()
        {
            return _allInvoices;
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

            Save();

        }



        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText($"{InvoiceNumber}.json", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the invoice: {ex.Message}");
            }
        }
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
        public void PrintInvoice()
        {
            ShopSetting shopSetting = new ShopSetting();
            Console.WriteLine($"Shop Name: {shopSetting.ShopName}");
            Console.WriteLine($"Invoice Header: {shopSetting.InvoiceHeader}");
            Console.WriteLine($"Telephone: {shopSetting.Telephone}");
            Console.WriteLine($"Fax: {shopSetting.Fax}");
            Console.WriteLine($"Email: {shopSetting.Email}");
            Console.WriteLine($"Website: {shopSetting.Website}");
            Console.WriteLine($"Invoice Number: {InvoiceNumber}");
            Console.WriteLine($"Customer Name: {CustomerFullName}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Invoice Date: {InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss")}");
            Console.WriteLine("Items:");

            foreach (var item in Items)
            {
                Console.WriteLine($"  Item ID: {item.ItemId}");
                Console.WriteLine($"  Item Name: {item.ItemName}");
                Console.WriteLine($"  Unit Price: {item.Price:C}");
                Console.WriteLine($"  Quantity: {item.Quantity}");
                //Console.WriteLine($"  Total Price: {item.Price * item.Quantity:RO}");
                Console.WriteLine();
            }

            Console.WriteLine($"Total Amount: {TotalAmount:C}");
            Console.WriteLine($"Paid Amount: {PaidAmount:C}");
            Console.WriteLine($"Balance: {Balance:C}");
        }
    }



}

