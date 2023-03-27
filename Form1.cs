using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing.Printing;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        private int flag = -1;
        private Bitmap bmp;
        private double g = 9.81;
        private int time = 10;
        private double dt = 0.0005;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textBoxX.Text);
            double l = double.Parse(textBoxL.Text);
            double n = double.Parse(textBoxN.Text);
            flag *= -1;
            startModulation(x, l, n);
        }
        private async void startModulation(double x, double l, double n)
        {
            double x0 = x;
            double O = 0;
            double w = Math.Sqrt(g / l);
            double xgrad =40;
            int x1;
            int y1;
            int y2  = pictureBox1.Height / 2;
            int x2  = pictureBox1.Width / 2;
            while(flag == 1)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                O += xgrad * dt;
                x += (-2 * n * x - Math.Pow(w, 2) * Math.Sin(O)) * dt;

                x1 = pictureBox1.Width / 2 + (int)Math.Round(x * Math.Sin(O));
                y1 = pictureBox1.Height / 2 + (int)Math.Sqrt(Math.Pow(l,2) - Math.Pow(x1 - x2,2));

                    brezenhem(bmp, x1, x2, y1, y2);
                pictureBox1.Image = bmp;
                await Task.Delay(1);
            }
        }

        public void brezenhem(Bitmap bitmap, int x1, int x2, int y1, int y2)
        {
            int Px = Math.Abs(x2 - x1);
            int Py = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;
            int E = Px - Py;
            bitmap.SetPixel(x1, y1, Color.Black);
            bitmap.SetPixel(x2, y2, Color.Black);
            while (x1 != x2 || y1 != y2)
            {
                bitmap.SetPixel(x1, y1, Color.Black);
                int E2 = E * 2;
                if (E2 > -Py)
                {
                    E -= Py;
                    x1 += signX;
                }
                if (E2 < Px)
                {
                    E += Px;
                    y1 += signY;
                }
            }
        }

    }
}
