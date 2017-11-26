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
                int pointer;
                bool found;
                for (int i = 0; i < itemsToLoad.Count; i++) {
                    pointer = HeldItems.Count / 2;
                    found = false;
                    for (int t = 0; HeldItems.Count / Math.Pow(2, t) > 1; t++) {
                        if (itemsToLoad[i].ProductNumb > HeldItems[t].ProductNumb)
                        {
                            pointer += HeldItems.Count / (int) Math.Pow(2, t);
                        }
                        else if (itemsToLoad[i].ProductNumb < HeldItems[t].ProductNumb)
                        {
                            pointer -= HeldItems.Count / (int) Math.Pow(2, t);
                        }
                        else {
                            HeldItems[pointer].ProductQuantity += itemsToLoad[i].ProductQuantity;
                            found = true;
                            break;
                        }
                    }
                    if (found == false) {
                        HeldItems.Insert(pointer, itemsToLoad[i]);
                    }
                }
                return true;
            }
        }

        public bool Deliver(Pos destination, int productNumb, int productQuantity, ref Order orderToDeliver) {
            
            int pointer = HeldItems.Count / 2;
            for (int t = 0; HeldItems.Count / Math.Pow(2, t) > 1; t++)
            {
                if (productNumb > HeldItems[pointer].ProductNumb)
                {
                    pointer += HeldItems.Count / (int)Math.Pow(2, t);
                }
                else if (productNumb < HeldItems[pointer].ProductNumb)
                {
                    pointer -= HeldItems.Count / (int)Math.Pow(2, t);
                }
                else if (productQuantity < HeldItems[pointer].ProductQuantity)
                {
                    Console.WriteLine("Not enough of item '" + productNumb + "' to deliver");
                    return false;
                }
            }

            if (destination.X == Location.X && destination.Y == Location.Y)
            {
                for (int i = 0; HeldItems.Count / Math.Pow(2, i) > 1; i++)
                {
                    pointer = HeldItems.Count / 2;
                    if (productNumb > HeldItems[pointer].ProductNumb)
                    {
                        pointer += HeldItems.Count / (int)Math.Pow(2, i);
                    }
                    else if (productNumb < HeldItems[pointer].ProductNumb)
                    {
                        pointer -= HeldItems.Count / (int)Math.Pow(2, i);
                    }
                    else
                    {
                        if (HeldItems[i].ProductQuantity >= orderToDeliver.ItemsToDeliver[i].ProductQuantity)
                        {
                            orderToDeliver.ItemsToDeliver[i].ProductQuantity = 0;
                            HeldItems[i].ProductQuantity -= orderToDeliver.ItemsToDeliver[i].ProductQuantity;
                            orderToDeliver.Completed = true;
                        }
                        else {
                            orderToDeliver.ItemsToDeliver[i].ProductQuantity -= HeldItems[i].ProductQuantity;
                            HeldItems[i].ProductQuantity = 0;
                        }
                    }
                }
            }
            else {
                TurnsTillTarget = TimeToPosition(orderToDeliver.Location);
            }
            return true;
        }
        
    }
}
