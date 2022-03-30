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
        public enum DirectionsEnum {Up, Down, Left, Right, None};
        private int width;
        private int height;
        private int points;
        private int defaultSpeed;
        private List<Point> snake;
        private DirectionsEnum direction;
        private DirectionsEnum previousMoveDirection;
        private Timer gameTimer;
        private Point food;
        private Random rand;
        public bool isGameOver;

        public delegate void myAction();
        public myAction GameChanged;

        public GameSnake(int width, int height, int tickrate)
        {
            isGameOver = false;
            points = 0;

            this.width = width;
            this.height = height;
            defaultSpeed = tickrate;

            snake = new List<Point>();
            snake.Add(new Point(this.width/2, this.height-3));
            snake.Add(new Point(this.width/2, this.height-2));
            snake.Add(new Point(this.width/2, this.height-1));

            direction = DirectionsEnum.Up;
            previousMoveDirection = DirectionsEnum.Up;

            rand = new Random();
            food = snake.First();
            while(snake.Contains(food))
            {
                food.X = rand.Next(width);
                food.Y = rand.Next(height);
            }

            gameTimer = new Timer();
            gameTimer.Interval = tickrate;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }
        public void Restart(int width, int height, int tickrate)
        {
            isGameOver = false;
            points = 0;

            this.width = width;
            this.height = height;
            defaultSpeed = tickrate;

            snake.Clear();
            snake.Add(new Point(this.width / 2, this.height - 3));
            snake.Add(new Point(this.width / 2, this.height - 2));
            snake.Add(new Point(this.width / 2, this.height - 1));

            direction = DirectionsEnum.Up;

            food = snake.First();
            while (snake.Contains(food))
            {
                food.X = rand.Next(width);
                food.Y = rand.Next(height);
            }

            gameTimer = new Timer();
            gameTimer.Interval = tickrate;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        internal void OnKeyDown(Keys keyCode)
        {
            switch(keyCode)
            {
                case Keys.Up: case Keys.W:
                    if(previousMoveDirection != DirectionsEnum.Down && gameTimer.Enabled) 
                        direction = DirectionsEnum.Up;
                    break;

                case Keys.Down: case Keys.S:
                    if (previousMoveDirection != DirectionsEnum.Up && gameTimer.Enabled)  
                        direction = DirectionsEnum.Down;
                    break;

                case Keys.Left: case Keys.A:
                    if (previousMoveDirection != DirectionsEnum.Right && gameTimer.Enabled) 
                        direction = DirectionsEnum.Left;
                    break;

                case Keys.Right: case Keys.D:
                    if (previousMoveDirection != DirectionsEnum.Left && gameTimer.Enabled) 
                        direction = DirectionsEnum.Right;
                    break;

                case Keys.Space:
                    gameTimer.Enabled = !gameTimer.Enabled;
                    break;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Point newHead = Point.Empty;
            switch(direction)
            {
                case DirectionsEnum.Up:
                    newHead = new Point(snake.First().X, snake.First().Y - 1);
                    previousMoveDirection = DirectionsEnum.Up;
                    break;

                case DirectionsEnum.Down:
                    newHead = new Point(snake.First().X, snake.First().Y + 1);
                    previousMoveDirection = DirectionsEnum.Down;
                    break;

                case DirectionsEnum.Left:
                    newHead = new Point(snake.First().X - 1, snake.First().Y);
                    previousMoveDirection = DirectionsEnum.Left;
                    break;

                case DirectionsEnum.Right:
                    newHead = new Point(snake.First().X + 1, snake.First().Y);
                    previousMoveDirection = DirectionsEnum.Right;
                    break;
            }
            
            if(newHead.X >= width) newHead.X -= width;
            else if(newHead.X < 0) newHead.X += width;

            if(newHead.Y >= height) newHead.Y -= height;
            else if(newHead.Y < 0) newHead.Y += height;

            if(snake.Contains(newHead))
            {
                gameTimer.Enabled = false;
                isGameOver = true;
            }

            snake.Insert(0, newHead);
            if(newHead != Food) snake.Remove(snake.Last());
            else
            {
                points += (int)(100*((float)defaultSpeed/gameTimer.Interval));
                gameTimer.Interval = (int)(gameTimer.Interval * 0.95);
                while (snake.Contains(food))
                {
                    food.X = rand.Next(0, width);
                    food.Y = rand.Next(0, height);
                }
            }

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
        public Point Food { get => food; }
        public int DefaultSpeed { get => defaultSpeed; }
        public int Points { get => points; }
    }
}
