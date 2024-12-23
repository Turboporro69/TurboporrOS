using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using System.Drawing;
using sys = Cosmos.System;

namespace TurboporrOS.Graphics
{
    public class Tab
    {
        private static List<Tab> tabs = new List<Tab>();
        private static Pen outlinePen = new Pen(Color.White),xBtnPen=new Pen(Color.Red);

        internal const Int32 defaultWindowSize = 400, windowTopPartSize = 25, xBtnSize = 25;
        private List<TabComponent> components;
        public Int32 X
        {
            get;

            private set;
        }

        public Int32 Y
        {
            get;
            private set;
        }

        private protected Boolean beingDragged;
        private protected Int32 dragDiffX, dragDiffY;

        public Tab (Int32 _x,Int32 _y)
        {
            Int32 x = _x, y = _y;

            if ((x > (Kernel.gui.canvas.Mode.Columns - Tab.defaultWindowSize)) || ((y > (Kernel.gui.canvas.Mode.Rows - (Tab.defaultWindowSize + 100)))));
            {
                x=Kernel.gui.canvas.Mode.Columns - (Tab.defaultWindowSize + 1);
                y = Kernel.gui.canvas.Mode.Rows - (Tab.defaultWindowSize + 100+1);
            }

            this.components = new List<TabComponent>();
            this.X = x;
            this.Y = y;
            this.beingDragged = false;

            Tab.tabs.Add(this);

        }
        private void draw ()
        {
            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen,this.X,this.Y,Tab.defaultWindowSize,Tab.windowTopPartSize);

            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen, this.X + (Tab.defaultWindowSize - Tab.xBtnSize),this.Y,Tab.xBtnSize,Tab.xBtnSize );
            Kernel.gui.canvas.DrawFilledRectangle(Tab.xBtnPen, this.X + (Tab.defaultWindowSize - (Tab.xBtnSize - 1)),this.Y+1, Tab.xBtnSize - 1, Tab.xBtnSize - 1);

            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen, this.X, this.Y + Tab.windowTopPartSize,Tab.defaultWindowSize, Tab.defaultWindowSize - Tab.windowTopPartSize);
        }
    }
}
