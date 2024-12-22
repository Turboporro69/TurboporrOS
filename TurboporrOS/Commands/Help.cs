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
            return "Hello World";
        }
    }
}
