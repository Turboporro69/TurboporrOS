using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboporrOS.Commands
{
    public class Help : Command
    {
        public Help (String name) : base(name) { }

        public override string execute(string[] args)
        {
            return "Available commands:\n" +
                "help - Shows this message\n" +
                "file cfile - Creates a file\n" +
                "file rfile - Removes a file\n" +
                "file cdir - Creates a directory\n" +
                "file rdir - Removes a directory\n" +
                "file writestr - Writes a string to a file\n" +
                "file readstr - Reads a string from a file\n" +
                "launchgui - Launches the GUI \n(The GUI works very slowly, so if you try it, it won't crash. It just updates \nthe screen every 7 seconds)";
        }
    }
}
