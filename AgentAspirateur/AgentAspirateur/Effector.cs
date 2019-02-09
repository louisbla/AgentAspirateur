using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Effector
    {
        public bool Move(Agent agent, String direction)
        {
            bool movementPossible = false;
            switch (direction)
            {
                case "haut":
                    if (agent.PosY > 0)
                    {
                        movementPossible = true;
                        agent.PosY--;
                    }
                    break;
                case "bas":
                    if (agent.PosY < 9)
                    {
                        movementPossible = true;
                        agent.PosY++;
                    }
                    break;
                case "droite":
                    if (agent.PosX < 9)
                    {
                        movementPossible = true;
                        agent.PosX++;
                    }
                    break;
                case "gauche":
                    if (agent.PosX > 0)
                    {
                        movementPossible = true;
                        agent.PosX--;
                    }
                    break;
            }
            return movementPossible;
        }

        public void Grab(Room room)
        {
            room.Jewel = false;
        }

        public void Aspire(Room room)
        {
            room.Jewel = false;
            room.Dust = false;
        }
    }
}
