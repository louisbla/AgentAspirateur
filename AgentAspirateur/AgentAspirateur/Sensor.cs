using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Sensor
    {
        Castle castle;

        public Sensor(Castle castle) {
            this.castle = castle;
        }
        public Castle GetEnvironnement()
        {
            return new Castle(castle);
        }
    }
}
