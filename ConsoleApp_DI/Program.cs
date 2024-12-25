namespace ConsoleApp_DI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 刘铁锰 抽象类与开闭原则
            Vehicle vehicle = new RaceCar();
            vehicle.Run(); 
            #endregion
        }
    }

    #region 刘铁锰 抽象类与开闭原则
    interface IVehicle
    {
        void Stop();
        void Fill();

        /// <summary>
        /// Run
        /// </summary>
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
        /// <summary>
        /// RaceCarRun
        /// </summary>
        public override void Run()
        {
            Console.WriteLine("Race car is running...");
        }
    }
    #endregion
}
