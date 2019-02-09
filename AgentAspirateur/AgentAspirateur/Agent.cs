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
        //private InternState bdi;
        private int posX;
        private int posY;
        private Sensor sensor;
        private Effector effector = new Effector();
        private EtatInterne etatInterne = new EtatInterne();

        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }

        public Agent(Castle castle)
        {
            sensor = new Sensor(castle);
            PosX = 0;
            PosY = 0;
        }

        public void Run()
        {
            isAlive = true;

            while (isAlive)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Agent is running");

                Castle tempCastle = ObserveEnvironnementWithSensors();
                UpdateMyState(tempCastle);
                String action = ChooseAnAction();

                DoAction(action);
            }
        }

        private Castle ObserveEnvironnementWithSensors()
        {
            return sensor.GetEnvironnement();
        }

        private void UpdateMyState(Castle castle)
        {
            etatInterne.Belief = castle;
        }

        private String ChooseAnAction()
        {
            return "droite";
        }

        private void DoAction(String action)
        {
            effector.Move(this, action);
        }
    }
}
