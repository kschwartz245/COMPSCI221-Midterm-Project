using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPSCI221_Midterm
{
    public class Store
    {
        private int GoldAmount;
        private int StoreSize;
        public Item[] StoreInventory;
        public Store(int GoldAmount, int StoreSize, Item[] StoreInventory)
        {
            this.GoldAmount = GoldAmount;
            this.StoreSize = StoreSize;
            this.StoreInventory = StoreInventory;
        }

        public int GOLDAMOUNT
        {
            get => GoldAmount;
            set
            {
                if (GoldAmount < 0)
                {
                    throw new Exception("The store can't have an negative amount of gold");
                }
                GoldAmount = value;
            }
        }

        public int STORESIZE
        {
            get => StoreSize;
            set
            {
                if (StoreSize < 0)
                {
                    throw new Exception("The store can't be a negative size");
                }
                StoreSize = value;
            }
        }

        public Item[] STOREINVENTORY
        {
            get => StoreInventory;
            set
            {
                if (StoreInventory.Length < 0)
                {
                    throw new Exception("The store inventory cannot be negative");
                }
            }

        }
    }
}
