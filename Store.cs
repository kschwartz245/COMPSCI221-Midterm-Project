using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPSCI221_Midterm
{
    public class Store
    {
        public int DailyInteractions;
        public int MaxInteractions;
        
        private int GoldAmount;
        public int StoreInventory;
         public Store(int gold, int size, Item[] inv, int maxInteractions, int dailyRent)
        {
            GoldAmount = GOLDAMOUNT;
            StoreInventory = STOREINVENTORY;

            DailyInteractions = DAILYINTERACTIONS;    
            MaxInteractions = MAXINTERACTIONS;
            DailyRent = DAILYRENT;
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

        Public int MAXINTERACTIONS
        {
           get => MaxInteractions
            set
           {
               if (MaxInteractions < 0)
               {
                   throw new Exception("There cannot be a negative amount of max interactions")
               }
                   MaxInteractions = 7;
           }

        Public int DAILYINTERACTIONS
        {
            get => DailyInteractions
            set
            {
                if (DailyInteractions < 0)
                {
                    throw new Exception("There cannot be a negative amount of store interactions")
                }
                    DailyInteractions = 0;
            }
        }
    }
}
