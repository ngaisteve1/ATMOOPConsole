using System;
using ATMOOPProject;

namespace ATMConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            var app = new AtmApp();
            app.Initialization();
            app.Execute();
        }
    }
}
