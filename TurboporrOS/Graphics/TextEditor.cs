using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using Sys=Cosmos.System;
using System.IO;
using Cosmos.System;

namespace TurboporrOS.Graphics
{
    public class TextEditor : Tab
    {
        private Text richTextBox;

        public Font font;
        public Pen pen;
        public readonly String openedFile;
        public TextEditor (Int32 _x,Int32 _y, String fileName, Font font,Pen pen) : base (_x,_y)
        {
            this.font = font;
            this.pen = pen;
            this.richTextBox = new Text(0, 0, "", this.font, this.pen,true);
            try {
                if (!(Sys.FileSystem.VFS.VFSManager.FileExists(fileName))) {
                    Sys.FileSystem.VFS.VFSManager.CreateFile(fileName);
                    this.components.Add(this.richTextBox); 
                    this.openedFile = fileName;             
                    return;
                }
            }

            catch {
                this.close();
                return;

            }

            try {
                FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(fileName).GetFileStream();
                if (fs.CanRead&&fs.CanWrite) {
                    Byte[] dataArr = new Byte[fs.Length];
                    fs.Read(dataArr, 0, dataArr.Length);
                    this.richTextBox.text = Encoding.ASCII.GetString(dataArr);
                }
                else {
                    this.close();
                    return;
                }
            }

            catch {
                this.close();
                return;
            }
            this.openedFile = fileName;
            this.components.Add(this.richTextBox);
            this.richTextBox.draw(this);

        }

        public override void onKeyPress(KeyEvent keyData)
        {
            if (keyData.Key==ConsoleKeyEx.Backspace) {
                this.richTextBox.text = this.richTextBox.text.Substring(0,this.richTextBox.text.Length-1);
                this.richTextBox.draw(this);
            }
            else if (keyData.Key==ConsoleKeyEx.Enter) {
                this.richTextBox.text += "\n";
                this.richTextBox.draw(this);
            }
            else if (keyData.Key==ConsoleKeyEx.Escape) {
                try {
                    FileStream fs=(FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(this.openedFile).GetFileStream();

                    if (fs.CanWrite) {
                        Byte[] data=Encoding.ASCII.GetBytes(this.richTextBox.text);
                        fs.Write(data,0,data.Length);
                        fs.Close();
                    }
                    else return;
                    
                }
                catch {

                }
            }
        }
    }
}
