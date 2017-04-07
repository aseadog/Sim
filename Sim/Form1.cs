using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sim
{
    public partial class Form1 : Form
    {

        List<PictureBox> droplets = new List<PictureBox>();
        const int nDroplets = 100;
        Size sizeDroplet = new Size(2,2);
        Random rand = new Random();
        const int screenWidth = 1000;
        const int screenHeight = 600;
        Color dropletColor = Color.Blue;
        BorderStyle borders = BorderStyle.FixedSingle;
        Timer simTime;
        const int simTimeInterval = 1;
        int[,] speed = new int[nDroplets, 2];
        const double gravity = 1;
        double errorMargin = 3;
        double errornext = 5;
        Color sidesColor = Color.Gray;
        double dampner = 0.5;
        PictureBox taxDivider = new PictureBox();
        PictureBox taxDividerBottom = new PictureBox();
        PictureBox taxBottom = new PictureBox();
        PictureBox salaryBottom = new PictureBox();

        double[,] loc = new double [nDroplets, 2];
        double[,] vel = new double[nDroplets, 2];

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Width = screenWidth;
            this.Height = screenHeight;
            this.CenterToScreen();
            this.Controls.Add(taxDivider);
            this.Controls.Add(taxDividerBottom);
            this.Controls.Add(taxBottom);
            this.Controls.Add(salaryBottom);

            for (int i = 0; i < nDroplets; i++)
            {
                
                vel[i, 0] = rand.Next(1, 3);
                vel[i, 1] = rand.Next(0, 3);

            }

          

            for (int i = 0; i < nDroplets; i++)
            {
                PictureBox droplet = new PictureBox();
                droplet.Size = sizeDroplet;
                loc[i, 0] = rand.Next(150, 400);
                loc[i, 1] = rand.Next(50, 100);
                droplet.Location = new Point(Convert.ToInt32(loc[i,0]), Convert.ToInt32(loc[i, 1]));
                droplet.BackColor = dropletColor;
                droplets.Add(droplet);
            }

            foreach (PictureBox droplet in droplets)
            {
                this.Controls.Add(droplet);
            }

            simTime = new Timer();
            simTime.Interval = simTimeInterval;
            simTime.Enabled = true;
            simTime.Tick += new EventHandler(simTimeTick);

        }


        void simTimeTick(object sender, EventArgs e)
        {
           

            PictureBox incomeRight = new PictureBox();
            incomeRight.Location = new Point(180, 100);
            incomeRight.BackColor = sidesColor;
            incomeRight.Size = new Size(30, 350);
            this.Controls.Add(incomeRight);

            
            taxDivider.Location = new Point(trackBar1.Value, 120);
            taxDivider.BackColor = sidesColor;
            taxDivider.Size = new Size(20, 60);
            
            

            PictureBox taxRight = new PictureBox();
            taxRight.Location = new Point(300, 180);
            taxRight.BackColor = sidesColor;
            taxRight.Size = new Size(30, 80);
            this.Controls.Add(taxRight);

           
            taxBottom.Location = new Point(200, 260);
            taxBottom.BackColor = sidesColor;
            taxBottom.Size = new Size(trackBar2.Value, 30);
           


            taxDividerBottom.Location = new Point(300, 160);
            taxDividerBottom.BackColor = sidesColor;
            taxDividerBottom.Size = new Size(taxDivider.Location.X - 300, 20);
            



            PictureBox mainBottom = new PictureBox();
            mainBottom.Location = new Point(130, 500);
            mainBottom.BackColor = sidesColor;
            mainBottom.Size = new Size(400, 20);
            this.Controls.Add(mainBottom);

            PictureBox maintop = new PictureBox();
            maintop.Location = new Point(100, 20);
            maintop.BackColor = sidesColor;
            
            maintop.Size = new Size(500, 30);
            this.Controls.Add(maintop);

            PictureBox incomeLeft = new PictureBox();
            incomeLeft.Location = new Point(100, 20);
            incomeLeft.BackColor = sidesColor;
            incomeLeft.Size = new Size(30, 500);
            this.Controls.Add(incomeLeft);

            PictureBox mainsupplytop = new PictureBox();
            mainsupplytop.Location = new Point(180, 80);
            mainsupplytop.BackColor = sidesColor;
            mainsupplytop.Size = new Size(150, 30);
            this.Controls.Add(mainsupplytop);

          
            salaryBottom.Location = new Point(330 + trackBar4.Value, 230);
            salaryBottom.BackColor = sidesColor;
            salaryBottom.Size = new Size(trackBar3.Value - trackBar4.Value, 30);
            

            PictureBox salaryRight = new PictureBox();
            salaryRight.Location = new Point(450, 120);
            salaryRight.BackColor = sidesColor;
            salaryRight.Size = new Size(30, 180);
            this.Controls.Add(salaryRight);

            PictureBox savingsBottom = new PictureBox();
            savingsBottom.Location = new Point(370, 300);
            savingsBottom.BackColor = sidesColor;
            savingsBottom.Size = new Size(110, 30);
            this.Controls.Add(savingsBottom);

            PictureBox bottomGuard = new PictureBox();
            bottomGuard.Location = new Point(400, 350);
            bottomGuard.BackColor = sidesColor;
            bottomGuard.Size = new Size(30, 170);
            this.Controls.Add(bottomGuard);

            for (int i = 0; i < nDroplets; i++)
            {

                vel[i, 1] = vel[i, 1] + gravity;

               
                loc[i, 0] = loc[i, 0] + vel[i, 0];
                loc[i, 1] = loc[i, 1] + vel[i, 1];
               

                if (checkCollisionXL(droplets[i]))
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = -loc[i, 0];
                    
                }

             

                if (checkCollisionXR(droplets[i]))
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = loc[i, 0] - 2 * (loc[i, 0] + Convert.ToDouble(sizeDroplet.Width) - Convert.ToDouble(screenWidth));
                }

                if (checkCollisionYT(droplets[i]))
                {
                    vel[i, 1] = -vel[i, 1];
                    loc[i, 1] = -loc[i, 1];
                }

                if (checkCollisionYB(droplets[i]))
                {
                    vel[i, 1] = -vel[i, 1]*dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + Convert.ToDouble(sizeDroplet.Height) - Convert.ToDouble(screenHeight));
                }


                //side collisions finished, still in for loop

                // chamber collisions

               

                if (droplets[i].Location.X >= incomeRight.Location.X && droplets[i].Location.X < incomeRight.Location.X + errornext && droplets[i].Location.Y > incomeRight.Bounds.Top && droplets[i].Location.Y < incomeRight.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * incomeRight.Location.X - loc[i, 0];
                }

                if (droplets[i].Location.X <= incomeRight.Bounds.Right && droplets[i].Location.X > incomeRight.Bounds.Right - errornext && droplets[i].Location.Y > incomeRight.Bounds.Top && droplets[i].Location.Y < incomeRight.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0]*dampner;
                    loc[i, 0] = loc[i, 0] + 2 * (incomeRight.Bounds.Right - loc[i,0]);
                }


                if (droplets[i].Location.X >= taxDivider.Bounds.Left && droplets[i].Location.X < taxDivider.Bounds.Left + errornext && droplets[i].Location.Y > taxDivider.Bounds.Top && droplets[i].Location.Y < taxDivider.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * taxDivider.Location.X - loc[i, 0];
                }

                if (droplets[i].Location.Y + sizeDroplet.Height >= taxDivider.Bounds.Top && droplets[i].Location.Y  + sizeDroplet.Height< taxDivider.Bounds.Top + errornext && droplets[i].Location.X > taxDivider.Bounds.Left && droplets[i].Location.X < taxDivider.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1];
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - taxDivider.Bounds.Top);
                }

                if (droplets[i].Location.X < taxDivider.Bounds.Right && droplets[i].Location.X > taxDivider.Bounds.Right - errornext && droplets[i].Location.Y > taxDivider.Bounds.Top && droplets[i].Location.Y < taxDivider.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = loc[i, 0] + 2 * (taxDivider.Bounds.Right-loc[i,0]);
                }

                if (droplets[i].Location.X >= taxRight.Bounds.Left && droplets[i].Location.X < taxRight.Bounds.Left + errornext && droplets[i].Location.Y > taxRight.Bounds.Top && droplets[i].Location.Y < taxRight.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * taxRight.Location.X - loc[i, 0];
                }

                if (droplets[i].Location.X <= taxRight.Bounds.Right && droplets[i].Location.X > taxRight.Bounds.Right - errornext && droplets[i].Location.Y > taxRight.Bounds.Top && droplets[i].Location.Y < taxRight.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = loc[i, 0] + 2 * (taxRight.Bounds.Right - loc[i, 0]);
                }


                if (droplets[i].Location.Y >= mainBottom.Location.Y && droplets[i].Location.Y < mainBottom.Bounds.Bottom && droplets[i].Location.X > mainBottom.Bounds.Left && droplets[i].Location.X < mainBottom.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1]*dampner;
                    loc[i, 1] = loc[i,1] - 2 * (loc[i, 1] + sizeDroplet.Height - mainBottom.Bounds.Top);

                }

                if (droplets[i].Location.Y >= taxBottom.Location.Y && droplets[i].Location.Y < taxBottom.Bounds.Bottom && droplets[i].Location.X > taxBottom.Bounds.Left && droplets[i].Location.X < taxBottom.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - taxBottom.Bounds.Top);

                }

                if (droplets[i].Location.Y >= taxDividerBottom.Location.Y && droplets[i].Location.Y < taxDividerBottom.Bounds.Bottom && droplets[i].Location.X > taxDividerBottom.Bounds.Left && droplets[i].Location.X < taxDividerBottom.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - taxDividerBottom.Bounds.Top);

                }

                if (droplets[i].Location.Y >= mainsupplytop.Location.Y && droplets[i].Location.Y < mainsupplytop.Bounds.Bottom && droplets[i].Location.X > mainsupplytop.Bounds.Left && droplets[i].Location.X < mainsupplytop.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - mainsupplytop.Bounds.Top);

                }


                if (droplets[i].Location.X <= incomeLeft.Bounds.Right && droplets[i].Location.X > incomeLeft.Bounds.Right - errornext && droplets[i].Location.Y > incomeLeft.Bounds.Top && droplets[i].Location.Y < incomeLeft.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = loc[i, 0] + 2 * (incomeLeft.Bounds.Right - loc[i, 0]);
                }


                if (droplets[i].Location.X >= incomeLeft.Location.X && droplets[i].Location.X < incomeLeft.Location.X + errornext && droplets[i].Location.Y > incomeLeft.Bounds.Top && droplets[i].Location.Y < incomeLeft.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * incomeLeft.Location.X - loc[i, 0];
                }

                //top

                if (droplets[i].Location.Y <= maintop.Bounds.Bottom && droplets[i].Location.Y > maintop.Bounds.Bottom - errornext && droplets[i].Location.X >= maintop.Bounds.Left && droplets[i].Location.X < maintop.Bounds.Right - sizeDroplet.Width)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] + 2 * (maintop.Bounds.Bottom - loc[i,1]);

                }

                // income pump

                if (droplets[i].Location.X > incomeLeft.Bounds.Right && droplets[i].Location.X < incomeRight.Bounds.Left )
                {
                    vel[i, 0] = 0;
                    vel[i, 1] = -5;
                }

                if (droplets[i].Location.X > incomeLeft.Bounds.Right && droplets[i].Location.X < incomeRight.Bounds.Left && droplets[i].Bounds.Bottom< mainsupplytop.Bounds.Top)
                {
                    vel[i, 0] = 3;
                    
                }

                if (droplets[i].Location.Y >= salaryBottom.Location.Y && droplets[i].Location.Y < salaryBottom.Bounds.Bottom && droplets[i].Location.X > salaryBottom.Bounds.Left && droplets[i].Location.X < salaryBottom.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - salaryBottom.Bounds.Top);

                }

                if (droplets[i].Location.X >= salaryRight.Bounds.Left && droplets[i].Location.X < salaryRight.Bounds.Left + errornext && droplets[i].Location.Y > salaryRight.Bounds.Top && droplets[i].Location.Y < salaryRight.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * salaryRight.Location.X - loc[i, 0];
                }


                if (droplets[i].Location.Y >= savingsBottom.Location.Y && droplets[i].Location.Y < savingsBottom.Bounds.Bottom && droplets[i].Location.X > savingsBottom.Bounds.Left && droplets[i].Location.X < savingsBottom.Bounds.Right)
                {
                    vel[i, 1] = -vel[i, 1] * dampner;
                    loc[i, 1] = loc[i, 1] - 2 * (loc[i, 1] + sizeDroplet.Height - savingsBottom.Bounds.Top);

                }

                if (droplets[i].Location.X >= bottomGuard.Bounds.Left && droplets[i].Location.X < bottomGuard.Bounds.Left + errornext && droplets[i].Location.Y > bottomGuard.Bounds.Top && droplets[i].Location.Y < bottomGuard.Bounds.Bottom)
                {
                    vel[i, 0] = -vel[i, 0];
                    loc[i, 0] = 2 * bottomGuard.Location.X - loc[i, 0];
                }


                // check droplet collisions

                //for (int j = 0; j < nDroplets; j++) // check x collision
                //{
                //    if (loc[i,0] + Convert.ToDouble(sizeDroplet.Width) > loc[j,0] - errorMargin && loc[i, 0] + Convert.ToDouble(sizeDroplet.Width) < loc[j, 0] + errorMargin && loc[i,1] + Convert.ToDouble(sizeDroplet.Height) >= loc[j,1] && loc[i,1] <= loc[j,1] + Convert.ToDouble(sizeDroplet.Height) && i!=j)
                   

                //    {
                //        double hold;
                       
                //        hold = vel[i, 0];
                //        vel[i, 0] = vel[j, 0];
                //        vel[j, 0] = hold;

                      
                //    }

                //}

                //for (int j = 0; j < nDroplets; j++)  // check y collision
                //{
                //    if (loc[i, 1] + Convert.ToDouble(sizeDroplet.Height) > loc[j, 1] - errorMargin && loc[i, 1] + Convert.ToDouble(sizeDroplet.Height) < loc[j, 1] + errorMargin && loc[i, 0] + Convert.ToDouble(sizeDroplet.Width) >= loc[j, 0] && loc[i, 0] <= loc[j, 0] + Convert.ToDouble(sizeDroplet.Width) && i != j)
                //    {
                //        double hold;
                      
                //        hold = vel[i, 1];
                //        vel[i, 1] = vel[j, 1];
                //        vel[j, 1] = hold;


                //    }

                //}

                droplets[i].Location = new Point(Convert.ToInt32(loc[i, 0]), Convert.ToInt32(loc[i, 1]));
                

            }

            
                      

        }


        public bool checkCollisionXL(PictureBox inputPictureBox)
        {
            if (inputPictureBox.Bounds.X <= 0 )
                return true;
            return false;
        }

        public bool checkCollisionXR(PictureBox inputPictureBox)
        {
            if ( inputPictureBox.Bounds.Right >= screenWidth)
                return true;
            return false;
        }

        public bool checkCollisionYT(PictureBox inputPictureBox)
        {
            if (inputPictureBox.Bounds.Top <= 0)
                return true;
            return false;
        }

        public bool checkCollisionYB(PictureBox inputPictureBox)
        {
            if ( inputPictureBox.Bounds.Bottom >= screenHeight)
                return true;
            return false;
        }

        public bool checkCollisionLeftMain(PictureBox inputPictureBox, PictureBox inputBoundary)
        {
            if (inputPictureBox.Bounds.IntersectsWith(inputBoundary.Bounds))
                return true;
            return false;
        }




        

       
    }
}
