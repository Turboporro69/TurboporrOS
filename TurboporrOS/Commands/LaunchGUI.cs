using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboporrOS.Graphics;

namespace TurboporrOS.Commands
{
    public class LaunchGUI : Command
    {
        public LaunchGUI(String name) : base(name) { }
        public override string execute(String[] args)
        {
            if (Kernel.gui!=null)
            
                return "GUI already launched";

            Kernel.gui = new GUI();
            return "GUI launched";
        } 
    
    }
}
