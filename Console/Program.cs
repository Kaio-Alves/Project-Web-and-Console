
namespace ProjectCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Cars[] car = new Cars[] { new Cars { name = "Carro1", time = 10}, new Cars { name = "Carro2", time = 5},
                new Cars { name = "Carro3", time = 1}, new Cars { name = "Carro4", time = 1}, 
                new Cars { name = "Carro5", time = 1}};
            int gasolinePump = 3;
            GasStationVia.GasStation(car, gasolinePump);
        }
    }
}
