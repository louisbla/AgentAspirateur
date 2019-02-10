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

        public event EventHandler MakeAMove;
        public event EventHandler DidAspire;
        public event EventHandler DidGrab;
        public void InformMove() => MakeAMove?.Invoke(this, EventArgs.Empty);
        public void InformAspire() => DidAspire?.Invoke(this, EventArgs.Empty);
        public void InformGrab() => DidGrab?.Invoke(this, EventArgs.Empty);

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
                CreateIntent();

                DoAction();
            }
        }

        private Castle ObserveEnvironnementWithSensors()
        {
            return sensor.GetEnvironnement();
        }

        private void UpdateMyState(Castle castle)
        {
            etatInterne.Belief = castle;
            etatInterne.room = castle.Rooms[posX][PosY];
        }

        private void CreateIntent()
        {
            etatInterne.Intents.Clear();

            String action = "";

            Noeud noeud;

            noeud = AStarAlgo(etatInterne);

            if (etatInterne.Belief.Rooms[posX][PosY].Dust)
            {
                effector.Aspire(etatInterne.Belief.Rooms[posX][PosY]);
                InformAspire();
            }
            if (etatInterne.Intents.Count == 0)
            {
                etatInterne.Intents.Enqueue("bas");
                etatInterne.Intents.Enqueue("droite");
                etatInterne.Intents.Enqueue("bas");
                etatInterne.Intents.Enqueue("droite");
            }
        }

        private Noeud AStarAlgo(EtatInterne etat)
        {
            Noeud depart = new Noeud(etat.room);
            Noeud arrive = new Noeud(PoussiereLaPlusProche());
            Noeud actuel = depart;
            actuel.Cost = Distance(depart.room, actuel.room) + Distance(actuel.room, arrive.room);

            while(actuel != arrive)
            {

            }

            return null;
        }

        private Room PoussiereLaPlusProche()
        {
            List<Room> dirtyRooms = new List<Room>();
            

            foreach(Room[] rooms in etatInterne.Belief.Rooms)
            {
                foreach (Room room in rooms)
                {
                    if (room.Dust)
                    {
                        dirtyRooms.Add(room);
                    }
                }
            }

            Room closestDirty = dirtyRooms[0];
            int closestDistance = Distance(etatInterne.room, dirtyRooms[0]);
            foreach(Room room in dirtyRooms)
            {
                int distance = Distance(etatInterne.room, room);
                if ( distance < closestDistance)
                {
                    closestDirty = room;
                    closestDistance = distance;
                }
            }


            return closestDirty;
        }

        private int Distance(Room start, Room end)
        {
            return Math.Abs(start.PosX - end.PosY) + Math.Abs(start.PosY - end.PosY);
        }

        private void DoAction()
        {
            String action = etatInterne.Intents.Dequeue();

            switch (action)
            {
                case "grab":
                    effector.Grab(etatInterne.Belief.Rooms[PosX][PosY]);
                    InformGrab();
                    break;
                case "aspire":
                    effector.Aspire(etatInterne.Belief.Rooms[PosX][PosY]);
                    InformAspire();
                    break;
                case "haut":
                    if(effector.Move(this, "haut"))
                        InformMove();
                    break;
                case "gauche":
                    if (effector.Move(this, "gauche"))
                        InformMove();
                    break;
                case "droite":
                    if (effector.Move(this, "droite"))
                        InformMove();
                    break;
                case "bas":
                    if (effector.Move(this, "bas"))
                        InformMove();
                    break;

            }
        }
    }
}
