using System;
using System.Collections.Generic;

namespace hashCode
{
    struct Pos
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class MainClass
	{
		public static void Main (string[] args)
		{
			List<string> lines = new List<string> ();

			String line;
			while ((line = Console.ReadLine ().Trim()) != null && line != "") {
				lines.Add (line);
			}

			String[] lineArray = lines.ToArray();

			int mapWidth = 0,
			mapHeight = 0,
			numDrones = 0,
			maxTurns = 0,
			maxPayload = 0;

			int[] itemWeights = null;

			for (int i = 0; i < lineArray.Length; i++) {
				String current = lineArray [i];
				if (i == 0) { // World data
					String[] parts = current.Split (new []{ " " }, StringSplitOptions.RemoveEmptyEntries);
					mapWidth = int.Parse (parts [0]);
					mapHeight = int.Parse (parts [1]);
					numDrones = int.Parse (parts [2]);
					maxTurns = int.Parse (parts [3]);
					maxPayload = int.Parse (parts [4]);
				} else if (i == 1) { // How many items
					itemWeights = new int[int.Parse (current)];
				} else if (i == 2) { // Fill item weights array
					String[] parts = current.Split (new []{ " " }, StringSplitOptions.RemoveEmptyEntries);
					for (int j = 0; j < itemWeights.Length; j++) {
						itemWeights [j] = int.Parse (parts [j]);
					}
				}
			}
		}
	}
}
