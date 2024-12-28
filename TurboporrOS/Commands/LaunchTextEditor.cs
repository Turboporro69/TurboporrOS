using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboporrOS.Graphics;
using Cosmos.System.Graphics;
using System.Drawing;

namespace TurboporrOS.Commands
{
    public class LaunchTextEditor : Command
    {
        public LaunchTextEditor (String name) : base (name) { }

        public override string execute (string[] args) {
            if (args.Length!=3) return "Expected X,Y and File name arguments";

            Int32 X, Y;

            if (!(Int32.TryParse(args[0], out X)))
                return "Expected a Number for argument 1 (X axis)";

            if (!(Int32.TryParse(args[0], out Y)))
                return "Expected a Number for argument 2 (Y axis)";

            new TextEditor(X, Y, args[2], new Font(Text.defaultFontData), new Pen(Color.White));

            return "Text Editor Launched";

        }
    }
}
