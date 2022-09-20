using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SnakeDescktop
{
    internal class Apple
    {
        public int X = 1;
        public int Y = 1;
        public Point pointApple;
        private Random random = new Random();
        private Snake snake;
        private Form AppleFrm = new Form()
        {
            Size = new Size(150, 50),
            StartPosition = FormStartPosition.Manual,
            ForeColor = Color.Red,
            BackColor = Color.Red,
            ControlBox = false,
            ShowInTaskbar = false
        };

        private bool InsideSnake()
        {
            foreach (var bodySnake in snake.BodySnake)
            {
                if (pointApple == bodySnake.Location)
                {
                    return true;
                }
            }
            return false;
        }

        public Apple(Snake snake)
        {
            this.snake = snake; 
            AppleFrm.Text = "Apple";
            do
            {
                do
                {
                    X = random.Next(1, Screen.PrimaryScreen.Bounds.Width - 150);
                }
                while (X % 135 != 0);
                do
                {
                    Y = random.Next(1, Screen.PrimaryScreen.Bounds.Height - 50);
                }
                while (Y % 40 != 0);
                pointApple = new Point(X, Y);
            } while (InsideSnake());
            AppleFrm.Location = pointApple;
            AppleFrm.Show();
        }
        public void Exit()
        {
            AppleFrm.Close();
        }
    }
}
