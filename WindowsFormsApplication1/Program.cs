using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }



        /*
        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox pb1 = new PictureBox();
            pb1.ImageLocation = "../SamuderaJayaMotor.png";
            pb1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        */

        static void Menu()
        {
            /*
            string chemin;
            int menu = 0;
            Bitmap abc = new Bitmap(chemin);

            string choix = "";
            while (choix != "oui" && choix != "non")
            {
                Console.WriteLine("Voulez vous afficher la matrice? oui ou non");
                choix = Console.ReadLine();
                if (choix == "oui")
                {
                    abc.AfficherMatrice();
                }
            }
            choix = "";

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
            }

            
            while (menu != -1)
            {

                Console.WriteLine("Menu modifications:");
                Console.WriteLine("1. SetPixel");
                Console.WriteLine("2. Inversion horizontale");
                Console.WriteLine("3. Conversion nuances de gris");
                Console.WriteLine("4. Changement contraste (A REVOIR)");
                Console.WriteLine("5. Conversion Noir et Blanc");
                Console.WriteLine("6. Filtre couleur");
                Console.WriteLine("7. Suppression couleur");
                Console.WriteLine("8. Inversion verticale");
                Console.WriteLine("9. Couleurs négatives");
                Console.WriteLine("-1. Etape suivante");
                menu = SaisieEntier();

                if (menu == 1)
                {
                    while (choix != "oui" && choix != "non")
                    {
                        Console.WriteLine("SOuhaitez vous essayer la methode SetPixel? oui ou non");
                        choix = Console.ReadLine();
                        if (choix == "oui")
                        {
                            //Test méthode SetPixel
                            abc.SetPixel(1, 3, 255, 0, 255);
                            Console.WriteLine("Afficher matrice?");
                            choix = Console.ReadLine();
                            if (choix == "oui")
                            {
                                abc.AfficherMatrice();
                            }
                            Console.WriteLine("Valeurs du pixel modifié:");
                            byte[] tab = abc.GetPixel(1, 3);
                            Console.Write(tab[0]);
                            Console.Write(" ");
                            Console.Write(tab[1]);
                            Console.Write(" ");
                            Console.Write(tab[2]);
                            Console.WriteLine();
                        }
                    }
                }
                else if (menu == 2)
                {
                    abc.InversionHorizontale();
                }
                else if (menu == 3)
                {
                    abc.NuanceGris();
                }
                else if (menu == 4)
                {
                    abc.Contraste();
                }
                else if (menu == 5)
                {
                    abc.NoirBlanc();
                }
                else if (menu == 6)
                {
                    abc.FiltreCouleur();
                }
                else if (menu == 7)
                {
                    abc.SuppressionCouleur();
                }
                else if (menu == 8)
                {
                    abc.InversionVerticale();
                }
                else if (menu == 9)
                {
                    abc.Negatif();
                }
                while (choix != "oui" && choix != "non")
                {
                    Console.WriteLine("Ecrire image dans un fichier? oui ou non");
                    choix = Console.ReadLine();
                    if (choix == "oui")
                    {
                        Console.WriteLine("Emplacement du fichier: ");
                        Console.WriteLine(abc.EcrireImage());


                    }
                }
                choix = "";
            }


            Console.ReadKey();
            */
        }
        static int SaisieEntier()
        {
            int saisie = 0;
            bool invalide = true;
            while (invalide)
            {
                invalide = false;
                try
                {
                    saisie = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    invalide = true;
                    Console.WriteLine("Veuillez saisir uniquement un nombre entier");
                }
            }
            return saisie;
        }

        static byte[] FichierImage(string cheminacces)
        {
            byte[] image = File.ReadAllBytes(cheminacces);
            Console.WriteLine("BITMAPFILEHEADER");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(image[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("BITMAPINFOHEADER");
            for (int i = 14; i < 54; i++)
            {
                Console.Write(image[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("Image");
            for (int i = 54; i < image.Length; i++)
            {
                Console.Write(image[i]);
                Console.Write(" ");
            }
            return image;
        }
    }
}
