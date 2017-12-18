using System;
using hashCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    struct Drone
    {
        public static int[] ItemWeights { get; set; }
        Pos Location { get; set; }
        int TurnsTillTarget { get; set; }
        int MaxWeight { get; }
        List<Product> HeldItems { get; set; }
        int CurrentWeight { get; set; }

        public Drone(int maxWeight){
            Location = Warehouses[0].Location;
            TurnsTillTarget = -1;
            MaxWeight = maxWeight;
            HeldItems = new List<Product>();
            CurrentWeight = 0;
        }

        public int TimeToPosition(Pos target)
        {
            return (int) Math.Ceiling(Math.Sqrt(Math.Pow(Location.X - target.X, 2) + Math.Pow(Location.Y - target.Y, 2)));
        }

        public bool Load(Pos warehouseLocation, List<Product> itemsToLoad) {
            int newWeight = CurrentWeight;
            for (int i = 0; i < itemsToLoad.Count; i++) {
                newWeight += ItemWeights[i] * itemsToLoad[i].ProductQuantity;
            }
            if (newWeight > MaxWeight) {
                return false;
            }
            else {
                bool found;
                for (int i = 0; i < itemsToLoad.Count; i++) {
                    found = false;
                    int left = 0,
                        right = HeldItems.Count - 1,
                        mid = 0;
                    while (left <= right)
                    {
                        mid = left + ((right - left) / 2);
                        if (HeldItems[mid].ProductNumb > itemsToLoad[i].ProductNumb)
                        {
                            right = mid - 1;
                        }
                        else if (HeldItems[mid].ProductNumb < itemsToLoad[i].ProductNumb)
                        {
                            left = mid + 1;
                        }
                        else
                        {
                            HeldItems[mid].ProductQuantity += itemsToLoad[i].ProductQuantity;
                            found = true;
                            break;
                        }
                    }
                    //-------------------------------------------------------------------
                    if (found == false)
                    {
                        HeldItems.Insert(mid, itemsToLoad[i]);
                    }
                }
                return true;
            }
        }

        public bool Deliver(Pos destination, int productNumb, int productQuantity, ref Order orderToDeliver) {

            //----------------------------finding index on drone----------------------------
            int left = 0,
                right = HeldItems.Count - 1,
                mid = 0;
            while (left <= right)
            {
                mid = left + ((right - left) / 2);
                if (HeldItems[mid].ProductNumb > productNumb)
                {
                    right = mid - 1;
                }
                else if (HeldItems[mid].ProductNumb < productNumb)
                {
                    left = mid + 1;
                }
                else
                {
                    if (HeldItems[mid].ProductQuantity < productQuantity)
                    {
                        Console.WriteLine("Not enough of item '" + productNumb + "' to deliver");
                        return false;
                    }
                }
                
            }
            //----------------------------finding index in order----------------------------
            left = 0;
            right = orderToDeliver.ItemsToDeliver.Count - 1;
            int midSecond = 0;
            while (left <= right)
            {
                midSecond = left + ((right - left) / 2);
                if (orderToDeliver.ItemsToDeliver[midSecond].ProductNumb > productNumb)
                {
                    right = midSecond - 1;
                }
                else if (orderToDeliver.ItemsToDeliver[mid].ProductNumb < productNumb)
                {
                    left = midSecond + 1;
                }
                else
                {
                    orderToDeliver.ItemsToDeliver[midSecond].ProductQuantity = 0;
                    HeldItems[mid].ProductQuantity -= orderToDeliver.ItemsToDeliver[midSecond].ProductQuantity;
                    orderToDeliver.Completed = true;
                    return true;
                }
            }
            TurnsTillTarget = TimeToPosition(orderToDeliver.Location);
            return true;
        }
        
    }
}
