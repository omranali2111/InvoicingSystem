using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class ManageShopItems
    {
        private List<ShopItem> shopInventory = new List<ShopItem>();

        public void addItems(ShopItem item)
        {
            shopInventory.Add(item);

        }
        public void SaveInventoryToFile(string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(shopInventory);
                File.WriteAllText(filePath, json);
            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public void deleteItems(ShopItem item)
        {
            shopInventory.Remove(item);

        }
        public void changeItemsPrice(string itemName, decimal newPrice)
        {
            try
            {
                ShopItem itemToChange = shopInventory.FirstOrDefault(item => item.ItemName == itemName);//LINQ extension method that searches the
                                                                                                        //shopInventory list for the first element
                                                                                                        //that satisfies a given condition.
                                                                                                        //using  lambda expression compares the ItemName property of
                                                                                                       //each ShopItem object with the provided itemName.

                if (itemToChange != null)
                {
                    itemToChange.Price = newPrice;
                    Console.WriteLine($"Price of item '{itemName}' changed to {newPrice:C}");
                }
                else
                {
                    Console.WriteLine($"Item '{itemName}' not found.");
                }
            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public List<ShopItem> reportAllItems()
        {
            return shopInventory;
        }


    }
}
