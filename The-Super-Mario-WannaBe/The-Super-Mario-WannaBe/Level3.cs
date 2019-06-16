using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Level3 : Level
    {

        public class ElevatorFloor
        {
            public enum FloorType
            {
                GoingDown,
                GoingUp
            }

            public static readonly int UpperLimit = GenericBlock1.Height;
            public static readonly int LowerLimit = FormHeight - GenericBlock1.Height;
            public static readonly Image MovingFloor = Properties.Resources.MovingFloor;
            public Rectangle Bounds { get; set; }
            public int Speed { get; set; }
            public FloorType Type;

            public ElevatorFloor(Point Location, FloorType Type)
            {
                if (Type.Equals(FloorType.GoingDown))
                {
                    Speed = 2;
                }
                else
                {
                    Speed = -1;
                }

                this.Bounds = new Rectangle(Location.X, Location.Y, MovingFloor.Width + 15, MovingFloor.Height + 2);
                this.Type = Type;
            }

            public void Move()
            {
                if (Type == FloorType.GoingUp)
                {
                    if ( Bounds.Top < UpperLimit)
                    {
                        Bounds = new Rectangle(Bounds.X, LowerLimit, Bounds.Width, Bounds.Height);
                    }
                    else
                    {
                        Bounds = new Rectangle(Bounds.X, Bounds.Y + Speed, Bounds.Width, Bounds.Height);
                    }
                }
                else
                {
                    if (Bounds.Bottom > LowerLimit)
                    {
                        Bounds = new Rectangle(Bounds.X, UpperLimit, Bounds.Width, Bounds.Height);
                    }
                    else
                    {
                        Bounds = new Rectangle(Bounds.X, Bounds.Y + Speed, Bounds.Width, Bounds.Height);
                    }
                }
            }

            public void Draw(Graphics g)
            {
                g.DrawImage(MovingFloor, Bounds);
            }
        }

        public static readonly Image UpsideDownSpikes = Properties.Resources.UpsideDownSpikes;
        public static readonly Image UpRightSpikes = Properties.Resources.UpRightSpikes;
        public static readonly Image GenericBlock1 = Properties.Resources.generic_block1;
        public static readonly Image GenericBlock2 = Properties.Resources.generic_block2;
        public static readonly Image Pole = Properties.Resources.Pole;
        public static readonly int FormHeight = Level2.FormHeight;
        public static readonly int FormWidth = Level2.FormWidth + 1;

        public Size SizeOfTrigger = Properties.Resources.SingleSpikeUpsideDown.Size;

        public List<Rectangle> UpsideDownTriggers { get; set; }
        public List<Rectangle> UpRightTriggers { get; set; }
        public List<ElevatorFloor> Elevator1 { get; set; }
        public List<ElevatorFloor> Elevator2 { get; set; }
        public List<ElevatorFloor> Elevator3 { get; set; }
        public Rectangle UpsideDownSpikesBounds { get; set; }
        public Rectangle UpRightSpikesBounds { get; set; }

        public bool c { get; set; }
        public int check { get; set; }

        public List<Rectangle> Poles { get; set; }
        public Random RandomFloorGenerator { get; set; } // za inicijalni pozicii na floors vo ramki na eden elevator

        public Level3(Hero Hero)
        {
            c = true;
            check = 0;

            Boundaries = new List<Rectangle>();
            UpsideDownTriggers = new List<Rectangle>();
            UpRightTriggers = new List<Rectangle>();
            Poles = new List<Rectangle>();
            InitializeBounds();
            InitializePoles();

            RandomFloorGenerator = new Random();

            UpsideDownSpikesBounds = new Rectangle(GenericBlock1.Width, GenericBlock1.Height, 21 * GenericBlock1.Width, GenericBlock1.Height);
            UpRightSpikesBounds = new Rectangle(GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height, 23 * GenericBlock1.Width, GenericBlock1.Height);

            InitializeElevator1();
            InitializeElevator2();
            InitializeElevator3();

            InitializeTriggers();

            this.Hero = Hero;
            this.Hero.Character = new RectangleF(FormWidth - GenericBlock1.Width, 7 * GenericBlock1.Height, Hero.Character.Width, Hero.Character.Height);
        }

        public void InitializeBounds()
        {
            // Adding Celling
            Boundaries.Add(new Rectangle(0, 0, 25 * GenericBlock1.Width, GenericBlock1.Height));

            // Adding Floor
            Boundaries.Add(new Rectangle(0, FormHeight - GenericBlock1.Height, 25 * GenericBlock1.Width, GenericBlock1.Height));

            // Adding RightWall
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, FormHeight - 10 * GenericBlock1.Height, GenericBlock1.Width, 10 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(FormWidth - 2 * GenericBlock1.Width, FormHeight - 11 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, GenericBlock1.Height, GenericBlock1.Width, 3 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(FormWidth - 2 * GenericBlock1.Width, GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));


            // Adding LeftWall
            Boundaries.Add(new Rectangle(0, GenericBlock1.Height, GenericBlock1.Width, 4 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(GenericBlock1.Width, GenericBlock1.Height * 8, GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(0, 8 * GenericBlock1.Height, GenericBlock1.Width, 10 * GenericBlock1.Height));
        }

        public void InitializeElevator1()
        {
            Elevator1 = new List<ElevatorFloor>();
            int size = ElevatorFloor.LowerLimit + 1 - ElevatorFloor.UpperLimit;

            int FirstFloorY = RandomFloorGenerator.Next(ElevatorFloor.UpperLimit, ElevatorFloor.LowerLimit + 1);

            int SecondFloorY, ThirdFloorY, FourthFloorY;
            SecondFloorY = (FirstFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            ThirdFloorY = (SecondFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            FourthFloorY = (ThirdFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            int X = 18 * GenericBlock1.Width + 4;

            Elevator1.Add(new ElevatorFloor(new Point(X,  FirstFloorY), ElevatorFloor.FloorType.GoingDown));
            Elevator1.Add(new ElevatorFloor(new Point(X, SecondFloorY), ElevatorFloor.FloorType.GoingDown));
            Elevator1.Add(new ElevatorFloor(new Point(X, ThirdFloorY), ElevatorFloor.FloorType.GoingDown));
            Elevator1.Add(new ElevatorFloor(new Point(X, FourthFloorY), ElevatorFloor.FloorType.GoingDown));

            foreach (ElevatorFloor floor in Elevator1)
            {
                Boundaries.Add(floor.Bounds);
            }
        }

        public void InitializeElevator2()
        {
            Elevator2 = new List<ElevatorFloor>();
            int size = ElevatorFloor.LowerLimit + 1 - ElevatorFloor.UpperLimit;

            int FirstFloorY = RandomFloorGenerator.Next(ElevatorFloor.UpperLimit, ElevatorFloor.LowerLimit + 1);

            int SecondFloorY, ThirdFloorY, FourthFloorY;
            SecondFloorY = (FirstFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            ThirdFloorY = (SecondFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            FourthFloorY = (ThirdFloorY + Pole.Height + 3 * ElevatorFloor.MovingFloor.Height + GenericBlock1.Height + 2) % (size);
            int X = 11 * GenericBlock1.Width + 4;

            Elevator2.Add(new ElevatorFloor(new Point(X, FirstFloorY), ElevatorFloor.FloorType.GoingUp));
            Elevator2.Add(new ElevatorFloor(new Point(X, SecondFloorY), ElevatorFloor.FloorType.GoingUp));
            Elevator2.Add(new ElevatorFloor(new Point(X, ThirdFloorY), ElevatorFloor.FloorType.GoingUp));
            Elevator2.Add(new ElevatorFloor(new Point(X, FourthFloorY), ElevatorFloor.FloorType.GoingUp));

            foreach (ElevatorFloor floor in Elevator2)
            {
                Boundaries.Add(floor.Bounds);
            }
        }

        public void InitializeElevator3()
        {
            Elevator3 = new List<ElevatorFloor>();
            int X = 4 * GenericBlock1.Width + 6;

            foreach(ElevatorFloor floor in Elevator1)
            {
                Elevator3.Add(new ElevatorFloor(new Point(X, floor.Bounds.Y), ElevatorFloor.FloorType.GoingDown));
            }

            foreach (ElevatorFloor floor in Elevator3)
            {
                Boundaries.Add(floor.Bounds);
            }
        }

        public void InitializeTriggers()
        {
            //UpsideDown triggers
            for (int i = UpsideDownSpikesBounds.X + 6; i < UpsideDownSpikesBounds.X + UpsideDownSpikesBounds.Width; i += GenericBlock1.Width)
            {
                UpsideDownTriggers.Add(new Rectangle(i, UpsideDownSpikesBounds.Y, 8, 24));
            }

            UpsideDownTriggers.Add(new Rectangle(UpsideDownSpikesBounds.X, UpsideDownSpikesBounds.Y, UpsideDownSpikesBounds.Width - 10, UpsideDownSpikesBounds.Height / 4));

            //UpRight triggers
            /*for (int i = UpRightSpikesBounds.X + 11; i < UpRightSpikesBounds.X + UpRightSpikesBounds.Width; i += GenericBlock1.Width + 2)
            {
                UpRightTriggers.Add(new Rectangle(i, UpRightSpikesBounds.Y, 10, 24));
            }*/

            for (int i = UpRightSpikesBounds.X; i < UpRightSpikesBounds.Right; i += GenericBlock1.Width)
            {
                UpRightTriggers.Add(new Rectangle(i, UpRightSpikesBounds.Y, 10, 24));
            }

            UpRightTriggers.Add(new Rectangle(UpRightSpikesBounds.X, UpRightSpikesBounds.Bottom - 8, UpRightSpikesBounds.Width - 2, UpRightSpikesBounds.Height / 4));
        }

        private void InitializePoles()
        {
            Poles.Add(new Rectangle(5 * GenericBlock1.Width + 10, GenericBlock1.Height, Pole.Width, FormHeight - 2 * GenericBlock1.Height));
            Poles.Add(new Rectangle(12 * GenericBlock1.Width + 8, GenericBlock1.Height, Pole.Width, FormHeight - 2 * GenericBlock1.Height));
            Poles.Add(new Rectangle(19 * GenericBlock1.Width + 6, GenericBlock1.Height, Pole.Width, FormHeight - 2 * GenericBlock1.Height));

        }

        private void DrawPoles(Graphics g)
        {
            foreach(Rectangle pole in Poles)
            {
                g.DrawImage(Pole, pole);
            }
        }

        private void DrawBottom(Graphics g)
        {
            for (int i = 0; i < Boundaries[1].Width; i += GenericBlock1.Width)
            {
               g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[1].X, Boundaries[1].Y, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawCelling(Graphics g)
        {
            for (int i = 0; i < Boundaries[0].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[0].X, Boundaries[0].Y, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawLeftWall(Graphics g)
        {
            for (int i = GenericBlock1.Height; i < Boundaries[6].Height + GenericBlock1.Height; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[6].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 8 * GenericBlock1.Height; i < Boundaries[8].Height + 8 * GenericBlock1.Height; i += GenericBlock1.Height)
            {
               g.DrawImage(GenericBlock1, new Rectangle(Boundaries[8].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            g.DrawImage(GenericBlock1, new Rectangle(Boundaries[7].X, Boundaries[7].Y, GenericBlock1.Width, GenericBlock1.Height));
        }

        private void DrawRightWall(Graphics g)
        {
            g.DrawImage(GenericBlock1, new Point(Boundaries[5].X, Boundaries[5].Y));

            for (int i = 0; i < Boundaries[3].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlock2, new Rectangle(i + Boundaries[3].X, Boundaries[3].Y, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 9 * GenericBlock1.Height; i < Boundaries[2].Height + 9 * GenericBlock1.Height; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[2].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = GenericBlock1.Height; i < Boundaries[4].Height + GenericBlock1.Height; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[4].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawUpsideDownSpikes(Graphics g)
        {
            g.DrawImage(UpsideDownSpikes, UpsideDownSpikesBounds);
        }

        private void DrawUpRightSpikes(Graphics g)
        {
            g.DrawImage(UpRightSpikes, UpRightSpikesBounds);
        }

        public override void Draw(Graphics g)
        {
            DrawPoles(g);
            DrawRightWall(g);
            DrawLeftWall(g);
            DrawBottom(g);
            DrawCelling(g);
            DrawUpsideDownSpikes(g);
            DrawUpRightSpikes(g);

            foreach(ElevatorFloor floor in Elevator1)
            {
                floor.Draw(g);
            }

            foreach (ElevatorFloor floor in Elevator3)
            {
                floor.Draw(g);
            }

            foreach (ElevatorFloor floor in Elevator2)
            {
                floor.Draw(g);
            }

            Hero.Draw(g);

            g.DrawRectangles(new Pen(new SolidBrush(Color.Red)), UpsideDownTriggers.ToArray());

            g.DrawRectangles(new Pen(new SolidBrush(Color.Red)), UpRightTriggers.ToArray());

        }

        public new void Update(bool[] arrows, bool space)
        {

            foreach (ElevatorFloor floor in Elevator1)
            {
                floor.Move();
            }
            foreach(ElevatorFloor floor in Elevator3)
            {
                floor.Move();
            }

            foreach(ElevatorFloor floor in Elevator2)
            {
                floor.Move();
            }

            UpdateElevatorBounds();
            base.Update(arrows, space);
        }

        private void UpdateElevatorBounds()
        {
            int counter = 0; 
            for (int i = 9; i < Boundaries.Count; i++)
            {
                if (counter <= 3) // elevator 1
                {
                    Boundaries[i] = Elevator1[counter].Bounds;
                }
                else if (counter >= 4 && counter <= 7) // elevator 2
                {
                    Boundaries[i] = Elevator2[counter - 4].Bounds;
                }
                else // elevator 3
                {
                    Boundaries[i] = Elevator3[counter - 8].Bounds;
                }
                ++counter;
            }
        }
    }
}