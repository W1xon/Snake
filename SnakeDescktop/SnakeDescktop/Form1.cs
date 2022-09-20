using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeDescktop
{
    public partial class Form1 : Form
    {
        private int maxScore = 1;
        Snake snake;
        Apple apple;
        int[] Level = 
        {
            500,
            400,
            300,
            200,
            100,
            50,
            30
        };
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Start();
            timerKeyDown.Interval = 1;
            timerKeyDown.Start();
            snake = new Snake();
            apple = new Apple(snake);
            comboBoxLevel.SelectedIndex = 0;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            snake.ShowSnake();
            maxScore = maxScore > snake.Lenght ? maxScore : snake.Lenght;
            labelCount.Text = "Score: " + snake.Lenght + "  Record: " + (maxScore > snake.Lenght ? maxScore : snake.Lenght);
            if (snake.Eat(apple))
                apple = new Apple(snake);
            snake.Move();
        }

        private void timerKeyDown_Tick(object sender, EventArgs e)
        {
            snake.KeyDown();
        }

        private void comboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Level[comboBoxLevel.SelectedIndex];
            snake.Destroy();
        }
    }
}
