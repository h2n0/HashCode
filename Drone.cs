using System;
using hashCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    class Drone
    {
        Pos Location { get; set; }
        int MaxWeight { get; }
        int HeldItems { get; set; }
        int CurrentWeight { get; set; }

        Drone(int maxWeight){
            Location = new Pos(0, 0);
            MaxWeight = maxWeight;
        }
        public int TimeToPosition(Pos target)
        {
            return (int) Math.Ceiling(Math.Sqrt(Math.Pow(Location.X - target.X, 2) + Math.Pow(Location.Y - target.Y, 2)));
        }
        public bool Load(Pos warehouseLocation, int[] itemsToLoad) {
            int newWeight = CurrentWeight;
            for (int i = 0; i < itemsToLoad.Length; i++) {
                newWeight += ItemWeights[i] * itemsToLoad[i];
            }
            if (newWeight > MaxWeight) {
                return false;
            }
            else {
                for (int i = 0; i < itemsToLoad.Length; i++) {
                    HeldItems += itemsToLoad[i];
                }
                return true;
            }
        }
    }
}
