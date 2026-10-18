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
            for(int i = 1; i<arr.Length+1; i++)
            {
                if (arr[i-1] == null)
                {
                    Console.WriteLine($"{i}. No Item!");
                }
                else
                {
                    Console.WriteLine($"{i}. {arr[i-1].ToString()}");
                }
            }
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

        static string ReadHeaderFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Error finding file");
            }
            using StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            return line;
        }

        static Item[] ReadItemsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Error finding file");
            }

            Item[] items = new Item[GetLineCount(path) - 1];
            using StreamReader reader = new StreamReader(path);
            reader.ReadLine();

            for (int i = 0; i < items.Length; i++)
            {
                string line = reader.ReadLine();
                string[] col = line.Split(',');

                //Parse data here for item file
                string name = col[0];
                int price = int.Parse(col[1]);

                items[i] = new Item(name, price);
            }
            return items;
        }

        //need readfile and writefile for savedata
        //Gold amount and array of shop parsed in and out of csv file

        static int ItemCount(Item[] inv) // Counts Items currently in inventory and returns an Int
        {
            int count = 0;
            for (int i = 0; i < inv.Length; i++)
            {
                if (inv[i] != null)
                {
                    count++;
                }
            }
            return count;
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

        static void AddItemV1(Item[] roster, Item[] inv) // Calls RoomInStore and places empty item in an empty slot
        {
            int pos = RoomInStore(inv);
            if (pos != -1)
            {
                Console.WriteLine("Select an Item to Add: \n");
                PrintItems(roster);
                string input = Console.ReadLine();
                int choice;
                bool isValid = int.TryParse(input, out choice);
                if (!isValid || choice > roster.Length) 
                {
                    Console.WriteLine("Input not valid! Numbers Only and Only Within the Choices Provided");
                }
                else
                {
                    inv[pos] = roster[choice]; 
                }
            }
            else
            {
                Console.WriteLine("Store is Full!\n");
            }
        }

        static void RemoveItemV1(Item[] inv)
        {
            string input;
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("\nSelect and Item to Remove, Select 0 to Exit: \n");
                PrintItems(inv);
                input = Console.ReadLine();
                bool isValid = int.TryParse(input, out choice);
                if (!isValid || choice > inv.Length || choice < 0)
                {
                    Console.WriteLine("Input not valid! Numbers from List Only");
                }
                else
                {
                    if (inv[choice-1] != null)
                    {
                        inv[choice-1] = null;
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
            int gold = 200;
            //string for path of items
            //string for path of basic store savedata
            //string for path of item savedata

            //need list of items imported to refer to i.e. ReadItemsFromFile()
            Item[] roster = new Item[0]; // fake array of items to compile code !!! Replace with Imported Item File Array !!!
            Item[] inv = new Item[20];// Array of Item Objects limited to store capacity


            bool exit = false;
            int choice;
            string input;
            PrintMenu(gold);
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
                            AddItemV1(roster, inv);
                            break;
                        case (3):// Remove Item
                            RemoveItemV1(inv);
                            break;
                        case(4):// Print Items
                            PrintItems(inv);
                            break;
                        case(5):// Save Data

                            break;
                        case (6):// Load Data

                            break;
                        case (7):
                    PrintPlayerInventory(playerInventory);
                          
                            break;
                        case (8):
                   Console.WriteLine("1. Buy from Store\n2. Sell to Store");
                   string sub = Console.ReadLine();
                  if (sub == "1") PlayerBuy(store.StoreInventory, playerInventory, store);
                  else if (sub == "2") PlayerSell(playerInventory, store);
                           break;

                    }

                    Item[] playerInventory = new Item[20];
                }
                Console.Clear();
                PrintMenu(gold);
            }
        }
    }
}
