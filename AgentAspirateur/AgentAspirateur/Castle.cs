using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AgentAspirateur
{
    class Castle 
    {
        public int scorePerformence;

        public event EventHandler DataChanged;

        public void Inform() => DataChanged?.Invoke(this, EventArgs.Empty);

        public Room[][] Rooms { get; set; }
        private bool gameIsRunning = false;

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
            scorePerformence = 0;

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

        public Castle()
        {
            scorePerformence = 0;

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

        private void Initialize()
        {
            scorePerformence = 0;

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
