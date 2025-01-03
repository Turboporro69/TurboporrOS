﻿using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using TurboporrOS.Commands;
using Cosmos.System.FileSystem;
using TurboporrOS.Graphics;
using Cosmos.System.Graphics;

namespace TurboporrOS
{
    public class Kernel : Sys.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;
        public static GUI gui; 
        protected override void BeforeRun()
        {
            this.vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(this.vfs);
            this.commandManager = new CommandManager();

            Console.WriteLine("Welcome to TurboporrOS, an operating system made with C# for High Seas.\nUse help for start");
     
        }

        protected override void Run()
        {
            if (Kernel.gui != null)
            {

                Kernel.gui.handleGUIInputs();
                Kernel.gui.canvas.Display();

                return;
            }

            Console.WriteLine(this.commandManager.processInput(Console.ReadLine()));

        }
    }
}
