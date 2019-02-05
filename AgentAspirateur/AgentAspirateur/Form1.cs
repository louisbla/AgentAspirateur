using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentAspirateur
{
    public partial class Form1 : Form
    {
        Castle castle;
        Agent agent;

        List<Panel> listPanels;

        public Form1()
        {
            InitializeComponent();
            InitializePanels();
            
            //Create the environnement thread
            castle = new Castle();
            castle.DataChanged += HandleDataChanged;
            Thread thread = new Thread(new ThreadStart(castle.Run));
            thread.Start();

            //Create the agent thread
            agent = new Agent();
            Thread thread2 = new Thread(new ThreadStart(agent.Run));
            thread2.Start();

            Invalidate();
        }

        /// <summary>
        /// Draw a 10*10 panel grid
        /// </summary>
        private void InitializePanels()
        {
            listPanels = new List<Panel>();
            for (int i = 0; i < 100; i++)
            {
                Panel panel = new Panel();
                listPanels.Add(panel);
                panel.BackColor = System.Drawing.Color.DeepSkyBlue;
                panel.Name = "panel" + i;
                int size = this.flowLayoutPanel1.Width / 11;
                panel.Size = new System.Drawing.Size(size, size);
                panel.Location = new System.Drawing.Point(i, 0);
                this.flowLayoutPanel1.Controls.Add(panel);
            }
        }

        public void HandleDataChanged(object sender, EventArgs args)
        {
            int compteur = 0;

            Room[][] rooms = castle.Rooms;
            for(int i = 0; i < rooms.Length; i++)
            {
                for(int j = 0; j < rooms[i].Length; j++)
                {
                    Console.WriteLine("Room[" + i + "][" + j + "] : Dust="+ rooms[i][j].Dust);


                    if (rooms[i][j].Dust && rooms[i][j].Jewel)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.Red;
                    }
                    else if (rooms[i][j].Dust)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.SandyBrown;
                    }
                    else if (rooms[i][j].Jewel)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.DeepSkyBlue;
                    }
                    compteur++;
                }
            }

            Console.WriteLine("The castle has changed, updating UI");
            Invalidate();
        }
    }
}
