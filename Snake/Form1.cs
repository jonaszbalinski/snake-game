using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {

        GameSnake gameSnake;
        public Form1()
        {
            InitializeComponent();
            gameSnake = new GameSnake(20, 15);
            DrawGame();
        }

        private void DrawGame()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(pictureBox.Image);
            //g.Clear(Color.Gray);

            for(int x = 0; x < gameSnake.Width; x++)
            {
                for(int y = 0; y < gameSnake.Height; y++)
                {
                    Color penColor = Color.DarkGray;
                    if ((x + y) % 2 == 0) penColor = Color.Gray;

                    g.FillRectangle(new SolidBrush(penColor), 
                        new Rectangle(pictureBox.Width/gameSnake.Width * x,
                                      pictureBox.Height / gameSnake.Height * y, 
                                      pictureBox.Width / gameSnake.Width, 
                                      pictureBox.Height / gameSnake.Height));
                }
            }

            foreach(Point p in gameSnake.Snake)
            {

            }
        }
    }
}
