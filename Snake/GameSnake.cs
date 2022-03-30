using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    class GameSnake
    {
        public enum DirectionsEnum {Up, Down, Left, Right};
        private int width;
        private int height;
        private List<Point> snake;
        private DirectionsEnum direction;
        private Timer gameTimer;
        private Point food;

        public delegate void myAction();
        public myAction GameChanged;

        public GameSnake(int width, int height)
        {
            this.width = width;
            this.height = height;

            snake = new List<Point>();
            snake.Add(new Point(this.width/2, this.height-3));
            snake.Add(new Point(this.width/2, this.height-2));
            snake.Add(new Point(this.width/2, this.height-1));

            direction = DirectionsEnum.Up;

            gameTimer = new Timer();
            gameTimer.Interval = 500;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Point newHead = Point.Empty;
            switch(direction)
            {
                case DirectionsEnum.Up:
                    newHead = new Point(snake.First().X, snake.First().Y - 1); 
                    break;

                case DirectionsEnum.Down:
                    newHead = new Point(snake.First().X, snake.First().Y + 1); 
                    break;

                case DirectionsEnum.Left:
                    newHead = new Point(snake.First().X - 1, snake.First().Y); 
                    break;

                case DirectionsEnum.Right:
                    newHead = new Point(snake.First().X + 1, snake.First().Y); 
                    break;
            }

            snake.Insert(0, newHead);
            snake.Remove(snake.Last());

            if(GameChanged != null)
            {
                GameChanged();
            }
        }

        public int Width { get => width;}
        public int Height { get => height;}
        public List<Point> Snake { get => snake;}
        internal DirectionsEnum Direction { get => direction;}
        public Timer GameTimer { get => gameTimer;}
    }
}
