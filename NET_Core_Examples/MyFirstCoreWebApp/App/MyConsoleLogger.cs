using System;

namespace MyFirstCoreWebApp.App
{
    public class MyConsoleLogger : ILog
    {
        public void info(string str)
        {
           Console.WriteLine(str);
        }
    }
}