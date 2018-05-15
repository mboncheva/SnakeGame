using SnakeGame.Forms;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSnakeImage();
        }

        private void LoadSnakeImage()
        {
            var imgPnlWidth = panel1.Width;
            var imgPnlHeight = panel1.Height;
            label1.Text = "Press Enter to Start Game";
            var image = new Bitmap(Resources.hqdefault, imgPnlWidth, imgPnlHeight);
            panel1.BackgroundImage = image;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Hide();
                var game = new Form2();
                game.ShowDialog();
                this.Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
            }
        }
    }
}
