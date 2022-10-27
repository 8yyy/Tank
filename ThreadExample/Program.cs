using System;
using System.Threading;

namespace ThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread myThread = new Thread(new ThreadStart(_ChildThread));
            myThread.Start();


            Thread.Sleep(2000);
            _isRun = false;
        }
        private static bool _isRun = true;
        private static void _ChildThread()
        {
            while (_isRun)
            {
                Console.WriteLine("Listening...");
                Thread.Sleep(500);
            }
        }
    }
}
