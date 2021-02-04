using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;

namespace CG_Paint_Project
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x, y, h, w, h1, w1;
        int nx, ny;
        bool moving = false; // this variable true when mouse down is occure
        Pen pn;
        SolidBrush sd;       

       

        Image file;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { pn = new Pen(Color.Black, (float)pievalue.Value); }
        public Form1()
        {
            InitializeComponent();
            g = picdraw.CreateGraphics();

            // لل بكتشربوكس _( pic_click )
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //
            pn = new Pen(Color.Black, (float)pievalue.Value);
            sd = new SolidBrush(Color.Black);
            // لل بكتشربوكس _( pic_click )
            pn.StartCap = pn.EndCap = System.Drawing.Drawing2D.LineCap.Round; //
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //textBox1.Visible = false;
        }
       
        private void pic_click(object sender, EventArgs e)
        {
            //عشان الالوان معموله ف بكتشر بوكس 
            PictureBox pic = (PictureBox)sender;
            pn.Color = pic.BackColor;
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pn.Color = colorDialog1.Color;
                sd.Color = colorDialog1.Color;
            }

        }
        // get starting point
        private void picdraw_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
            nx = x;
            ny = y;
            picdraw.Cursor = Cursors.Cross;
        }

        // get current point
        private void picdraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving && x != -1 && y != -1)
            {

                g.DrawLine(pn, new Point(x, y), e.Location);
                if (moving && x != -1 && y != -1 && (rectangle.Checked || line.Checked || ellipse.Checked || triangle.Checked))
                {
                    Pen newp = new Pen(pictureBox13.BackColor, 1);
                    g.DrawLine(newp, new Point(x, y), e.Location);
                }
                
                else if (moving && x != -1 && y != -1 && ppen.Checked)
                {
                    g.DrawLine(pn, new Point(x, y), e.Location);
                }

                x = e.X;
                y = e.Y;  
            }
            


            h1 = e.X - x;
            w1 = e.Y - y;
            Rectangle rec = new Rectangle(x, y, h1, w1);
            if (prectangle.Checked)
            {
                g.DrawRectangle(pn, rec);
            }
            else if (pline.Checked)
            {
                g.DrawLine(pn, x, y, -1, -1);
            }
            else if (pellipse.Checked)
            {
                g.DrawEllipse(pn, rec);
            }

        }

        

        // get ending point
        private void picdraw_MouseUp(object sender, MouseEventArgs e)
        {
            h = e.X;
            y = e.Y;

            x = -1;
            y = -1;

            h = e.X - nx;
            w = e.Y - ny;
            

            
            Rectangle shape = new Rectangle(nx, ny, h, w);
            if (rectangle.Checked)
            {
                g.RotateTransform((float)numrotate.Value);
                g.DrawRectangle(pn, shape);
                g.RotateTransform(-(float)numrotate.Value);
                
            }
            else if (line.Checked)
            {
                g.RotateTransform((float)numrotate.Value);
                g.DrawLine(pn, new Point(nx,ny), e.Location);
                g.RotateTransform(-(float)numrotate.Value);
            }
            else if (ellipse.Checked)
            {
                g.RotateTransform((float)numrotate.Value);
                g.DrawEllipse(pn, shape);
                g.RotateTransform(-(float)numrotate.Value);
            }
            else if(triangle.Checked)
            {

            }

            

            moving = false;
            x = -1;
            y = -1;
            
            picdraw.Cursor = Cursors.Default;
        }
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlhome.Visible = true;
        }
        private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlhome.Visible = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult newresult = MessageBox.Show("Do you want to save changes ?","Paint",MessageBoxButtons.OKCancel);
            if (newresult == DialogResult.Cancel)
            {
                picdraw.CreateGraphics().Clear(picdraw.BackColor);
            }
            else if (newresult == DialogResult.OK)
            {
                if (picdraw.Image != null)
                {
                    saveFileDialog1.Filter = "JPG (*.JPG) |*.JPG; *.PNG; *.GIF;*.ICO;*.TIF;*.TIFF;*.JPEG;*.JPE;*.JFIF;*.BMP;*.DIB;";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (saveFileDialog1.FileName.EndsWith(".jpg"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".png"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".gif"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".ico"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Icon);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".tif"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".tiff"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".jpeg"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".jpe"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        }
                        else if (saveFileDialog1.FileName.EndsWith(".bmp"))
                        {
                            picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                        }
                    }
                }
                else
                {
                    saveFileDialog1.Filter = "JPG (*.JPG) |*.JPG; *.PNG; *.GIF;*.ICO;*.TIF;*.TIFF;*.JPEG;*.JPE;*.JFIF;*.BMP;*.DIB;";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        int width = Convert.ToInt32(picdraw.ClientRectangle.Width);
                        int height = Convert.ToInt32(picdraw.ClientRectangle.Height);
                        Bitmap bmp = new Bitmap(width, height);
                        picdraw.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                        bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                }
            }
            
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All picture files |*.JPG; *.PNG; *.GIF;*.ICO;*.TIF;*.TIFF;*.JPEG;*.JPE;*.JFIF;*.BMP;*.DIB;";
            openFileDialog1.InitialDirectory = " C:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picdraw.Image = Image.FromFile(openFileDialog1.FileName);
                picdraw.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picdraw.Image != null)
            {
                saveFileDialog1.Filter = "JPG (*.JPG) |*.JPG; *.PNG; *.GIF;*.ICO;*.TIF;*.TIFF;*.JPEG;*.JPE;*.JFIF;*.BMP;*.DIB;";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if(saveFileDialog1.FileName.EndsWith(".jpg"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".png"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".gif"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".ico"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Icon);
                    }
                    else if (saveFileDialog1.FileName.EndsWith(".tif"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".tiff"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".jpeg"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    else if (saveFileDialog1.FileName.EndsWith(".jpe"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    else if(saveFileDialog1.FileName.EndsWith(".bmp"))
                    {
                        picdraw.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    }  
                } 
            }
            else
            {
                saveFileDialog1.Filter = "JPG (*.JPG) |*.JPG; *.PNG; *.GIF;*.ICO;*.TIF;*.TIFF;*.JPEG;*.JPE;*.JFIF;*.BMP;*.DIB;";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int width = Convert.ToInt32(picdraw.ClientRectangle.Width);
                    int height = Convert.ToInt32(picdraw.ClientRectangle.Height);
                    Bitmap bmp = new Bitmap(width, height);
                    picdraw.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                    bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
                }
            }
        }

        private void printtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (picdraw.Image != null)
            {
                Bitmap prbit = new Bitmap(picdraw.Width, picdraw.Height);
                picdraw.DrawToBitmap(prbit, new Rectangle(0, 0, picdraw.Width, picdraw.Height));
                g.DrawImage(prbit, 0, 0);
                prbit.Dispose();
            }
        }
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Show();
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void aboutApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Paint application :
paint used for drawing . Created by / Hala Mohammed under supervision En/ Abeer Saber ", "Paint Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void helpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"For any question 
Go to : www.HM.com", "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void exitToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox34_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picdraw.BackColor = colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picdraw.CreateGraphics().Clear(picdraw.BackColor);

           
        }

        private void line_click(object sender, EventArgs e)
        {

        }

        private void Ellipse_click(object sender, EventArgs e)
        {

        }

        private void Triangle_click(object sender, EventArgs e)
        {
            
        }

        private void Rectangle_click(object sender, EventArgs e)
        {
            
            
        }
        //private Rectangle GetRect()
        //{
        //    rect = new Rectangle();
        //    rect.X = Math.Min(locXY.X, locX1Y1.X);
        //    rect.Y = Math.Min(locXY.Y, locX1Y1.Y);
        //    rect.Width = Math.Abs(locXY.X - locX1Y1.X); // absolute value
        //    rect.Height = Math.Abs(locXY.Y - locX1Y1.Y);
        //    return rect;
        //}

        private void Star_click(object sender, EventArgs e)
        {

        }

        private void Heart_click(object sender, EventArgs e)
        {

        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pn = new Pen(Color.Black, (float)pievalue.Value);
        }

        private void homeToolStripMenuItem1_Click(object sender, EventArgs e) { }
        private void homeToolStripMenuItem_DoubleClick(object sender, EventArgs e) { }
        private void HomeToolStripMenuItem_DoubleClick(object sender, EventArgs e) { }
        private void homeToolStripMenuItem_CheckedChanged(object sender, EventArgs e) { }
        private void pnldraw_Paint(object sender, PaintEventArgs e)
        {
        //    if (rect != null)
        //    {
        //        e.Graphics.DrawRectangle(pn, GetRect());

        //    }
        }
        private void pnlhome_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox40_Click(object sender, EventArgs e) { }
        private void pictureBox7_Click(object sender, EventArgs e) { }

        private void pictureBox37_Click(object sender, EventArgs e) { }
        private void pictureBox36_Click(object sender, EventArgs e)
        {


        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pline_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ptriangle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pellipse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void prectangle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rectangle_CheckedChanged(object sender, EventArgs e)
        {

        }

        

        

        

        
        

        

        

        

        
        

        

       
        
    }
}
