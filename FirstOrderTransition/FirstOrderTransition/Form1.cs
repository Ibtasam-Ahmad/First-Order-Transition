using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstOrderTransition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = 51;
            int[,] spinsystem = new int[size, size];
            double T = 2, H, mu = 1;
              int sweeps = 100;
            //Graphics system
            Graphics gg = CreateGraphics();
            SolidBrush bb = new SolidBrush(Color.Blue);
            SolidBrush br = new SolidBrush(Color.Red);
            Pen pb = new Pen(Color.Blue,2);
            //Initialize the spin system
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    spinsystem[i, j] = 1;//all spin up
                    gg.FillEllipse(bb, 400 + i * 5,
                        100 + j * 5, 4, 4);
                }
            }
            double M = 0, Mave = 0;
            //Start increasing H
            for (H = -5; H < 5; H = H + 0.1)
            {
               
                for(int sw=0;sw<sweeps;sw++)
                {
                  
                    for (int i = 1; i < size - 1; i++)
                    {
                        for (int j = 1; j < size - 1; j++)
                        {
                            Flip(i, j,spinsystem,H,mu,T,bb,br,gg);
                        }
                    }
                }
                Mave = 0;
                for (int i = 1; i < size - 1; i++)
                {
                    for (int j = 1; j < size - 1; j++)
                    {
                        Mave = Mave + spinsystem[i, j];
                    }
                }
                M = Mave / (size * size - 4 * size + 4);
                gg.FillEllipse(br, 200 + (float)H * 30, 
                    500 - (float)M * 20, 5, 5);
            }


        }
        void Flip(int i,int j,
            int [,] arr,double H,double mu,double T,
            SolidBrush bb, SolidBrush br,Graphics gg)
        {
            Random obj=new Random();
            double E1 = -arr[i, j] * (arr[i - 1, j] + arr[i + 1, j]
                + arr[i, j - 1] + arr[i, j + 1] + mu * H);
            //E1 before flipping
            arr[i, j] = arr[i, j] * -1;//flip the spin
            double E2 = -arr[i, j] * (arr[i - 1, j] + arr[i + 1, j]
                + arr[i, j - 1] + arr[i, j + 1] + mu * H);

            double Eflip = E2 - E1;
            double Pflip = Math.Exp(-Eflip/(T));
            //Kb=1
            if (Eflip < 0)
            {
                if (arr[i, j] == -1)
                    gg.FillEllipse(br, 400 + i * 5,
                       100 + j * 5, 4, 4);
                else
                    gg.FillEllipse(bb, 400 + i * 5,
                       100 + j * 5, 4, 4);

            }
            else
            {

                double prob = obj.NextDouble();
                if (Pflip >= prob)
                {
                    if (arr[i, j] == -1)
                        gg.FillEllipse(br, 400 + i * 5,
                       100 + j * 5, 4, 4);
                    else
                        gg.FillEllipse(bb, 400 + i * 5,
                        100 + j * 5, 4, 4);
                }
                else
                {
                    arr[i, j] = arr[i, j] * -1;//reject the flipping
                }
            }
        }
    }
}
