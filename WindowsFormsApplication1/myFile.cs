using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    class myFile
    {
        //Indication: System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", text);
        byte[] BITMAPFILEHEADER;
        byte[] BITMAPINFOHEADER;
        byte[] BITMAPIMAGE;
        byte[,,] MATIMAGE;
        int lignes;
        int colonnes;
        byte[] FILE;
        byte[] fichier;
        public HeaderInfo HdIf;
        public FileHeader FlHd;

        public myFile(string chemin)
        {
            byte[] tablignes = new byte[4];
            byte[] tabcolonnes = new byte[4];
            this.BITMAPFILEHEADER = new byte[14];
            this.BITMAPINFOHEADER = new byte[40];
            this.FILE = File.ReadAllBytes(chemin);
            fichier = new byte[FILE.Length];
            this.BITMAPIMAGE = new byte[this.FILE.Length - 54];
            for (int i = 0; i < 14; i++)
            {
                this.BITMAPFILEHEADER[i] = this.FILE[i];
            }
            for (int i = 14; i < 54; i++)
            {
                this.BITMAPINFOHEADER[i - 14] = this.FILE[i];
            }
            for (int i = 54; i < FILE.Length; i++)
            {
                this.BITMAPIMAGE[i - 54] = this.FILE[i];
            }

            HdIf = new HeaderInfo(this.BITMAPINFOHEADER);
            FlHd = new FileHeader(this.BITMAPFILEHEADER);

            //FichierImage(BITMAPFILEHEADER, BITMAPINFOHEADER, BITMAPIMAGE);
            //Console.WriteLine("lignes");
            for (int i = 4; i < 8; i++) //Range la taille de colonnes de l'image dans un tableau
            {
                tabcolonnes[i - 4] = this.BITMAPINFOHEADER[i];
                //Console.WriteLine(this.BITMAPINFOHEADER[i]);
            }
            //Array.Reverse(tabcolonnes); //Inverse un tableau
            //Console.WriteLine("Colonnes");
            for (int i = 8; i < 12; i++) //Range la taille de lignes de l'image dans un tableau
            {
                tablignes[i - 8] = this.BITMAPINFOHEADER[i];
                //Console.WriteLine(this.BITMAPINFOHEADER[i]);
            }
            //Array.Reverse(tablignes);
            this.colonnes = BitConverter.ToInt32(tabcolonnes, 0); //Convertit le tableau de bytes en un entier.
            this.lignes = BitConverter.ToInt32(tablignes, 0);
            //Console.WriteLine(" ");
            //Console.WriteLine(this.colonnes);
            //Console.WriteLine(this.lignes);
            this.MATIMAGE = new byte[this.lignes, this.colonnes, 3];
            //int ligne = 0;
            //int colonne = 0;

            int j = 0;
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        this.MATIMAGE[ligne, colonne, i] = this.BITMAPIMAGE[j];
                        j++;
                    }
                }
            }
        }

        public void SetPixel(int ligne, int colonne, byte r, byte g, byte b)
        {
            this.MATIMAGE[ligne, colonne, 0] = r;
            this.MATIMAGE[ligne, colonne, 1] = g;
            this.MATIMAGE[ligne, colonne, 2] = b;
        }

        public byte[] GetPixel(int ligne, int colonne)
        {
            byte[] tab = new byte[3];
            tab[0] = this.MATIMAGE[ligne, colonne, 0];
            tab[1] = this.MATIMAGE[ligne, colonne, 1];
            tab[2] = this.MATIMAGE[ligne, colonne, 2];
            return tab;
        }

        public void AfficherMatrice()
        {
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write(this.MATIMAGE[ligne, colonne, i]);
                        Console.Write(" ");
                    }
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        public void MatConv(int[,] conv, bool flou)
        {
            byte[,,] tempmat = new byte[this.MATIMAGE.GetLength(0), this.MATIMAGE.GetLength(1), 3];
            for(int ligne=1; ligne<tempmat.GetLength(0)-1; ligne++)
            {
                for(int colonne = 1; colonne<tempmat.GetLength(1)-1; colonne++)
                {
                    for(int couleur=0; couleur<3; couleur++)
                    {
                        int valeur = conv[0, 0] * Convert.ToInt32(this.MATIMAGE[ligne - 1, colonne - 1, couleur]) + conv[0, 1] * Convert.ToInt32(this.MATIMAGE[ligne - 1, colonne, couleur]) + conv[0, 2] * Convert.ToInt32(this.MATIMAGE[ligne - 1, colonne + 1, couleur]) + conv[1, 0] * Convert.ToInt32(this.MATIMAGE[ligne, colonne - 1, couleur]) + conv[1, 1] * Convert.ToInt32(this.MATIMAGE[ligne, colonne, couleur]) + conv[1, 2] * Convert.ToInt32(this.MATIMAGE[ligne, colonne + 1, couleur]) + conv[2, 0] * Convert.ToInt32(this.MATIMAGE[ligne + 1, colonne - 1, couleur]) + conv[2, 1] * Convert.ToInt32(this.MATIMAGE[ligne + 1, colonne, couleur]) + conv[2, 2] * Convert.ToInt32(this.MATIMAGE[ligne + 1, colonne + 1, couleur]);
                        if(flou)
                        {
                            valeur = valeur / 9;
                        }
                        byte conversion;
                        if (valeur<0)
                        {
                            conversion = 0;
                        }
                        else if(valeur>255)
                        {
                            conversion = 255;
                        }
                        else
                        {
                            conversion = Convert.ToByte(valeur);
                        }
                        tempmat[ligne, colonne, couleur] = conversion;
                    }

                }
            }
            
            for (int ligne = 0; ligne < tempmat.GetLength(0); ligne++)
            {
                for(int couleur =0; couleur<3; couleur++)
                {
                    tempmat[ligne, 0, couleur] = tempmat[ligne, 1, couleur];
                }
            }
            for (int colonne = 0; colonne < tempmat.GetLength(1); colonne++)
            {
                for (int couleur = 0; couleur < 3; couleur++)
                {
                    tempmat[0, colonne, couleur] = tempmat[1, colonne, couleur];
                }
            }
            for (int ligne = 0; ligne < tempmat.GetLength(0); ligne++)
            {
                for (int couleur = 0; couleur < 3; couleur++)
                {
                    tempmat[ligne, tempmat.GetLength(1)-1, couleur] = tempmat[ligne, tempmat.GetLength(1)-2, couleur];
                }
            }
            for (int colonne = 0; colonne < tempmat.GetLength(1); colonne++)
            {
                for (int couleur = 0; couleur < 3; couleur++)
                {
                    tempmat[tempmat.GetLength(0)-1, colonne, couleur] = tempmat[tempmat.GetLength(0)-2, colonne, couleur];
                }
            }
            
            this.MATIMAGE = tempmat;
        }

        public bool EcrireImage(string adresse)
        {
            
            for (int i = 0; i < 14; i++)
            {
                fichier[i] = BITMAPFILEHEADER[i];
            }
            for (int i = 14; i < 54; i++)
            {
                fichier[i] = BITMAPINFOHEADER[i - 14];
            }
            
                int j = 54;
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            fichier[j] = this.MATIMAGE[ligne, colonne, i];
                            j++;
                        }
                    }
                }
            

            try
            {
                File.WriteAllBytes(adresse, fichier);
            }
            catch(System.IO.IOException)
            {
                return false;
            }

            return true;
            //string dossier = ""; //System.IO.Directory.GetCurrentDirectory();
            //return dossier;
        }

        public void InversionHorizontale()
        {
            byte[] fichier = new byte[this.lignes * this.colonnes * 3];
            int j = 0;
            for (int ligne = this.lignes - 1; ligne >= 0; ligne--)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        fichier[j] = this.MATIMAGE[ligne, colonne, i];
                        j++;
                    }
                }
            }
            j = 0;
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        this.MATIMAGE[ligne, colonne, i] = fichier[j];
                        j++;
                    }
                }
            }

        }

        public void ModifierCouleur(int couleur, int value)
        {
            byte value2 = Convert.ToByte(Math.Abs(value));
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    if(value<0)
                    {
                        if (Convert.ToInt32(this.MATIMAGE[ligne, colonne, couleur]) <= value2)
                        {
                            this.MATIMAGE[ligne, colonne, couleur] = 0;
                        }
                        else
                        {
                            this.MATIMAGE[ligne, colonne, couleur] -= value2;
                        }
                        
                    }
                    else
                    {

                        if (Convert.ToInt32(this.MATIMAGE[ligne, colonne, couleur]) >= 255 - value2)
                        {
                            this.MATIMAGE[ligne, colonne, couleur] = 255;
                        }
                        else
                        {
                            this.MATIMAGE[ligne, colonne, couleur] += value2;
                        }
                    }
                    
                }
            }
        }

        public void InversionVerticale()
        {
            byte[] fichier = new byte[this.lignes * this.colonnes * 3];
            int j = 0;
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = this.colonnes - 1; colonne >= 0; colonne--)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        fichier[j] = this.MATIMAGE[ligne, colonne, i];
                        j++;
                    }
                }
            }
            j = 0;
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        this.MATIMAGE[ligne, colonne, i] = fichier[j];
                        j++;
                    }
                }
            }

        }

        public void NuanceGris()
        {
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    int val1 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 0]);
                    int val2 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 1]);
                    int val3 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 2]);
                    int moyenne = (val1 + val2 + val3) / 3;
                    byte moyennebyte = Convert.ToByte(moyenne);
                    this.MATIMAGE[ligne, colonne, 0] = moyennebyte;
                    this.MATIMAGE[ligne, colonne, 1] = moyennebyte;
                    this.MATIMAGE[ligne, colonne, 2] = moyennebyte;
                }
            }
        }

        public void NoirBlanc()
        {
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    int val1 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 0]);
                    int val2 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 1]);
                    int val3 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 2]);
                    int moyenne = (val1 + val2 + val3) / 3;
                    byte moyennebyte = Convert.ToByte(moyenne);
                    if (moyennebyte < 255 / 2)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 0;
                        this.MATIMAGE[ligne, colonne, 1] = 0;
                        this.MATIMAGE[ligne, colonne, 2] = 0;
                    }
                    else
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }

                }
            }
        }

        public void InvCouleurs()
        {
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    
                    byte g = this.MATIMAGE[ligne, colonne, 1];
                    byte r = this.MATIMAGE[ligne, colonne, 2];
                    byte b = this.MATIMAGE[ligne, colonne, 0];
                    this.MATIMAGE[ligne, colonne, 0] = g;
                    this.MATIMAGE[ligne, colonne, 1] = r;
                    this.MATIMAGE[ligne, colonne, 2] = b;
                    
                }
            }
        }

        public void Negatif2()
        {
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    
                    int r = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 0]);
                    int g = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 1]);
                    int b = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 2]);
                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;
                    this.MATIMAGE[ligne, colonne, 0] = Convert.ToByte(r);
                    this.MATIMAGE[ligne, colonne, 1] = Convert.ToByte(g);
                    this.MATIMAGE[ligne, colonne, 2] = Convert.ToByte(b);

                }
            }
        }

        public void FiltreCouleur(string couleur)
        {
            
            if (couleur == "rouge")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
            }
            if (couleur == "vert")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                    }
                }
            }
            if (couleur == "bleu")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                    }
                }
            }
        }

        public void SuppressionCouleur(string couleur)
        {
            
            if (couleur == "rouge")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 2] = 0;
                    }
                }
            }
            if (couleur == "vert")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 1] = 0;
                    }
                }
            }
            if (couleur == "bleu")
            {
                for (int ligne = 0; ligne < this.lignes; ligne++)
                {
                    for (int colonne = 0; colonne < this.colonnes; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 0;
                    }
                }
            }
        }

        public void Contraste(decimal dec)
        {
            int percent = Convert.ToInt32(dec);
            double contraste = percent * 2.55 / 2;
            for (int ligne = 0; ligne < this.lignes; ligne++)
            {
                for (int colonne = 0; colonne < this.colonnes; colonne++)
                {
                    int val1 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 0]);
                    int val2 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 1]);
                    int val3 = Convert.ToInt32(this.MATIMAGE[ligne, colonne, 2]);
                    int moyenne = (val1 + val2 + val3) / 3;
                    if (moyenne < contraste)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 0;
                        this.MATIMAGE[ligne, colonne, 1] = 0;
                        this.MATIMAGE[ligne, colonne, 2] = 0;
                    }
                    if (moyenne > (255 - contraste))
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
            }
        }

        public bool DessinerRectangle()
        {
            int taillebordure = 5;
            if(lignes>12&&colonnes>12)
            {
                for(int ligne=0; ligne<taillebordure; ligne++)
                {
                    for(int colonne=0; colonne<colonnes-1; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
                for (int colonne = 0; colonne < taillebordure; colonne++)
                {
                    for (int ligne = 0; ligne < lignes - 1; ligne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
                for(int ligne=lignes-1; ligne>lignes-1-taillebordure; ligne--)
                {
                    for (int colonne = 0; colonne < colonnes - 1; colonne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
                for (int colonne = colonnes - 1; colonne > colonnes-1-taillebordure; colonne--)
                {
                    for (int ligne = 0; ligne < lignes - 1; ligne++)
                    {
                        this.MATIMAGE[ligne, colonne, 0] = 255;
                        this.MATIMAGE[ligne, colonne, 1] = 255;
                        this.MATIMAGE[ligne, colonne, 2] = 255;
                    }
                }
                return true;
            }
            return false;
        }

        public byte[] bitmapimage
        {
            get { return this.BITMAPIMAGE; }
            set { this.BITMAPIMAGE = value; }

        }

        public string InfoHeaderToString()
        {
            string head;
            head = System.Text.Encoding.Unicode.GetString(this.bitmapinfoheader);
            return head;
        }

        public byte[] bitmapfileheader
        {
            get { return this.BITMAPFILEHEADER; }
            set { this.BITMAPFILEHEADER = value; }
        }

        public byte[] bitmapinfoheader
        {
            get { return this.BITMAPINFOHEADER; }
            set { this.BITMAPINFOHEADER = value; }
        }

        static void FichierImage(byte[] fileheader, byte[] infoheader, byte[] image)
        {
            Console.WriteLine("BITMAPFILEHEADER");
            for (int i = 0; i < 14; i++)
            {
                Console.Write(fileheader[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("BITMAPINFOHEADER");
            for (int i = 0; i < 40; i++)
            {
                Console.Write(infoheader[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("Image");
            for (int i = 0; i < image.Length; i++)
            {
                Console.Write(image[i]);
                Console.Write(" ");
            }
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

        public byte[] PixelsToByteTab()
        {
            byte[] ImageActu = new byte[this.BITMAPIMAGE.Length];
            int j = 0;
            for (int ligne = 0; ligne < this.MATIMAGE.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < this.MATIMAGE.GetLength(1); colonne++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ImageActu[j] = this.MATIMAGE[ligne, colonne, i];
                        j++;
                    }
                }
            }
            return ImageActu;
        }

        public byte[,,] MATRICE
        {
            get { return this.MATIMAGE; }
        }

        public void RetrecirImage() //Fonctionne pas
        {
            if(this.MATIMAGE.GetLength(0)>3 && this.MATIMAGE.GetLength(1)>3)
            {
                int height = this.MATIMAGE.GetLength(0) / 3;
                int width = this.MATIMAGE.GetLength(1) / 3;
                byte[,,] NewMat = new byte[height, width, 3];
                int l = 0;
                int c = 0;
                for(int ligne = 1; ligne<this.MATIMAGE.GetLength(0)-3; ligne+=3)
                {
                    for(int colonne = 1; colonne<this.MATIMAGE.GetLength(1)-3; colonne+=3)
                    {
                        for(int color = 0; color<3; color++)
                        {
                            NewMat[l, c, color] = this.MATIMAGE[ligne, colonne, color];
                            colonne++;
                        }
                    }
                    ligne++;
                }
                this.MATIMAGE = NewMat;
                byte[] bfSize = BitConverter.GetBytes(54 + (NewMat.GetLength(0) * NewMat.GetLength(1)));

                byte[] biWidth = BitConverter.GetBytes(NewMat.GetLength(0));
                byte[] biHeight = BitConverter.GetBytes(NewMat.GetLength(1));
                this.lignes = BitConverter.ToInt32(biHeight, 0);
                this.colonnes = BitConverter.ToInt32(biWidth, 0);
                fichier = new byte[54 + (this.lignes * this.colonnes * 3)];
                for(int i=2; i<6; i++)
                {
                    this.BITMAPFILEHEADER[i] = bfSize[i - 2];
                }
                for(int i=4; i<8; i++)
                {
                    this.BITMAPINFOHEADER[i] = biWidth[i - 4];
                }
                for(int i=8; i<12; i++)
                {
                    this.BITMAPINFOHEADER[i] = biHeight[i - 8];
                }
            }
        }
    }
}
