using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace COMPSCI221_Midterm
{
    public class Shop
    {
        static void PrintMenu(Store store, string name)
        {
            Console.WriteLine($"{name}: \nCurrent Gold: {store.GOLDAMOUNT} \nCurrent Save: {store.SAVE}");
            Console.WriteLine("Select an Option:\n1. Exit Program\n2. Add Item\n3. Remove Item\n4. Print Items \n5. Save Data \n6. Load Data \n7. Expedition \n8. Settings");
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
            int save = int.Parse(col[2]);
            Store store = new Store(gold, size, inv, save);
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
            string line = $"{store.GOLDAMOUNT},{store.STORESIZE},{store.SAVE}";
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

        static void QuestingV1(Store store, Item[] roster)
        {
            string input;
            int choice;
            Console.WriteLine("\n Would you like to send an adventurer out for 200 gold?, Select 0 to Exit or 1 to send out: \n");
            input = Console.ReadLine();
            bool isValid = int.TryParse(input, out choice);
            if (isValid == true) {
                new Exception("Invalid option");
            }
            else if (choice == 0) {
                return;
            } else if (choice == 1) {
                if(store.GOLDAMOUNT > 200) {
                    new Exception("Not enough gold to send an adventurer out");
                } else {
                    int pos = RoomInStore(store.StoreInventory);
                    if (pos == -1) {
                        new Exception("No space for new items");
                    } 
                    store.GOLDAMOUNT = store.GOLDAMOUNT - 200;
                    Random rnd = new Random();
                    int rand = rnd.Next(0,6);
                    if (rand == 1) {
                        Item fill = new Item("Wooden Sword", 20);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Wooden Sword (20) gold");
                    }
                    else if (rand == 2) {
                        Item fill = new Item("Jar of dirt", 2);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Jar of dirt (2) gold");
                    }
                    else if (rand == 3) {
                        Item fill = new Item("Iron Sword", 250);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Iron Sword (250) gold");
                    }
                    else if (rand == 4) {
                        Item fill = new Item("Magic Wand", 300);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Magic Wand (300) gold");
                    }
                    else if (rand == 5) {
                        Item fill = new Item("Dragon Scale", 500);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Dragon Scale (500) gold");
                    }
                    else {
                        Item fill = new Item("Giant Spoon", 750);
                        roster[pos] = fill;
                        Console.WriteLine("Your adventurer found a Giant Spoon (750) gold");
                    }
                }
            }
        }

        static void Settings(Store store, string name) {
            Console.WriteLine("What would you like to change: \n1. Store's Name \n2. Starting Gold \n3. Current Save Slot \n0. Exit");
            string input;
            int choice;
            input = Console.ReadLine();
            bool isValid = int.TryParse(input, out choice);
            if (isValid == false) {
                new Exception("Invalid Option");
            } else {
                if (choice == 0)
                {
                    return;
                }
                else if (choice == 1)
                {
                    Console.WriteLine("What do you want the store's name to be changed to?");
                    input = Console.ReadLine();
                    name = input;
                }
                else if (choice == 2)
                {
                    Console.WriteLine("How much gold do you want to start with?");
                    input = Console.ReadLine();
                    store.GOLDAMOUNT = int.Parse(input);
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Which Save Slot to save to? 1 / 2 / 3 ?");
                    input = Console.ReadLine();
                    isValid = int.TryParse(input, out choice);
                    if (isValid == false)
                    {
                        new Exception("Invalid Option");
                    }
                    else
                    {
                        if (choice == 1 || choice == 2 || choice == 3)
                            store.SAVE = int.Parse(input);
                    }
                }
            }
        }

        static void Main()
        {
            //Starting Save Slot
            int saveSlot = 1; // !!! Can only be 1 2 or 3 !!!
            //Starting Size
            int size = 20;
            //Starting Gold
            int gold = 200;
            //string for path of items
            string rosterPath = "Roster.csv";
            //string for path of basic store savedata
            string storePath = "Store.csv" + saveSlot;
            //string for path of item savedata
            string storeItemsPath = "Inventory.csv" + saveSlot;
            //string for store name
            string name = "Average Store";

            Item[] roster = new Item[GetLineCount(rosterPath)];
            roster = ReadItemsFromFile(rosterPath);
            Item[] inv = new Item[size];// Array of Item Objects limited to store capacity
            Store store = new Store(gold, size, inv, 1);

            bool exit = false;
            int choice;
            string input;
            PrintMenu(store, name);
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
                        case (7):// Questing
                            QuestingV1(store, roster);
                            break;
                        case (8)://Settings
                            Settings(store, name);
                            storePath = "Store" + store.SAVE + ".csv";
                            storeItemsPath = "Inventory" + store.SAVE + ".csv";
                            break;
                    }
                }
                Console.Clear();
                PrintMenu(store, name);
            }
        }
    }
}
