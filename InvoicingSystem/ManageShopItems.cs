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
        List<ShopItem> loadedItems = LoadAllShopItems();
        private int nextItemId = 1; // Counter for generating unique IDs


        public void addItems(ShopItem item)
        {
            shopInventory.Add(item);
            item.ItemId = nextItemId++;

        }
        public ShopItem GetItemById(int itemId)
        {
            return shopInventory.Concat(loadedItems).FirstOrDefault(item => item.ItemId == itemId);
        }
      
        public void deleteItems(int itemId)
        {
            try
            {
                ShopItem itemToDelete = shopInventory.Find(item => item.ItemId == itemId);
                if (itemToDelete != null)
                {
                    shopInventory.Remove(itemToDelete);
                }
                else
                {
                    Console.WriteLine($"Item with ID {itemId} not found.");
                }
            }catch (Exception ex) { Console.WriteLine( ex.Message); }

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
          
            return shopInventory.Concat(loadedItems).ToList();

        }
        public static void SaveItemsToFile(List<ShopItem> items, string filePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText($"ShopItem{filePath}.Json", json);
                Console.WriteLine("Items saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving items: {ex.Message}");
            }
        }


        public static List<ShopItem> LoadAllShopItems()
        {
            List<ShopItem> loadedShopItems = new List<ShopItem>();
            try
            {
                string[] shopItemFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "ShopItem*.json");
                foreach (var file in shopItemFiles)
                {
                    string json = File.ReadAllText(file);
                    List<ShopItem> itemsInFile = JsonSerializer.Deserialize<List<ShopItem>>(json);
                    loadedShopItems.AddRange(itemsInFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading shop items: {ex.Message}");
            }
            return loadedShopItems;
        }

    }
}
