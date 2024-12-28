using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;


namespace TurboporrOS.Graphics
{
    internal class ConsoleTab : Tab
    {
        public Pen consoleTextPen;
        public Font consoleTextFont;

        public Text inputText, responseText;

        private StringBuilder input;
        public ConsoleTab(Int32 _x, Int32 _y) : base(_x, _y)
        {
            this.consoleTextPen = new Pen(Color.White);
            this.consoleTextFont = new Font(Text.defaultFontData);

            this.inputText = new Text(0, Tab.defaultWindowSize-(Tab.windowTopPartSize+this.consoleTextFont.bpl+1), "_", this.consoleTextFont, this.consoleTextPen);
            this.responseText=new Text(0,(this.consoleTextFont.bpl+1)*2,"", this.consoleTextFont,this.consoleTextPen);
            this.input = new StringBuilder();

            this.addComponent(new Text(0,0, "TurboporrOS Console", this.consoleTextFont, this.consoleTextPen));
            this.addComponent(new Text(0, 17, "Type 'help' for help", this.consoleTextFont, this.consoleTextPen));
            this.addComponent(this.inputText);
            this.addComponent(this.responseText);

        }
        public override void onKeyPress(KeyEvent keyData)
        {
            if (keyData.Key==ConsoleKeyEx.Enter)
            {
                this.input.Clear();
                this.inputText.text = "_";
                this.inputText.draw(this);
            }
            else if (keyData.Key==ConsoleKeyEx.Backspace)
            {
                this.input=this.input.Remove(this.input.Length - 1, 1); 
                this.inputText.text = this.input.ToString();
                this.inputText.draw(this);
            }
            else {
                this.input.Append(keyData.KeyChar);
                this.inputText.text = this.input.ToString();
                this.inputText.draw(this);
            }
        }

    }
}
