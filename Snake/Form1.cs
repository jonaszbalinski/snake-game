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
    public partial class Form : System.Windows.Forms.Form
    {

        GameSnake gameSnake;
        Graphics g;
        public Form()
        {
            InitializeComponent();
            gameSnake = new GameSnake(50, 30, 150);
            gameSnake.GameChanged += DrawGame;

            DrawGame();
        }

        private void DrawGame()
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.Gray);

            Color snakeColor = Color.Green;
            if (gameSnake.isGameOver) snakeColor = Color.Black;

            float tileWidth = (float)pictureBox.Width / gameSnake.Width;
            float tileHeight = (float)pictureBox.Height / gameSnake.Height;

            for (int x = 0; x < gameSnake.Width; x++)
            {
                for(int y = 0; y < gameSnake.Height; y++)
                {
                    Color penColor = Color.DarkGray;
                    if ((x + y) % 2 == 0) penColor = Color.Gray;

                    g.FillRectangle(new SolidBrush(penColor), 
                        new Rectangle((int)(tileWidth * x),
                                      (int)(tileHeight * y),
                                      (int)(tileWidth),
                                      (int)(tileHeight)));
                }
            }

            foreach(Point p in gameSnake.Snake)
            {
                g.FillEllipse(new SolidBrush(snakeColor), 
                    new Rectangle((int)(tileWidth * p.X),
                                  (int)(tileHeight * p.Y),
                                  (int)(tileWidth),
                                  (int)(tileHeight)));
            }

            g.FillEllipse(new SolidBrush(Color.Red),
                    new Rectangle((int)(tileWidth * gameSnake.Food.X),
                                  (int)(tileHeight * gameSnake.Food.Y),
                                  (int)(tileWidth),
                                  (int)(tileHeight)));

            labelScore.Text = "Your score: " + gameSnake.Points.ToString();
            float mod = (float)gameSnake.DefaultSpeed / gameSnake.GameTimer.Interval;
            labelSpeed.Text = "Current speed modifier: x" + mod.ToString("n2");

            if (gameSnake.isGameOver)
            {
                if (MessageBox.Show("Do you want to restart game?", 
                    "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gameSnake.Restart(gameSnake.Width, gameSnake.Height, gameSnake.GameTimer.Interval);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            gameSnake.OnKeyDown(e.KeyCode);
        }
    }
}
