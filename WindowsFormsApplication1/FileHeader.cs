using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class FileHeader
    {
        public byte[] headerfile;
        public byte[] signature;
        public byte[] filesize;
        public byte[] reserved;
        public byte[] dataoffset;

        public FileHeader(byte[] headerfile)
        {
            this.headerfile = headerfile;
            this.signature = new byte[2];
            for(int i=0; i<2; i++)
            {
                this.signature[i] = this.headerfile[i];
            }
            this.filesize = new byte[4];
            for(int i=2; i<6; i++)
            {
                this.filesize[i - 2] = this.headerfile[i];
            }
            this.reserved = new byte[4];
            for(int i=6; i<10; i++)
            {
                this.reserved[i - 6] = this.headerfile[i];
            }
            this.dataoffset = new byte[4];
            for(int i=10; i<14; i++)
            {
                this.dataoffset[i - 10] = this.headerfile[i];
            }
        }

        public byte[] Signature
        {
            get { return this.signature; }
            set
            {
                this.signature = value;
            }
        }

        public byte[] Filesize
        {
            get { return this.filesize; }
            set
            {
                this.filesize = value;
            }
        }

        public byte[] Reserved
        {
            get { return this.reserved; }
            set
            {
                this.reserved = value;
            }
        }

        public byte[] Dataoffset
        {
            get { return this.dataoffset; }
            set
            {
                this.dataoffset = value;
            }
        }

        public byte[] ToByteTab()
        {
            for (int i = 0; i < 2; i++)
            {
                this.headerfile[i] = this.signature[i];
            }
            for (int i = 2; i < 6; i++)
            {
                this.headerfile[i] = this.filesize[i - 2];
            }
            for (int i = 6; i < 10; i++)
            {
                this.headerfile[i] = this.reserved[i - 6];
            }
            for (int i = 10; i < 14; i++)
            {
                this.headerfile[i] = this.dataoffset[i - 10];
            }
            return this.headerfile;
        }

        public string toString()
        {
            int sign = BitConverter.ToInt16(this.signature,0);
            int fsize = BitConverter.ToInt32(this.filesize, 0);
            int rsrvd = BitConverter.ToInt32(this.reserved, 0);
            int dtofst = BitConverter.ToInt32(this.dataoffset, 0);
            string all = (sign + " " + fsize + " " + rsrvd + " " + dtofst);
            return all;
        }
    }
}
