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
                int pointer = HeldItems.Count / 2;
                for (int i = 0; i < itemsToLoad.Count; i++) {
                    for (int t = 0; HeldItems.Count / Math.Pow(2, t) > 1; t++) {
                        if (itemsToLoad[i].ProductNumb > HeldItems[t].ProductNumb)
                        {
                            pointer += HeldItems.Count / (int)Math.Pow(2, t);
                        }
                        else if (itemsToLoad[i].ProductNumb < HeldItems[t].ProductNumb)
                        {
                            pointer -= HeldItems.Count / (int)Math.Pow(2, t);
                        }
                        else {
                            HeldItems[pointer].ProductQuantity += itemsToLoad[i].ProductQuantity;
                        }
                    }
                }
                return true;
            }
        }

        public bool Deliver(Pos destination, ref Order orderToDeliver) {

            int pointer;
            for (int i = 0; i < orderToDeliver.ItemsToDeliver.Count; i++)
            {
                pointer = HeldItems.Count / 2;
                for (int t = 0; HeldItems.Count / Math.Pow(2, t) > 1; t++)
                {
                    if (orderToDeliver.ItemsToDeliver[i].ProductNumb > HeldItems[t].ProductNumb)
                    {
                        pointer += HeldItems.Count / (int)Math.Pow(2, t);
                    }
                    else if (orderToDeliver.ItemsToDeliver[i].ProductNumb < HeldItems[t].ProductNumb)
                    {
                        pointer -= HeldItems.Count / (int)Math.Pow(2, t);
                    }
                    else if (HeldItems[i].ProductQuantity < orderToDeliver.ItemsToDeliver[i].ProductQuantity)
                    {
                        Console.WriteLine("Not enough of item '" + i + "' to deliver");
                        return false;
                    }
                }
            }

            if (destination.X == Location.X && destination.Y == Location.Y)
            {
                int itemsLeftToDeliver = 0;
                for (int i = 0; i < orderToDeliver.ItemsToDeliver.Count; i++)
                {
                    pointer = HeldItems.Count / 2;
                    for (int t = 0; HeldItems.Count / Math.Pow(2, t) > 1; t++)
                    {
                        if (orderToDeliver.ItemsToDeliver[i].ProductNumb > HeldItems[t].ProductNumb)
                        {
                            pointer += HeldItems.Count / (int)Math.Pow(2, t);
                        }
                        else if (orderToDeliver.ItemsToDeliver[i].ProductNumb < HeldItems[t].ProductNumb)
                        {
                            pointer -= HeldItems.Count / (int)Math.Pow(2, t);
                        }
                        else if (HeldItems[i].ProductQuantity < orderToDeliver.ItemsToDeliver[i].ProductQuantity)
                        {
                            orderToDeliver.ItemsToDeliver[i].ProductQuantity -= HeldItems[i].ProductQuantity;
                            itemsLeftToDeliver += orderToDeliver.ItemsToDeliver[i].ProductQuantity;
                            if (itemsLeftToDeliver == 0)
                            {
                                orderToDeliver.Completed = true;
                            }
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
