using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pikachu
{
    public partial class Form1 : Form
    {
        private bool flag = true;
        private static GameModel myGameModel = new GameModel(20, 16, 36);
        public PictureBox[,] px = new PictureBox[myGameModel._height, myGameModel._width];
        struct _point
        {
            public int row, col;
        }
        _point _point1 = new _point();
        _point _point2 = new _point();
        public Form1()
        {
            InitializeComponent();
            
            this.Height = myGameModel._height * 65 + 53;
            this.Width = myGameModel._width * 53 + 33;
            for (int i = 0; i < myGameModel._height; i++)
            {
                for (int j = 0; j < myGameModel._width; j++)
                {
                    px[i,j] = new PictureBox();
                    px[i,j].Width = 53; px[i,j].Height = 65;
                    px[i,j].Top = 10 + i * 65; px[i,j].Left = 10 + j * 53;
                    try
                    {
                        string name = "pic" + myGameModel.getCell(i,j).ToString() + ".jpg";
                        px[i,j].Image = Image.FromFile(name);
                        this.Controls.Add(px[i,j]);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    px[i, j].Name = i.ToString() + "x" + j.ToString();
                    px[i,j].Click += new EventHandler(pictureBoxClickEventhandle);
                    px[i,j].MouseHover += new EventHandler(pictureBoxMouseHoverEventhandle);
                    px[i,j].MouseLeave += new EventHandler(pictureBoxMouseLeaveEventhandle);
                }
            }
        }
        public void pictureBoxMouseLeaveEventhandle(object sender, EventArgs e)
        {
            
        }
        public void pictureBoxMouseHoverEventhandle(object sender, EventArgs e)
        {
            
        }
        
        public void pictureBoxClickEventhandle(object sender, EventArgs e)
        {
            var control  = (Control)sender;
            var x = int.Parse(control.Name.Split('x')[0]);
            var y = int.Parse(control.Name.Split('x')[1]);
            makeClickedCell(sender as PictureBox);
            flag = !flag;
            if (!flag)
            {
                _point1.row = x;
                _point1.col = y;
                
            }
            else
            {
                _point2.row = x;
                _point2.col = y;
                if (myGameModel.CheckWid(_point1.row, _point1.col, _point2.row, _point2.col))
                {
                    (sender as PictureBox).Image = null;
                    px[_point1.row, _point1.col].Image = null;
                }
                else if (myGameModel.CheckHei(_point1.row, _point1.col, _point2.row, _point2.col))
                {
                    (sender as PictureBox).Image = null;
                    px[_point1.row, _point1.col].Image = null;
                }
                else if (myGameModel.CheckCom(_point1.row, _point1.col, _point2.row, _point2.col))
                {
                    (sender as PictureBox).Image = null;
                    px[_point1.row, _point1.col].Image = null;
                }
            }
        }

        public void makeClickedCell(PictureBox px)
        {
            float ratio = 0.27f;

            //convert image in picture to bitmap
            Bitmap bmp = (Bitmap)px.Image;

            for (int w = 0; w < bmp.Width; w++)
            {
                for (int h = 0; h < bmp.Height; h++)
                {
                    // get the pixel at position (w, h) of image
                    Color c = bmp.GetPixel(w, h);

                    // create new pixel
                    Color newC = Color.FromArgb((int)(ratio * c.A), (int)(ratio * c.R), (int)(ratio * c.G), (int)(ratio * c.B));

                    // change the value of pixel at posion (w, h)
                    bmp.SetPixel(w, h, newC);
                }
            }

            // update image in picture box
            px.Image = bmp;
        }
    }
}
