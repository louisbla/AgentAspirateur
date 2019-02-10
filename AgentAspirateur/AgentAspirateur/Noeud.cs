using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Noeud
    {
        public Noeud Parent { get; set; }
        public Room room { get; set; }
        public String Action { get; set; }
        public int Depth { get; set; }
        public int Cost { get; set; }


        public Noeud(Room room)
        {
            Parent = null;
            this.room = room;
            Action = "";
            Depth = 0;
            Cost = 0;
        }


    }
}
