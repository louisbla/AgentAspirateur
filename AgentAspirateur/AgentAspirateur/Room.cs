using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{

    class Room
    {
        public bool Jewel { get; set; }
        public bool Dust { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Room(int i, int j)
        {
            PosX = i;
            PosY = j;
            this.Night();
            Jewel = false;
            Dust = false;
        }

        public Room(Room room)
        {
            PosX = room.PosX;
            PosY = room.PosY;
            Jewel = room.Jewel;
            Dust = room.Dust;
        }

        private void GenerateJewel()
        {
            int probability = RandomInt.GetRandomInt();
            if(probability < 5)
            {
                Jewel = true;
            }
        }

        private void GenerateDust()
        {
            int probability = RandomInt.GetRandomInt();
            if (probability < 20)
            {
                Dust = true;
            }
        }

        /// <summary>
        /// Fait comme si une nuit avait été passée dans la chambre : de la poussière ou des bijoux peuvent apparaitre.
        /// </summary>
        public void Night()
        {
            this.GenerateJewel();
            this.GenerateDust();
        }
    }
}
