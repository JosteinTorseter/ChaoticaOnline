using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageResize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strPath = textBox1.Text;
            DirectoryInfo dir = Directory.CreateDirectory(strPath + "\\NEW");
            string[] files = Directory.GetFiles(strPath);
            int iSize = Int32.Parse(textBox2.Text);
            foreach (string s in files)
            {
                Image img = Image.FromFile(s);
                Bitmap bmp = Helper.ResizeImage(img, iSize, iSize);
                bmp.Save(dir.FullName + "\\" + System.IO.Path.GetFileNameWithoutExtension(s) + ".png");
            }
        }
    }
}
