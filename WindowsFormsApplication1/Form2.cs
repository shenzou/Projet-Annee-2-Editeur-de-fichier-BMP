using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; //Utilisé pour la PictureBox uniquement
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //Utilisé pour la fenêtre
using System.Threading; //Utilisé pour les différents processus
using System.IO;


// IMPORTANT !!
//Dans tout mon programme, La classe Image est seulement utilisée pour afficher l'image dans l'interface.
//Elle n'est en aucun cas utilisée pour traiter l'image.

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        string chemin;
        int menu;
        myFile abc;
        string tempfile;
        bool mod = true;

        public Form2(string chemin) //Constructeur simple
        {
            InitializeComponent();
            this.chemin = chemin; //Stocke l'adresse du fichier
            abc = new myFile(this.chemin); //Crée l'élément de la classe matrice à partir de l'adresse
            tempfile = "temp.bmp"; //Adresse du fichier où sont effectuées les modifications temporaires
             
        }


        public Form2(string chemin, bool modif) //Constructeur s'exécutant uniquement s'il y a eu un échec lors de la modification de l'image
        {
            InitializeComponent();
            this.chemin = chemin;
            menu = 0;
            abc = new myFile(this.chemin);
            tempfile = "temp.bmp";
            label6.Text = "Erreur, réessayez plusieurs fois !";
        }

        private void button1_Click(object sender, EventArgs e) //Permet de modifier un pixel dans l'image.
        {
            abc.SetPixel(1, 3, 255, 0, 255); 
            mod = abc.EcrireImage(tempfile); //Applique la modification à la matrice de l'image
            pb_Methode(Image.FromFile(tempfile),tempfile); //Affiche l'image dans la picturebox
            NewThread(); 
        }

        public void ThreadTest()
        {
            if(mod == true)
            {
                Application.Run(new Form2(tempfile)); //Si la modification a réussi
            }
            else
            {
                Application.Run(new Form2(tempfile, mod)); //Si elle a raté
            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //En cas de fermeture de la fenêtre par l'utilisateur
        {
            Application.Exit();
            Thread.CurrentThread.Abort();
            
        }

        private void Form2_Load(object sender, EventArgs e) //Opérations s'appliquant à l'ouverture de la fenêtre
        {
            
            label1.Text = this.chemin;
            Image image = Image.FromFile(this.chemin);
            pb_Methode(image, this.chemin);
            
        }

        private void pb_Methode(Image image, string file) //Méthode pour aficher l'image dans la picturebox
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read); //Récupère les droits d'accès au fichier
            //pictureBox1.Image = Image.FromStream(fs);
            PictureBox pb = new PictureBox(); 
            pb.ImageLocation = this.chemin; //Emplacement de l'image
            pb.Image = Image.FromStream(fs);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = pictureBox1.Size;
            pb.Parent = pictureBox1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
            fs.Close(); //Ferme le stream pour relacher les droits d'accès à l'image
            //Controls.Add(pictureBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Inversion Horizontale
        {
            abc.InversionHorizontale();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void button3_Click(object sender, EventArgs e) //Nuances de gris
        {
            abc.NuanceGris();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        

        private void button5_Click(object sender, EventArgs e) //Noir et Blanc
        {
            abc.NoirBlanc();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        

        private void button8_Click(object sender, EventArgs e) //Inversion verticale
        {
            abc.InversionVerticale();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void button9_Click(object sender, EventArgs e) //Inversion des couleurs (R vers G, G vers B, B vers R)
        {
            abc.InvCouleurs();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void button10_Click(object sender, EventArgs e) //Ecrit l'image dans un fichier de sortie
        {
            abc.EcrireImage("mod.bmp");
            string fileName = "mod.bmp";
            FileInfo f = new FileInfo(fileName);
            label7.Text = f.FullName;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) //Modifie le contraste avec un curseur
        {
            abc.Contraste(numericUpDown1.Value);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e) //Suppression d'un type de couleur dans l'image (Mise à 0)
        {
            label5.Text = checkedListBox1.GetItemText(checkedListBox1.SelectedItem);
            abc.SuppressionCouleur(checkedListBox2.GetItemText(checkedListBox2.SelectedItem));
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) //Mets à 255 une des trois couleurs élementaires
        {
            label4.Text = checkedListBox1.GetItemText(checkedListBox1.SelectedItem);
            abc.FiltreCouleur(checkedListBox1.GetItemText(checkedListBox1.SelectedItem));
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile),tempfile);
            NewThread();
        }

        private void NewThread() //Permet d'actualiser la fenêtre après modification de l'image
        {
            
            Application.Exit();
            Thread Menu = new Thread(new ThreadStart(ThreadTest));
            Menu.Start();
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e) //Modifie le rouge de l'image
        {
            abc.ModifierCouleur(2,trackBar1.Value);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void trackBar2_Scroll(object sender, EventArgs e) //Modifie le vert de l'image
        {
            abc.ModifierCouleur(1, trackBar2.Value);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void trackBar3_Scroll(object sender, EventArgs e) //Modifie le bleu de l'image
        {
            abc.ModifierCouleur(0, trackBar3.Value);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button6_Click(object sender, EventArgs e) //Applique un flou à l'image (Matrice de convolution)
        {
            int[,] conv = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            abc.MatConv(conv,true);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button4_Click(object sender, EventArgs e) //Détecte les contours des éléments de l'image (Matrice de convolution)
        {
            int[,] conv = new int[3, 3] { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
            abc.MatConv(conv,false);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button7_Click(object sender, EventArgs e) //Renforcement des bords (Matrice de convolution)
        {
            int[,] conv = new int[3, 3] { { 0,0,0 }, { -1, 1, 0 }, { 0,0,0 } };
            abc.MatConv(conv,false);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button11_Click(object sender, EventArgs e) //Repoussage (Matrice de convolution)
        {
            int[,] conv = new int[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            abc.MatConv(conv, false);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button12_Click(object sender, EventArgs e) //Couleurs négatives
        {
            abc.Negatif2();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button13_Click(object sender, EventArgs e) //Redémarre tout le programme
        {
            Thread Menu;
            Menu = new Thread(new ThreadStart(ThreadLoop));
            Menu.Start();
            Application.Exit();
        }

        public static void ThreadLoop() //Exécute le premier menu
        {

            int compteur = 0;
            //while (Thread.CurrentThread.IsAlive)
            while (compteur == 0)
            {
                Application.Run(new Form1());
                compteur++;


            }

        }

        private void button14_Click(object sender, EventArgs e) //Augmente le contraste de l'image (Matrice de convolution)
        {
            int[,] conv = new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            abc.MatConv(conv, false);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button15_Click(object sender, EventArgs e) //Réduction du bruit de l'image
        {
            int[,] conv = new int[3, 3] { { 1, 1, 1 }, { 1, 4, 1 }, { 1, 1, 1 } };
            abc.MatConv(conv, true);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button16_Click(object sender, EventArgs e) //Détection des bords (Autre matrice de convolution, plus souple)
        {
            int[,] conv = new int[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
            abc.MatConv(conv, false);
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool succes = abc.DessinerRectangle();
            if(succes==false)
            {
                label11.Text = "Image trop petite !";
            }
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Thread Menu;
            Menu = new Thread(new ThreadStart(ThreadLoop2));
            Menu.Start();
            //Application.Exit();
        }

        public void ThreadLoop2()
        {

            int compteur = 0;
            //while (Thread.CurrentThread.IsAlive)
            while (compteur == 0)
            {
                Application.Run(new Histo(abc.PixelsToByteTab(),abc.MATRICE.GetLength(0),abc.MATRICE.GetLength(1)));
                compteur++;


            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            abc.RetrecirImage();
            mod = abc.EcrireImage(tempfile);
            pb_Methode(Image.FromFile(tempfile), tempfile);
            NewThread();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string tab = abc.HdIf.toString();
            label12.Text = tab;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            string tab = abc.FlHd.toString();
            label13.Text = tab;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            bool complet = true;
            int[,] Conv = new int[3, 3];
            try
            {
                Conv[0,0] = Convert.ToInt32(textBox1.Text);
                Conv[0,1] = Convert.ToInt32(textBox2.Text);
                Conv[0,2] = Convert.ToInt32(textBox3.Text);
                Conv[1,0] = Convert.ToInt32(textBox4.Text);
                Conv[1,1] = Convert.ToInt32(textBox5.Text);
                Conv[1,2] = Convert.ToInt32(textBox6.Text);
                Conv[2,0] = Convert.ToInt32(textBox7.Text);
                Conv[2,1] = Convert.ToInt32(textBox8.Text);
                Conv[2,2] = Convert.ToInt32(textBox9.Text);
            }
            catch(System.FormatException)
            {
                complet = false;
                label14.Text = "Erreur dans la matrice.";
            }
            if(complet == true)
            {
                abc.MatConv(Conv, true);
                mod = abc.EcrireImage(tempfile);
                pb_Methode(Image.FromFile(tempfile), tempfile);
                NewThread();
            }
        }
    }
}
