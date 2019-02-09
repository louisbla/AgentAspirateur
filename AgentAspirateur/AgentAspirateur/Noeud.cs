using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Noeud
    {
        Noeud Parent { get; set; }
        Room ActualState { get; set; }
        private String Action { get; set; }
        private int Depth { get; set; }
        private int Cost { get; set; }


        public Noeud(Room room)
        {
            Parent = null;
            ActualState = room;
            Action = null;
            Depth = 0;
            Cost = 0;
        }


    }
}
