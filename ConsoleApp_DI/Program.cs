namespace ConsoleApp_DI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle vehicle = new RaceCar();
            vehicle.Run();
        }
    }

    #region 刘铁锰 抽象类与接口
    interface IVehicle
    {
        void Stop();
        void Fill();
        void Run();
    }

    abstract class Vehicle : IVehicle
    {
        public void Fill()
        {
            Console.WriteLine("Stopped!");
        }

        public abstract void Run();

        public void Stop()
        {
            Console.WriteLine("Pay and fill...");
        }
    }

    class Car : Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Car is Running...");
        }
    }

    class Trunck : Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Truck is running...");
        }
    }

    class RaceCar : Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Race car is running...");
        }
    }
    #endregion
}
