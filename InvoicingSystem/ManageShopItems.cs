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
            string json = JsonSerializer.Serialize(shopInventory);
            File.WriteAllText(filePath, json);
        }
        public void deleteItems()
        {

        }
        public void changeItemsPrice()
        {

        }
        public void reportAllItems()
        {

        }


    }
}
