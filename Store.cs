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
        public int StoreInventory;
        public Store(int GoldAmount, int StoreInventory)
        {
            GoldAmount = GOLDAMOUNT;
            StoreInventory = STOREINVENTORY;
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

        public int STOREINVENTORY
        {
            get => StoreInventory;
            set
            {
                if (StoreInventory < 0)
                {
                    throw new Exception("The store inventory cannot be negative");
                }
            }

        }
    }
}
