using Microsoft.Office.Interop.Excel;
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
using AForge.Imaging.Filters;

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

        private void button2_Click(object sender, EventArgs e)
        {
            string strPath = textBox1.Text;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Open(strPath + "\\Data.xlsx");
            Worksheet ws = wb.Worksheets["WorldItems"];
            int i = 2;
            string strImage = "";
            int iRarity = 0;
            string color = "";
            bool isUnique = false;
            string sNext = ws.Cells[i, 1].Formula;
            while (!String.IsNullOrEmpty(sNext))
            {
                strImage = ws.Cells[i, 12].Formula;
                //string s = ws.Cells[i, 10].Value.toString();
                iRarity = Int32.Parse(ws.Cells[i, 10].Formula);
                int iBool = Int32.Parse(ws.Cells[i, 7].Formula);
                isUnique = Convert.ToBoolean(iBool);
                color = RarityColor(iRarity, isUnique);
                Bitmap bmp = new Bitmap(strPath + "\\items\\" + strImage);
                FloodFillWithAForge(ref bmp, 0, 0, ColorTranslator.FromHtml(color));
                bmp.Save(strPath + "\\done\\" + strImage.Replace(".jpg", ".png"));
                i++;
                sNext = ws.Cells[i, 1].Formula;
            }
        }

        public void FloodFillWithAForge(ref Bitmap bmp, int x, int y, Color fillColor)
        {
            PointedColorFloodFill filter = new PointedColorFloodFill();
            filter.Tolerance = Color.FromArgb(1, 1, 1);
            filter.StartingPoint = new AForge.IntPoint(x, y);
            filter.FillColor = fillColor;
            filter.ApplyInPlace(bmp);
        }

        public string RarityColor(int rarity, bool isUnique)
        {
            if (isUnique) { return "#FFBB00"; }
            if (rarity > 79)
            {
                return "#595959";
            }
            else if (rarity > 59)
            {
                return "#86AC41";
            }
            else if (rarity > 39)
            {
                return "#86AC41";
            }
            else if (rarity > 19)
            {
                return "#68829E";
            }
            else if (rarity > 9)
            {
                return "#375E97";
            }
            else
            {
                return "#800080";
            }
        }

    }
}
