using System;

namespace Notes
{
    internal class Inventory
    {
        static int GetLineCount(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            int count = 0;
            using StreamReader reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    count++;
            }

            return count;
        }

        static Items[] ReadItemsFromFile(string path)
        {

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }


            int lineCount = GetLineCount(path);
            Items[] items = new Items[lineCount - 1];

            using StreamReader reader = new StreamReader(path);
            reader.ReadLine();



            for (int i = 0; i < items.Length; i++)
            {
                string line = reader.ReadLine();
                string[] columns = line.Split(',');
                
                int Inventory = int.Parse(columns[0]);

                items[i] = new Items (Inventory);
            }

            return items;
        }

        static void Main()
        {
            string path = @"C:\Users\almig\source\repos\Notes\Items.csv";

            try
            {
                Items[] items = ReadItemsFromFile(path);


                Console.WriteLine($"{items.Length} Inventory: ");
                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine(items[i]);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
