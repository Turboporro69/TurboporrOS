using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace TurboporrOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.WriteLine("Welcome to TurboporrOS, an operating system made with C# just for fun");
        }

        protected override void Run()
        {

        }
    }
}
