using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboporrOS.Graphics
{
    public class LaunchTextEditor : Command
    {
        public LaunchTextEditor (String name) : base (name) { }

        public override string execute (string[] args) {
            if (args.Length!=3) return "Expected X,Y and File name arguments";

            Int32 x, y;

            if (!(Int32.TryParse(args[0],out x)))
                return "Expected a Number for argument 1 (X axis)"
            
        }
    }
}
