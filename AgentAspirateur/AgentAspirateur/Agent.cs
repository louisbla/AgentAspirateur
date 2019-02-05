using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Agent
    {
        private bool isAlive = false;

        public void Run()
        {
            isAlive = true;

            while (isAlive)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Agent is running");
            }
        }
    }
}
