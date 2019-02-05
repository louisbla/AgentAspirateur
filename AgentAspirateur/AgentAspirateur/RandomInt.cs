using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class RandomInt
    {
        public static Random rdm;

        public static int GetRandomInt()
        {
            if(rdm == null)
            {
                rdm = new Random();
            }

            return rdm.Next(1000);
        }
    }
}
