using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystem
{
    internal class Menu
    {
        private static ShopSetting settings = new ShopSetting();
        private static ManageShopItems shopItemManager = new ManageShopItems();
        private static List<Invoice> invoices = new List<Invoice>();
        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Shop Settings");
                Console.WriteLine("2. Manage Shop Items");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShowShopSettingsMenu();
                            break;
                        case 2:
                            ShowManageShopItemsMenu();
                            break;
                        case 3:
                            return; // Exit the program
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void ShowShopSettingsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Shop Settings");
                Console.WriteLine("1. Load Data");
                Console.WriteLine("2. Set Shop Name");
                Console.WriteLine("3. Set Invoice Header");
                Console.WriteLine("4. Go Back");
                Console.Write("Select an option: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            LoadData();
                            break;
                        case 2:
                            SetShopName();
                            break;
                        case 3:
                            SetInvoiceHeader();
                            break;
                        case 4:
                            return; // Go back to main menu
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        private static void LoadData()
        {
            ManageShopItems.LoadAllShopItems(); // Load items
            Invoice.LoadAllInvoices();   // Load invoices
            Console.WriteLine("Data loaded.");
        }
        private static void SetShopName()
        {
            Console.WriteLine("Enter the new shop name: ");
            string newShopName = Console.ReadLine();
            settings.ShopName = newShopName;
            Console.WriteLine("Shop name updated.");
        }

        private static void SetInvoiceHeader()
        {
            Console.WriteLine("Enter the new invoice header: ");
            string newInvoiceHeader = Console.ReadLine();

            Console.WriteLine("Enter the new telephone: ");
            string telephone = Console.ReadLine();

            Console.WriteLine("Enter the new fax: ");
            string fax = Console.ReadLine();

            Console.WriteLine("Enter the new email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter the new website: ");
            string website = Console.ReadLine();

            settings.SetInvoiceHeader(newInvoiceHeader);
            settings.SetContactInformation(telephone, fax, email, website);

            Console.WriteLine("Invoice header and contact information updated.");
        }

        private static void ShowManageShopItemsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manage Shop Items");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Delete Item");
                Console.WriteLine("3. Change Item Price");
                Console.WriteLine("4. Report All Items");
                Console.WriteLine("5. Go Back");
                Console.Write("Select an option: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ShopItem newItem = new ShopItem();

                            Console.Write("Enter Item Name: ");
                            newItem.ItemName = Console.ReadLine();

                            Console.Write("Enter Price: ");
                            decimal price;
                            if (decimal.TryParse(Console.ReadLine(), out price))
                            {
                                newItem.Price = price;
                            }
                            else
                            {
                                Console.WriteLine("Invalid price format. Item not added.");
                                break;
                            }

                            Console.Write("Enter Quantity: ");
                            int quantity;
                            if (int.TryParse(Console.ReadLine(), out quantity))
                            {
                                newItem.Quantity = quantity;
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity format. Item not added.");
                                break;
                            }

                            // Add the newly created item to the list
                            shopItemManager.addItems(newItem);

                            Console.WriteLine("Item added.");
                            break;

                        case 2:
                            Console.Write("Enter Item ID to delete: ");
                            int itemIdToDelete;
                            if (int.TryParse(Console.ReadLine(), out itemIdToDelete))
                            {
                                shopItemManager.deleteItems(itemIdToDelete); // Pass the item ID to the DeleteItem method
                                Console.WriteLine($"Item with ID {itemIdToDelete} deleted.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid item ID.");
                            }
                            break; ;
                        case 3:
                            Console.Write("Enter Item Name: ");
                            string itemName = Console.ReadLine();

                            Console.Write("Enter New Price: ");
                            decimal newPrice;
                            if (decimal.TryParse(Console.ReadLine(), out newPrice))
                            {
                                shopItemManager.changeItemsPrice(itemName, newPrice); // Pass the item name and new price to the ChangeItemPrice method
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid price.");
                            }
                            break;
                        case 4:
                            shopItemManager.reportAllItems();
                            break;
                        case 5:
                            return; // Go back to main menu
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
