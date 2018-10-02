using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Histo : Form
    {

        byte[] Image;
        int lignes;
        int colonnes;
        Bitmap bmp;
        Graphics g;

        public Histo(byte[] Image, int lignes, int colonnes)
        {
            InitializeComponent();
            this.Image = Image;
            this.lignes = lignes;
            this.colonnes = colonnes;

            
            this.bmp = new Bitmap(600, 400);
            
            
        }

        public Histo(Bitmap bmp)
        {

            InitializeComponent();
            this.bmp = bmp;
            
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            
        }

        private void NewThread()
        {

            Application.ExitThread();
            Thread Menu = new Thread(new ThreadStart(Thread));
            Menu.Start();

        }

        public void Thread()
        {
            Application.Run(new Histo(this.bmp));
        }

        public void Bleu()
        {
            this.g = Graphics.FromImage(bmp);
            int nbelements = Image.Length / 3; //le nombre de valeurs d'une des 3 couleurs dans l'image
            int nbbytes = Image.Length; //Le nombre de bytes
            int zone1 = 0;
            int zone2 = 0;
            int zone3 = 0;
            int zone4 = 0;
            int zone5 = 0;
            int zone6 = 0;

            for (int i = 0; i < nbbytes-3; i += 3)
            {
                if(40>this.Image[i])
                {
                    zone1++;
                }
                else if(40<=this.Image[i] && this.Image[i]<80)
                {
                    zone2++;
                }
                else if(80 <= this.Image[i] && this.Image[i] < 120)
                {
                    zone3++;
                }
                else if(120 <= this.Image[i] && this.Image[i] < 160)
                {
                    zone4++;
                }
                else if (160 <= this.Image[i] && this.Image[i] < 200)
                {
                    zone5++;
                }
                else
                {
                    zone6++;
                }
            }
            int z1 = zone1 / (nbelements/400) ; //400 est la hauteur en pixels
            this.g.DrawLine(new Pen(Color.Blue), 50, 400, 50, 400-z1);
            int z2 = zone2 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Blue), 150, 400, 150, 400-z2);
            int z3 = zone3 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Blue), 250, 400, 250, 400-z3);
            int z4 = zone4 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Blue), 350, 400, 350, 400-z4);
            int z5 = zone5 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Blue), 450, 400, 450, 400-z5);
            int z6 = zone6 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Blue), 550, 400, 550, 400-z6);
            //this.bmp = new Bitmap(600, 400, g);
            this.g.Dispose();
            

        }

        public void Rouge()
        {
            this.g = Graphics.FromImage(bmp);
            int nbelements = Image.Length / 3; //le nombre de valeurs d'une des 3 couleurs dans l'image
            int nbbytes = Image.Length; //Le nombre de bytes
            int zone1 = 0;
            int zone2 = 0;
            int zone3 = 0;
            int zone4 = 0;
            int zone5 = 0;
            int zone6 = 0;

            for (int i = 2; i < nbbytes - 3; i += 3)
            {
                if (40 > this.Image[i])
                {
                    zone1++;
                }
                else if (40 <= this.Image[i] && this.Image[i] < 80)
                {
                    zone2++;
                }
                else if (80 <= this.Image[i] && this.Image[i] < 120)
                {
                    zone3++;
                }
                else if (120 <= this.Image[i] && this.Image[i] < 160)
                {
                    zone4++;
                }
                else if (160 <= this.Image[i] && this.Image[i] < 200)
                {
                    zone5++;
                }
                else
                {
                    zone6++;
                }
            }
            int z1 = zone1 / (nbelements / 400); //400 est la hauteur en pixels
            this.g.DrawLine(new Pen(Color.Red), 70, 400, 70, 400 - z1);
            int z2 = zone2 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Red), 170, 400, 170, 400 - z2);
            int z3 = zone3 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Red), 270, 400, 270, 400 - z3);
            int z4 = zone4 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Red), 370, 400, 370, 400 - z4);
            int z5 = zone5 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Red), 470, 400, 470, 400 - z5);
            int z6 = zone6 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Red), 570, 400, 570, 400 - z6);
            //this.bmp = new Bitmap(600, 400, g);
            this.g.Dispose();
            

        }

        public void Vert()
        {
            this.g = Graphics.FromImage(bmp);
            int nbelements = Image.Length / 3; //le nombre de valeurs d'une des 3 couleurs dans l'image
            int nbbytes = Image.Length; //Le nombre de bytes
            int zone1 = 0;
            int zone2 = 0;
            int zone3 = 0;
            int zone4 = 0;
            int zone5 = 0;
            int zone6 = 0;

            for (int i = 1; i < nbbytes - 3; i += 3)
            {
                if (40 > this.Image[i])
                {
                    zone1++;
                }
                else if (40 <= this.Image[i] && this.Image[i] < 80)
                {
                    zone2++;
                }
                else if (80 <= this.Image[i] && this.Image[i] < 120)
                {
                    zone3++;
                }
                else if (120 <= this.Image[i] && this.Image[i] < 160)
                {
                    zone4++;
                }
                else if (160 <= this.Image[i] && this.Image[i] < 200)
                {
                    zone5++;
                }
                else
                {
                    zone6++;
                }
            }
            int z1 = zone1 / (nbelements / 400); //400 est la hauteur en pixels
            this.g.DrawLine(new Pen(Color.Green), 90, 400, 90, 400 - z1);
            int z2 = zone2 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Green), 190, 400, 190, 400 - z2);
            int z3 = zone3 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Green), 290, 400, 290, 400 - z3);
            int z4 = zone4 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Green), 390, 400, 390, 400 - z4);
            int z5 = zone5 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Green), 490, 400, 490, 400 - z5);
            int z6 = zone6 / (nbelements / 400);
            this.g.DrawLine(new Pen(Color.Green), 590, 400, 590, 400 - z6);
            //this.bmp = new Bitmap(600, 400, g);
            this.g.Dispose();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bleu();
            NewThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vert();
            NewThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rouge();
            NewThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.g = Graphics.FromImage(bmp);
            Bleu();

            this.g = Graphics.FromImage(bmp);
            Rouge();
            
            this.g = Graphics.FromImage(bmp);
            Vert();

            NewThread();
        }
    }
}
