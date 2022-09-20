using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace SnakeDescktop
{
    internal class Snake
    {
        private static int vectorX = 1, vectorY = 0;
        private int distanceX = 135, distanceY = 40;
        public int Lenght = 1;

        public List<Form> BodySnake = new List<Form>()
        {
            new Form()
            {
                Location = Spawn(),
                Size = new Size(150,50),
                StartPosition = FormStartPosition.Manual,
                ForeColor = Color.Green,
                BackColor = Color.Green,
                ControlBox = false,
                Text = "Snake",
                ShowInTaskbar = false
            }
        };
        public void Append()
        {
            BodySnake.Add
                (new Form()
                {
                    Location = new Point(BodySnake[BodySnake.Count - 1].Location.X, BodySnake[BodySnake.Count - 1].Location.Y),
                    Size = BodySnake[BodySnake.Count - 1].Size,
                    StartPosition = FormStartPosition.Manual,
                    ForeColor = Color.Green,
                    BackColor = Color.Green,
                    ControlBox = false,
                    Text = "Snake",
                    ShowInTaskbar = false
                });
            BodySnake[0].Focus();
        }
        public void Destroy()
        {
            vectorX = 1;
            vectorY = 0;
            for (int i = 0; i != BodySnake.Count;)
            {
                BodySnake[0].Close();
                BodySnake.RemoveAt(0);
            }
            BodySnake = new List<Form>()
            {
            new Form()
            {
                Location = Spawn(),
                Size = new Size(150,50),
                StartPosition = FormStartPosition.Manual,
                ForeColor = Color.Green,
                BackColor = Color.Green,
                ControlBox = false,
                Text = "Snake",
                ShowInTaskbar = false
            }
           };
        }
        public void Move()
        {
            Lenght = BodySnake.Count;
            Form snakeHead = BodySnake[0];
            Point lastPointHead = snakeHead.Location;
            Point lastPointBody = new Point();
            Point lastPointBody2;
            snakeHead.Focus();

            snakeHead.Location = new Point(snakeHead.Location.X + (distanceX * vectorX), snakeHead.Location.Y + (distanceY * vectorY));

            if (snakeHead.Location.X >= Screen.PrimaryScreen.Bounds.Width)
                Destroy();
            if (snakeHead.Location.Y >= Screen.PrimaryScreen.Bounds.Height)
                Destroy();
            if (snakeHead.Location.Y < 0)
                Destroy();
            if (snakeHead.Location.X < 0)
                Destroy();

            for (int i = 1; i < BodySnake.Count; i++)
            {
                if (snakeHead.Location == BodySnake[i].Location)
                    Destroy();
            }
            for (int i = 1; i < BodySnake.Count; i++)
            {
                if (i == 1)
                {
                    lastPointBody = BodySnake[i].Location;
                    BodySnake[i].Location = lastPointHead;
                }
                else
                {
                    lastPointBody2 = BodySnake[i].Location;
                    BodySnake[i].Location = lastPointBody;
                    lastPointBody = lastPointBody2;
                }

            }
        }

        private static Point Spawn()
        {
            Random random = new Random();
            vectorX = random.Next(2);
            if (vectorX == 0)
                vectorY = random.Next(2);
            int X = 0, Y = 0;
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
            return new Point(X, Y);
        }
        public void KeyDown()
        {
            BodySnake[0].KeyDown += SnakeHead_KeyDown;
        }
        private void SnakeHead_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (vectorY != 1)
                    {
                        vectorX = 0;
                        vectorY = -1;
                    }
                    break;
                case Keys.S:
                    if (vectorY != -1)
                    {
                        vectorX = 0;
                        vectorY = 1;
                    }
                    break;
                case Keys.A:
                    if (vectorX != 1)
                    {
                        vectorX = -1;
                        vectorY = 0;
                    }
                    break;
                case Keys.D:
                    if (vectorX != -1)
                    {
                        vectorX = 1;
                        vectorY = 0;
                    }
                    break;
                case Keys.Space:
                    Append();
                    break;

            }
        }

        public void ShowSnake()
        {
            foreach (Form body in BodySnake)
            {
                body.Show();
            }
        }
        public bool Eat(Apple apple)
        {
            if (BodySnake[0].Location == apple.pointApple)
            {
                Append();
                apple.Exit();
                return true;
            }
            return false;
        }
    }
}
