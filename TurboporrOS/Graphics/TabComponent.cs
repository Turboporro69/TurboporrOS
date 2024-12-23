using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboporrOS.Graphics
{
    public class TabComponent
    {
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
        private readonly Byte size;

        public TabComponent(Int32 X, Int32 Y, Byte size) { 
            this.X = X;
            this.Y = Y;
            this.size = size;
        }

        public virtual void draw (Tab sender)
        {

        }
    }
}
