﻿using System;
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

        protected List<TabComponent> components;
        private List<Tuple<sys.Graphics.Point, Color>> pixelsBehindTab;

        private protected Boolean beingDragged;
        private protected Int32 dragDiffX, dragDiffY;

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

        public Boolean focused 
        { 
            get;
            private set;
        }

        public Tab (Int32 _x,Int32 _y)
        {
            Int32 x = _x, y = _y;

            if ((x > (Kernel.gui.canvas.Mode.Columns - Tab.defaultWindowSize)) || ((y > (Kernel.gui.canvas.Mode.Rows - (Tab.defaultWindowSize + 100)))))
            {
                x = Kernel.gui.canvas.Mode.Columns - (Tab.defaultWindowSize + 1);
                y = Kernel.gui.canvas.Mode.Rows - (Tab.defaultWindowSize + 100 + 1);
            }
            this.pixelsBehindTab = new List<Tuple<sys.Graphics.Point, Color>>(Tab.defaultWindowSize*Tab.defaultWindowSize);
            this.components = new List<TabComponent>();
            this.X = x;
            this.Y = y;
            this.beingDragged = false;
            if (Tab.tabs.Count == 0) this.focused = true;
            else this.focused = false;

            this.draw();

            Tab.tabs.Add(this);

        }
        private void draw ()
        {
            this.storePixelBehindTab();
            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen,this.X,this.Y,Tab.defaultWindowSize,Tab.windowTopPartSize);

            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen, this.X + (Tab.defaultWindowSize - Tab.xBtnSize),this.Y,Tab.xBtnSize,Tab.xBtnSize );
            Kernel.gui.canvas.DrawFilledRectangle(Tab.xBtnPen, this.X + (Tab.defaultWindowSize - (Tab.xBtnSize - 1)),this.Y+1, Tab.xBtnSize - 1, Tab.xBtnSize - 1);

            Kernel.gui.canvas.DrawRectangle(Tab.outlinePen, this.X, this.Y + Tab.windowTopPartSize,Tab.defaultWindowSize, Tab.defaultWindowSize - Tab.windowTopPartSize);

            foreach (TabComponent tc in this.components)
                tc.draw(this);
        }

        public void addComponent (TabComponent tabComponent)
        {
            this.components.Add(tabComponent);
            tabComponent.draw(this);
        }

        public void move (Int32 newX,Int32 newY)
        {
            this.restorePixelBehindTab();

            this.X = newX;
            this.Y = newY;
            this.draw();
        }

        public void close ()
        {
            this.restorePixelBehindTab();
            Tab.tabs.Remove(this);
        }

        public static void tryProcessTabLMBDown (Int32 mouseX, Int32 mouseY)
        {
            if (Tab.tabs.Count == 0) return;

            foreach (Tab tab in Tab.tabs)
            {
                Rectangle mouseLocation=new Rectangle(mouseX, mouseY, 1, 1);

                if (mouseLocation.IntersectsWith(new Rectangle(tab.X,tab.Y,Tab.defaultWindowSize,Tab.defaultWindowSize)))
                    tab.focused = true;

                if (mouseLocation.IntersectsWith(new Rectangle(tab.X, tab.Y, (Tab.defaultWindowSize - Tab.xBtnSize), Tab.windowTopPartSize)))
                {
                    tab.dragDiffX = (mouseX - tab.X);
                    tab.dragDiffY = (mouseY - tab.Y);
                    tab.beingDragged = true;
                }
                else if (mouseLocation.IntersectsWith(new Rectangle(tab.X + (defaultWindowSize - Tab.xBtnSize), tab.Y, Tab.xBtnSize, Tab.xBtnSize)))
                    tab.close();

            }
        } 

        public static void tryProcessTabLMBRelease (Int32 mouseX, Int32 mouseY)
        {
            if (Tab.tabs.Count == 0) return;

            foreach (Tab tab in Tab.tabs)
            {
                if (tab.beingDragged)
                {
                    tab.beingDragged = false;
                    tab.move(mouseX - tab.dragDiffX, mouseY - tab.dragDiffY);
                }
            }

        }

        private void storePixelBehindTab()
        {
            this.pixelsBehindTab.Clear();
            UInt16 x = 0, y = 0;

            Int32 fixedY, fixedX;

            while (y <= Tab.defaultWindowSize)
            {
                x = 0;

                while (x <=Tab.defaultWindowSize)
                {
                    fixedX = (this.X + x);
                    fixedY = this.Y + y;
                    this.pixelsBehindTab.Add(new Tuple<sys.Graphics.Point, Color>(new sys.Graphics.Point(fixedX, fixedY), Kernel.gui.canvas.GetPointColor(fixedX, fixedY)));
                    x++;
                }

                ++y;
            }
        }
        private void restorePixelBehindTab()
        {
            foreach (Tuple<sys.Graphics.Point, Color> pixelData in this.pixelsBehindTab) 
                Kernel.gui.canvas.DrawPoint(new Pen(pixelData.Item2), pixelData.Item1);
        }

        public void focus()
        {
            foreach (Tab tab in Tab.tabs)
            {
                if (tab.focused)
                {
                    tab.focused = false;
                }
            }

            this.focused = true;

        }

        public virtual void onKeyPress (sys.KeyEvent keyData) { }

        public static void tryProcessKeyPress(sys.KeyEvent keyData) 
        {
            if (Tab.tabs.Count == 0) return;
            
            foreach (Tab tab in Tab.tabs)
                if (tab.focused)
                    tab.onKeyPress(keyData);
            
        }
    }
}
