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
        String statutBarText = "StatutBar";

        List<Panel> listPanels;

        Bitmap poussiere = new Bitmap(@"..\..\images\poussiere.jpg");
        Bitmap bijou = new Bitmap(@"..\..\images\bijou.jpg");
        Bitmap propre = new Bitmap(@"..\..\images\propre.jpg");
        Bitmap robot = new Bitmap(@"..\..\images\robot2.png");

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
            agent = new Agent(castle);
            Thread thread2 = new Thread(new ThreadStart(agent.Run));
            thread2.Start();
            castle.SubscribeToAgent(agent);

            

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
                panel.BackgroundImageLayout = ImageLayout.Stretch;
                this.flowLayoutPanel1.Controls.Add(panel);
            }
        }

        /// <summary>
        /// Updates the UI when the castle changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandleDataChanged(object sender, EventArgs args)
        {
            int compteur = 0;

            Room[][] rooms = castle.Rooms;
            for(int j = 0; j < rooms.Length; j++)
            {
                for(int i = 0; i < rooms[j].Length; i++)
                {
                    if (rooms[i][j].Dust && rooms[i][j].Jewel)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.Red;
                    }
                    else if (rooms[i][j].Dust)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.SandyBrown;
                        //listPanels[compteur].BackgroundImage = poussiere;
                    }
                    else if (rooms[i][j].Jewel)
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.Yellow;
                        //listPanels[compteur].BackgroundImage = bijou;
                    }
                    else
                    {
                        listPanels[compteur].BackColor = System.Drawing.Color.DeepSkyBlue;
                        //listPanels[compteur].BackgroundImage = propre;
                    }
                    if (agent.PosX == i && agent.PosY == j)
                    {
                        listPanels[compteur].BackgroundImage = robot;
                    }
                    else
                    {
                        listPanels[compteur].BackgroundImage = null;
                    }
                    compteur++;
                }

            }
            statusStrip1.Items.Clear();
            statusStrip1.Items.Add("Score du robot : " + castle.ScorePerformence);
            Console.WriteLine("The castle has changed, UI had been updated");
            Invalidate();
        }

    }
}
