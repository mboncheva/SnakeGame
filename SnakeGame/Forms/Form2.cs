using SnakeGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Forms
{
    public partial class Form2 : Form
    {
        private List<Coordinate> snake = new List<Coordinate>();
        private Coordinate food = new Coordinate();

        public Form2()
        {
            InitializeComponent();
            ChangeSizeOnForm();

            new InitialCondition();

            GameTimer.Interval = 1000 / InitialCondition.Speed;
            LoadEvents();
            GameTimer.Start();

            label1.Visible = false;
            StartNewGame();
        }

        // Methods
        private void StartNewGame()
        {
            new InitialCondition();
            label1.Visible = false;
            snake.Clear();

            var headSnake = new Coordinate
            {
                X = 10,
                Y = 5
            };

            snake.Add(headSnake);

            GenerateFood();
        }

        private void GenerateFood()
        {
            var maxXpos = this.Size.Width / InitialCondition.Width - 2;
            var maxYpos = this.Size.Height / InitialCondition.Height - 3;

            var random = new Random();

            food = new Coordinate
            {
                X = random.Next(0, maxXpos),
                Y = random.Next(0, maxYpos)
            };

        }

        private void MoveSnake()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (InitialCondition.Position)
                    {
                        case Position.Up:
                            snake[i].Y--;
                            break;
                        case Position.Down:
                            snake[i].Y++;
                            break;
                        case Position.Left:
                            snake[i].X--;
                            break;
                        case Position.Right:
                            snake[i].X++;
                            break;
                    }

                    var maxW = this.Width / InitialCondition.Width - 1;
                    var maxH = this.Height / InitialCondition.Height - 3;

                    if (snake[i].X >= maxW || snake[i].Y >= maxH || snake[i].X < 0 || snake[i].Y < 0)
                    {
                        Die();
                    }

                    for (int j = 1; j < snake.Count; j++)
                    {
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            Die();
                        }
                    }

                    if (snake[0].X == food.X && snake[0].Y == food.Y)
                    {
                        Eat();
                    }


                }
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }

        }

        private void Die()
        {
            InitialCondition.GameOver = true;
        }

        private void Eat()
        {
            var food = new Coordinate
            {
                X = snake[snake.Count - 1].X,
                Y = snake[snake.Count - 1].Y
            };

            snake.Add(food);
            InitialCondition.Score += InitialCondition.Points;
            GenerateFood();
        }

        private void LoadEvents()
        {
           GameTimer.Tick += GameTimer_Tick;
           Paint += Form2_Paint;
           KeyDown += Form2_KeyDown;
        }

        private void ChangeSizeOnForm()
        {
            var form = new Form1();
            this.Size = new Size
            {
                Height = form.Height,
                Width = form.Width

            };
        }

        // Events
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (InitialCondition.GameOver == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    StartNewGame();
                }
                else if(e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Right && InitialCondition.Position != Position.Left)
                {
                    InitialCondition.Position = Position.Right;
                }
                else if (e.KeyCode == Keys.Left && InitialCondition.Position != Position.Right)
                {
                    InitialCondition.Position = Position.Left;
                }
                else if (e.KeyCode == Keys.Down && InitialCondition.Position != Position.Up)
                {
                    InitialCondition.Position = Position.Down;
                }
                else if (e.KeyCode == Keys.Up && InitialCondition.Position != Position.Down)
                {
                    InitialCondition.Position = Position.Up;
                }
                
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics p = e.Graphics;
            if (InitialCondition.GameOver != true)
            {
                Brush snakeColor;
                var foodColor = Brushes.DarkRed;

                for (int i = 0; i < snake.Count; i++)
                {

                    if (i == 0)
                    {
                        snakeColor = Brushes.Green;
                    }
                    else
                    {
                        snakeColor = Brushes.ForestGreen;
                    }

                    p.FillEllipse(snakeColor, new Rectangle(snake[i].X * InitialCondition.Width,
                                                           snake[i].Y * InitialCondition.Height,
                                                            InitialCondition.Width, InitialCondition.Height));

                    p.FillEllipse(Brushes.Red, new Rectangle(food.X * InitialCondition.Width,
                                                                    food.Y * InitialCondition.Height,
                                                                    InitialCondition.Width, InitialCondition.Height));
                }
            }
            else
            {
                var gameOver =
             $"Game Over{Environment.NewLine}Your final score is: {InitialCondition.Score}{Environment.NewLine}Press Entert to Start New Game";

                label1.Text = gameOver;
                label1.Visible = true;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            this.Invalidate();
        }
    }
}
