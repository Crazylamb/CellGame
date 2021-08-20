using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Game of life";
            comboBox1.SelectedIndex = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CreateGeneration();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
            //graphics.FillRectangle(Brushes.Red, 0, 0, resolution, resolution);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        //Start the game
        private void StartGame()
        {
            if (timer1.Enabled) { return; }

            nudDensity.Enabled = false;
            nudResolution.Enabled = false;
            resolution = (int)nudResolution.Value;

            game = new Game(pictureBox1.Height / resolution,
                pictureBox1.Width / resolution,
                (int)nudDensity.Minimum + (int)nudDensity.Maximum - (int)nudDensity.Value);

            resolution = (int)nudResolution.Value;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }

        //Create the gamefield
        private void CreateGeneration()
        {
            graphics.Clear(Color.Black);

            var field = game.GetCurrentGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x,y])
                        graphics.FillRectangle(GetBrushColor(), x * resolution, y * resolution, resolution - 1, resolution - 1);

                }
            }

            pictureBox1.Refresh();
            game.CreateGeneration();
        }

        //Change brushes color
        private Brush GetBrushColor()
        {
            if (comboBox1.SelectedItem.ToString() == "Green")
                return Brushes.Green;
            else if (comboBox1.SelectedItem.ToString() == "Yellow")
                return Brushes.Yellow;
            else
                return Brushes.Red;
        }

        //Stop timer
        private void StopGame()
        {
            if (!timer1.Enabled) { return; }
            timer1.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;

        }

        //To add or delete cells
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled) { return; }
            
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                game.AddCell(x, y);
            }

            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                game.DeleteCell(x, y);
            }
        }

        //Not needed yet
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
