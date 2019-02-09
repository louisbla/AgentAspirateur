using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AgentAspirateur
{
    class Castle 
    {
        public int ScorePerformence { get; set; }

        public event EventHandler DataChanged;

        public void Inform() => DataChanged?.Invoke(this, EventArgs.Empty);

        public Room[][] Rooms { get; set; }
        private bool gameIsRunning = false;
        private Agent agent;

        public void Run()
        {
            this.Initialize();

            gameIsRunning = true;
            while (gameIsRunning == true)
            {
                Thread.Sleep(1000);
                this.ConsumeNight();
                this.Inform();
            }
        }
        public Castle(Castle castle)
        {
            ScorePerformence = 0;

            this.Rooms = new Room[10][];
            for (int i = 0; i < 10; i++)
            {
                this.Rooms[i] = new Room[10];
                for (int j = 0; j < 10; j++)
                {
                    this.Rooms[i][j] = new Room(castle.Rooms[i][j]);
                }
            }
        }

        public void SubscribeToAgent(Agent agent)
        {
            this.agent = agent;
            agent.MakeAMove += HandleMakeAMove;
            agent.DidAspire += HandleDidAspire;
            agent.DidGrab += HandleDidGrab;
        }

        public Castle()
        {
            ScorePerformence = 0;

            this.Rooms = new Room[10][];
            for (int i = 0; i < 10; i++)
            {
                this.Rooms[i] = new Room[10];
                for (int j = 0; j < 10; j++)
                {
                    this.Rooms[i][j] = new Room();
                }
            }
        }

        /// <summary>
        /// Updates the scorePerformence when the robot makes a move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandleMakeAMove(object sender, EventArgs args)
        {
            ScorePerformence--;
        }
        /// <summary>
        /// Updates the scorePerformence when the robot aspires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandleDidAspire(object sender, EventArgs args)
        {
            ScorePerformence--;
            if(Rooms[agent.PosX][agent.PosY].Dust == true)
            {
                ScorePerformence += 10;
            }
            if (Rooms[agent.PosX][agent.PosY].Jewel == true)
            {
                ScorePerformence -= 50;
            }

            Rooms[agent.PosX][agent.PosY].Dust = false;
            Rooms[agent.PosX][agent.PosY].Jewel = false;

        }
        /// <summary>
        /// Updates the scorePerformence when the robot makes a grab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandleDidGrab(object sender, EventArgs args)
        {
            ScorePerformence--;
            if (Rooms[agent.PosX][agent.PosY].Jewel == true)
            {
                ScorePerformence += 50;
            }

            Rooms[agent.PosX][agent.PosY].Jewel = false;
        }

        private void Initialize()
        {
            ScorePerformence = 0;

            this.Rooms = new Room[10][];
            for (int i = 0; i< 10; i++)
            {
                this.Rooms[i] = new Room[10];
                for(int j = 0; j<10; j++)
                {
                    this.Rooms[i][j] = new Room();
                }
            }
        }

        /// <summary>
        /// Fait comme si une nuit avait été passée dans le chateau : de la poussière ou des bijoux peuvent apparaitre dans les chambres
        /// </summary>
        public void ConsumeNight()
        {
            if (Rooms != null)
            {
                foreach (Room[] aisle in Rooms)
                {
                    foreach (Room room in aisle)
                    {
                        room.Night();
                    }
                }
            }
        }


    }
}
