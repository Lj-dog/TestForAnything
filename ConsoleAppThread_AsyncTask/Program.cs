using System;
using System.Threading;

public class ServerClass
{

    static int count = 0;
    // The method that will be called when the thread is started.
    public void InstanceMethod()
    {
        Console.WriteLine(
            "ServerClass.InstanceMethod is running on another thread.");

        int data = count++;
        // Pause for a moment to provide a delay to make
        // threads more apparent.
        Thread.Sleep(3000);
        Console.WriteLine(
            "The instance method called by the worker thread has ended. " + data);
    }
}

public class Simple
{
    public static void Main()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateThreads();
        }
    }
    public static void CreateThreads()
    {
        ServerClass serverObject = new ServerClass();

        Thread InstanceCaller = new Thread(new ThreadStart(serverObject.InstanceMethod));
        // Start the thread.
        InstanceCaller.Start();

        Console.WriteLine("The Main() thread calls this after "
            + "starting the new InstanceCaller thread.");

    }
}