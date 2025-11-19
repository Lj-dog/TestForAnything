using System.Net.Http.Headers;

namespace ConsoleApp_DI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 刘铁锰 抽象类与开闭原则
            //Vehicle adf;
            //Vehicle vehicle = new RaceCar();
            //vehicle.Run();

            //RaceCar raceCar;
            //raceCar.Run();
            #endregion

            #region 刘铁猛 DI

            #endregion

            #region 接口与抽象类结合解耦 士兵 猎人 开枪
            SolidersBase solider1 = new Cook(RANK.OR_1);
            SolidersBase solider2 = new Army(RANK.OR_2);

            Hunter hunter = new();
            ShootHelper.SomeOneShoot(hunter);
            ShootHelper.SomeOneShoot((IShooting)solider2);
            if (solider1 is IShooting shooter1)
                ShootHelper.SomeOneShoot(shooter1);
            else
            {
                Console.WriteLine("don't know shooting");
            }
            if (solider2 is IShooting shooter2)
                ShootHelper.SomeOneShoot(shooter2);
            else
            {
                Console.WriteLine("don't know shooting");
            }
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

    #region 接口与抽象类结合解耦 士兵 猎人 开枪
    /// <summary>
    /// 等级
    /// </summary>
    enum RANK
    {
        OR_1,
        OR_2,
    };


    abstract class SolidersBase
    {
        protected RANK rank;

        public SolidersBase(RANK rank)
        {
            this.rank = rank;
        }

        public abstract RANK GetRank();
    }

    /// <summary>
    /// 炊事员
    /// </summary>
    class Cook : SolidersBase
    {
        public Cook(RANK rank)
            : base(rank) { }

        public override RANK GetRank()
        {
            return rank;
        }
    }

    /// <summary>
    /// 陆军
    /// </summary>
    class Army : SolidersBase, IShooting
    {
        public Army(RANK rank)
            : base(rank) { }

        public override RANK GetRank()
        {
            return rank;
        }

        public void Shooting()
        {
            Console.WriteLine("Solider shoot");
        }
    }

    /// <summary>
    /// 开枪
    /// </summary>
    interface IShooting
    {
        public void Shooting();
    }

    class Hunter : IShooting
    {
        public void Shooting()
        {
            Console.WriteLine("Hunter shoot");
        }
    }

    static class ShootHelper
    {
        public static void SomeOneShoot(IShooting shooter)
        {
            shooter.Shooting();
        }
    }
    #endregion
}
