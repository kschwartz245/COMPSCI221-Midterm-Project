using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPSCI221_Midterm
{
    public class Shop
    {
        static void PrintMenu(int gold)
        {
            Console.WriteLine($"Adventure Shop Sim:\nCurrent Gold: {gold}\n");
            Console.WriteLine("Select an Option:\n1. Exit Program\n2. Add Item\n3. Remove Item\n4. Print Items \n5. Save Data \n6. Load Data");
        }

        static void PrintItems(Item[] arr)
        {
            for(int i = 0; i<arr.Length; i++)
            {
                if (arr[i] == null)
                {
                    Console.WriteLine($"{i}. No Item!");
                }
                else
                {
                    Console.WriteLine($"{i}. {arr[i].ToString()}");
                }
            }
            Console.WriteLine("\n");
        }

        static void ListItems(Item[] arr)
        {
                Console.WriteLine("\nPress Enter Button to Return to Menu\n");
                PrintItems(arr);
                Console.ReadLine();
                Console.WriteLine("\n");
        }

        static int GetLineCount(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            int count = 0;
            using StreamReader reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                count++;
            }

            return count;
        }

        static Store ReadStoreFromFile(string path, Item[] inv)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Error finding file");
            }
            using StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            string[] col = line.Split(',');
            int gold = int.Parse(col[0]);
            int size = int.Parse(col[1]);
            Store store = new Store(gold, size, inv);
            return store;
        }

        static Item[] ReadItemsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Error finding file");
            }

            Item[] items = new Item[GetLineCount(path)];
            using StreamReader reader = new StreamReader(path);

            for (int i = 0; i < items.Length; i++)
            {
                string line = reader.ReadLine();
                string[] col = line.Split(',');

                //Parse data here for item file
                string name = col[0];
                int price = int.Parse(col[1]);
                if (name != "null")
                    items[i] = new Item(name, price);
                else
                    items[i] = null;
            }
            return items;
        }

        static void WriteShopToFile(string path, Store store)
        {
            using StreamWriter writer = new StreamWriter(path, false);
            string line = $"{store.GOLDAMOUNT},{store.STORESIZE}";
            writer.WriteLine(line);
        }

        static void WriteItemsToFile(string path, Item[] inv)
        {
            using StreamWriter writer = new StreamWriter(path, false);
            for (int i = 0;i < inv.Length;i++)
            {
                if (inv[i] != null)
                {
                    string line = string.Join(",", inv[i].Name, inv[i].Price);
                    writer.WriteLine(line);
                }
                else
                {
                    string line = string.Join(",", "null", 0);
                    writer.WriteLine(line);
                }

            }
        }
        
        static int RoomInStore(Item[] inv) // Searches array passed in for first empty space, returning position if found or -1 if full
        {
            for (int i = 0; i< inv.Length; i++)
            {
                if (inv[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        static void AddItemV1(Item[] roster, Store store) // Calls RoomInStore and places empty item in an empty slot
        {
            int choice = 0;
            while (choice != -1)
            {
                int pos = RoomInStore(store.StoreInventory);
                if (pos != -1)
                {
                    Console.WriteLine("\nSelect an Item to Add, Select -1 to Exit: \n");
                    PrintItems(roster);
                    string input = Console.ReadLine();
                    bool isValid = int.TryParse(input, out choice);
                    if (!isValid || choice > roster.Length-1 || choice == -1)
                    {
                        Console.WriteLine("Input not valid! Numbers Only and Only Within the Choices Provided");
                    }
                    else
                    {
                        if (roster[choice].Price < store.GOLDAMOUNT)
                        {
                            store.GOLDAMOUNT -= roster[choice].Price;
                            store.StoreInventory[pos] = roster[choice];
                            Console.WriteLine("Item Bought");
                        }
                        else
                        {
                            Console.WriteLine("Not Enough Gold");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Store is Full!\n");
                    choice = -1;
                }
            }
        }

        static void RemoveItemV1(Store store)
        {
            string input;
            int choice = 0;
            while (choice != -1)
            {
                Console.WriteLine("\nSelect an Item to Remove, Select -1 to Exit: \n");
                PrintItems(store.StoreInventory);
                input = Console.ReadLine();
                bool isValid = int.TryParse(input, out choice);
                if (!isValid || choice > store.StoreInventory.Length - 1 || choice == -1)
                {
                    Console.WriteLine("Input not valid! Numbers from List Only");
                }
                else
                {
                    if (store.StoreInventory[choice] != null)
                    {
                        store.GOLDAMOUNT += store.StoreInventory[choice].Price;
                        store.StoreInventory[choice] = null;
                        Console.WriteLine("Item Sold");
                    }
                    else
                    {
                        Console.WriteLine("No Item Selected!");
                    }
                }
            }
        }

        static void Main()
        {
            //Starting Size
            int size = 20;
            //Starting Gold
            int gold = 200;
            //string for path of items
            string rosterPath = "Roster.csv";
            //string for path of basic store savedata
            string storePath = "Store.csv";
            //string for path of item savedata
            string storeItemsPath = "Inventory.csv";

            Item[] roster = new Item[GetLineCount(rosterPath)];
            roster = ReadItemsFromFile(rosterPath);
            Item[] inv = new Item[size];// Array of Item Objects limited to store capacity
            Store store = new Store(gold, size, inv);

            bool exit = false;
            int choice;
            string input;
            PrintMenu(store.GOLDAMOUNT);
            while (!exit)
            {
                input = Console.ReadLine();
                bool isValid = int.TryParse(input, out choice);
                if (!isValid)
                {
                    Console.WriteLine("Input not valid! Numbers Only");
                }
                else
                {
                    switch (choice)
                    {
                        default:
                            Console.WriteLine("\nSelect an option from the list");
                            break;
                        case (1):// Exit Program
                            exit = true;
                            break;
                        case (2):// Add Item
                            AddItemV1(roster, store);
                            break;
                        case (3):// Remove Item
                            RemoveItemV1(store);
                            break;
                        case (4)://Print Items
                            ListItems(store.StoreInventory);
                            break;
                        case(5):// Save Data
                            WriteItemsToFile(storeItemsPath, store.StoreInventory);
                            WriteShopToFile(storePath, store);
                            break;
                        case(6):// Load Data
                            inv = ReadItemsFromFile(storeItemsPath);
                            store = ReadStoreFromFile(storePath, inv);
                            break;
                    }
                }
                Console.Clear();
                PrintMenu(store.GOLDAMOUNT);
            }
        }
    }
}
