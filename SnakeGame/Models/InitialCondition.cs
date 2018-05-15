using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Models
{
  public class InitialCondition
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Position Position { get; set; }

        public InitialCondition()
        {
            Width = 15;
            Height = 15;
            Speed = 8;
            Score = 0;
            Points = 10;
            GameOver = false;
            Position = Position.Right;
        }
    }
}
