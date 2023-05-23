using System;
using System.Linq;


namespace ProjectCMD
{
    internal class GasStationVia
    {
        public static void GasStation(Cars[] car, int gasolinePump)
        {
            var order = car.OrderBy(e => e.time);
            var count = 0;
            foreach (Cars i in order)
            {
                if (count == gasolinePump)
                {
                    Console.WriteLine("Ops,looks like the Gas Pumps are busy, wait your turn sir:");
                    count = count - 1;
                }
                InsertDb.Insert(i.name, i.time);
                Console.WriteLine($"Free Bomb: Car:{i.name}-Time:{i.time} segs");
                count++;
            }
        }
    }
}
