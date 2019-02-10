using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class EtatInterne
    {
        private Castle belief;
        //Desire
        private Queue<String> intents;

        public Room room { get; set; }

        public Castle Belief { get => belief; set => belief = value; }
        public Queue<String> Intents { get => intents; set => intents = value; }


        public EtatInterne()
        {
            Intents = new Queue<string>();
        }
    }
}
