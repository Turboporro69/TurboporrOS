using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using TurboporrOS.Commands;
using Cosmos.System.FileSystem;

namespace TurboporrOS
{
    public class Kernel : Sys.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;
        protected override void BeforeRun()
        {
            this.vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
            Console.Clear();
            this.commandManager = new CommandManager();
            Console.WriteLine("Welcome to TurboporrOS, an operating system made with C# just for fun");
     
        }

        protected override void Run()
        {
            String response;
            String input = Console.ReadLine();
            response = this.commandManager.processInput(input);
            Console.WriteLine(response);
        }
    }
}
