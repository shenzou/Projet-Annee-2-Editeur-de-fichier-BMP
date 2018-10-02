using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class HeaderInfo
    {
        public byte[] infoheader;
        private byte[] size;
        private byte[] width;
        private byte[] height;
        private byte[] planes;
        private byte[] bitcount;
        private byte[] compression;
        private byte[] imagesize;
        private byte[] xpixelsperm;
        private byte[] ypixelsperm;
        private byte[] colorsused;
        private byte[] colorsimportant;

        public byte[] Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public byte[] Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public byte[] Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public byte[] Planes
        {
            get
            {
                return planes;
            }

            set
            {
                planes = value;
            }
        }

        public byte[] Bitcount
        {
            get
            {
                return bitcount;
            }

            set
            {
                bitcount = value;
            }
        }

        public byte[] Compression
        {
            get
            {
                return compression;
            }

            set
            {
                compression = value;
            }
        }

        public byte[] Imagesize
        {
            get
            {
                return imagesize;
            }

            set
            {
                imagesize = value;
            }
        }

        public byte[] Xpixelsperm
        {
            get
            {
                return xpixelsperm;
            }

            set
            {
                xpixelsperm = value;
            }
        }

        public byte[] Ypixelsperm
        {
            get
            {
                return ypixelsperm;
            }

            set
            {
                ypixelsperm = value;
            }
        }

        public byte[] Colorsused
        {
            get
            {
                return colorsused;
            }

            set
            {
                colorsused = value;
            }
        }

        public byte[] Colorsimportant
        {
            get
            {
                return colorsimportant;
            }

            set
            {
                colorsimportant = value;
            }
        }

        public HeaderInfo(byte[] headerinfo)
        {
            this.infoheader = headerinfo;
            this.size = new byte[4];
            for(int i=0; i<4; i++)
            {
                this.size[i] = this.infoheader[i];
            }
            this.width = new byte[4];
            for(int i=4; i<8; i++)
            {
                this.width[i - 4] = this.infoheader[i];
            }
            this.height = new byte[4];
            for(int i=8; i<12; i++)
            {
                this.height[i - 8] = this.infoheader[i];
            }
            this.planes = new byte[2];
            for(int i=12; i<14; i++)
            {
                this.planes[i - 12] = this.infoheader[i];
            }
            this.bitcount = new byte[2];
            for(int i=14; i<16; i++)
            {
                this.bitcount[i - 14] = this.infoheader[i];
            }
            this.compression = new byte[4];
            for(int i=16; i<20; i++)
            {
                this.compression[i - 16] = this.infoheader[i];
            }
            this.imagesize = new byte[4];
            for(int i = 20; i<24; i++)
            {
                this.imagesize[i - 20] = this.infoheader[i];
            }
            this.xpixelsperm = new byte[4];
            for(int i=24; i<28; i++)
            {
                this.xpixelsperm[i - 24] = this.infoheader[i];
            }
            this.ypixelsperm = new byte[4];
            for(int i=28; i<32; i++)
            {
                this.ypixelsperm[i - 28] = this.infoheader[i];
            }
            this.colorsused = new byte[4];
            for(int i=32; i<36; i++)
            {
                this.colorsused[i - 32] = this.infoheader[i];
            }
            this.colorsimportant = new byte[4];
            for(int i = 36; i<40; i++)
            {
                this.colorsimportant[i - 36] = this.infoheader[i];
            }
        }

        public string toString()
        {
            int siz = BitConverter.ToInt32(this.size, 0);
            int wi = BitConverter.ToInt32(this.width, 0);
            int he = BitConverter.ToInt32(this.height, 0);
            int pla = BitConverter.ToInt16(this.planes, 0);
            int btc = BitConverter.ToInt16(this.bitcount, 0);
            int cpr = BitConverter.ToInt32(this.compression, 0);
            int imgsiz = BitConverter.ToInt32(this.imagesize, 0);
            int x = BitConverter.ToInt32(this.xpixelsperm, 0);
            int y = BitConverter.ToInt32(this.ypixelsperm, 0);
            int clrused = BitConverter.ToInt32(this.colorsused, 0);
            int clrimp = BitConverter.ToInt32(this.colorsimportant, 0);
            string all = (siz + " " + wi + " " + he + " " + pla + " " + btc + " " + cpr + " " + imgsiz + " " + x + " " + y + " " + clrused + " " + clrimp);
            return all;
        }

        public byte[] ToByteTab()
        {
            for (int i = 0; i < 4; i++)
            {
                this.infoheader[i] = this.size[i];
            }
            for (int i = 4; i < 8; i++)
            {
                this.infoheader[i] = this.width[i - 4];
            }
            for (int i = 8; i < 12; i++)
            {
                this.infoheader[i] = this.height[i - 8];
            }
            for (int i = 12; i < 14; i++)
            {
                this.infoheader[i] = this.planes[i - 12];
            }
            for (int i = 14; i < 16; i++)
            {
                this.infoheader[i] = this.bitcount[i - 14];
            }
            for (int i = 16; i < 20; i++)
            {
                this.infoheader[i] = this.compression[i - 16];
            }
            for (int i = 20; i < 24; i++)
            {
                this.infoheader[i] = this.imagesize[i - 20];
            }
            for (int i = 24; i < 28; i++)
            {
                this.infoheader[i] = this.xpixelsperm[i - 24];
            }
            for (int i = 28; i < 32; i++)
            {
                this.infoheader[i] = this.ypixelsperm[i - 28];
            }
            for (int i = 32; i < 36; i++)
            {
                this.infoheader[i] = this.colorsused[i - 32];
            }
            for (int i = 36; i < 40; i++)
            {
                this.infoheader[i] = this.colorsimportant[i - 36];
            }
            return this.infoheader;
        }
    }
}
