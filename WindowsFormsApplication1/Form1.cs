using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    

    public partial class Form1 : Form
    {

        public static string chemin = "";
        
        
        public Form1()
        {
            
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            chemin = "Test001.bmp"; //Définit le chemin du fichier
            Thread Menu;
            Menu = new Thread(new ThreadStart(ThreadLoop));
            Menu.Start();
            Application.Exit();

            
        }

        public static void ThreadLoop()
        {
            
            int compteur = 0;
            //while (Thread.CurrentThread.IsAlive)
            while (compteur==0)
            {
                    Application.Run(new Form2(chemin)); //Lance le processus
                    compteur++;
                
                
            }
            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            chemin = "lac_en_montagne.bmp";
            Thread Menu;
            Menu = new Thread(new ThreadStart(ThreadLoop));
            Menu.Start();
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            openFileDialog1 = new OpenFileDialog(); //Méthode permettant d'ouvrir une fenetre de sélection de fichier.

            openFileDialog1.InitialDirectory = "c:\\"; //Répertoire de recherche initiale du fichier
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp"; //Type de fichier à afficher
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) //Une fois que le fichier est sélectionné
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                label2.Text = openFileDialog1.FileName; //Ecrit l'adresse du fichier dans une zone de texte
                                chemin = openFileDialog1.FileName; //Donne l'adresse du fichier à la variable chemin
                                openFileDialog1.Dispose();
                                Thread Menu;
                                Menu = new Thread(new ThreadStart(ThreadLoop)); //Lance un nouveau processus pour ouvrir une nouvelle fenêtre
                                Menu.Start(); //Ouvre cette nouvelle fenêtre
                                Application.Exit(); //Quitte la fenêtre actuelle
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
            catch(System.Threading.ThreadStateException)
            {
                label1.Text = "Impossible après redémarrage, faire le choix 1 ou 2.";
            }
            

        }
    }
}
