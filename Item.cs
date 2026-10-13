using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPSCI221_Midterm
{
    public class Item
    {
        //Items need static name & price with proper encapsulation
        private string name;
        private int price;
        
        public Item(string name, int price) {
            if(string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentException("Item's name cannot be null or empty.");
            }
            this.name = name;

            if(price < 0) {
                throw new ArgumentException("Item's price cannot be less than zero.");
            }
            this.price = price;
        }

        public override string ToString() {
            return $"{name} is {price} gold.";
        }

        public string Name { get => name;} 
        public int Price { get => price;}

    }

    //Inherit class during stretch goal for modifiers on items
}
