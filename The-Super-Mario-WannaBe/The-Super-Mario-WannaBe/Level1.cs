using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Level1 : Level
    {
        public class Spike
        {
            public enum TypeOfSpike
            {
                LeftToRight,
                RightToLeft
            }

            public Rectangle Bounds { get; set; }
            public Rectangle Start { get; set; }
            public Rectangle Destination { get; set; }
            public bool IsActive { get; set; }
            public int TopBoundary { get; set; }
            public int LowBoundary { get; set; }
            public TypeOfSpike Type { get; set; }
            public bool ArrivedAtDestination { get; set; }
            public bool ArrivedAtBeginning { get; set; }
            private static readonly Image LeftToRightSpike = Properties.Resources.left_to_right_spike;
            private static readonly Image RightToLeftSpike = Properties.Resources.right_to_left_spike;

            public int VerticalBoundary { get; set; }
            public bool Inequality { get; set; }

            public Spike(Rectangle Start, Rectangle End, int TopBoundary, int LowBoundary, TypeOfSpike Type, int VerticalBoundary, bool Inequality)
            {
                Bounds = Start;
                Destination = End;
                IsActive = false;
                this.Start = Start;
                this.TopBoundary = TopBoundary;
                this.LowBoundary = LowBoundary;
                this.Type = Type;
                ArrivedAtDestination = false;
                ArrivedAtBeginning = false;
                this.VerticalBoundary = VerticalBoundary;
                this.Inequality = Inequality;
            }

            private void CheckHeroPosition(Hero Hero) // heroIsInPosition bool
            {
                if (Hero.Character.Y >= TopBoundary && Hero.Character.Y <= LowBoundary)
                {
                    if (Inequality)
                    {
                        if (Hero.Character.X <= VerticalBoundary)
                        {
                            Activate();
                        }
                    }
                    else
                    {
                        if (Hero.Character.X >= VerticalBoundary)
                        {
                            Activate();
                        }
                    }
                }
            }

            private void Activate()
            {
                IsActive = true;
            }

            private void Deactivate()
            {
                if (Bounds.Equals(Start))
                {
                    IsActive = false;
                    ArrivedAtDestination = false;
                }
            }

            public void Spawn(Hero Hero)
            {
                CheckHeroPosition(Hero);
                if (IsActive)
                {
                    Move();
                }
                Deactivate();
            }

            public void Move()
            {
                if (Type.Equals(TypeOfSpike.LeftToRight))
                {
                    if (ArrivedAtDestination)
                    {
                        ReturnToLeft();
                    }

                    if (Bounds.Right < Destination.Right && !ArrivedAtDestination)
                    {
                        MoveRight();
                    }
                    else
                    {
                        ArrivedAtDestination = true;
                    }                   
                }
                else
                {
                    if (ArrivedAtDestination)
                    {
                        ReturnToRight();
                    }

                    if (Bounds.Left > Destination.Left && !ArrivedAtDestination)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        ArrivedAtDestination = true;
                    }
                }
            }

            public void Draw(Graphics g)
            {
                if (IsActive)
                {
                    if (Type == TypeOfSpike.LeftToRight)
                    {
                        g.DrawImage(LeftToRightSpike, Bounds);
                    }
                    else
                    {
                        g.DrawImage(RightToLeftSpike, Bounds);
                    }
                }
            }

            private void MoveLeft()
            {
                int x = Bounds.X - 10;
                int y = Bounds.Y;
                int width = Bounds.Width;
                int height = Bounds.Height;

                Bounds = new Rectangle(x, y, width, height);
            }

            private void MoveRight()
            {
                int x = Bounds.X + 10;
                int y = Bounds.Y;
                int width = Bounds.Width;
                int height = Bounds.Height;

                Bounds = new Rectangle(x, y, width, height);
            }

            private void ReturnToLeft()
            {
                int x = Bounds.X - 1;
                int y = Bounds.Y;
                int width = Bounds.Width;
                int height = Bounds.Height;

                Bounds = new Rectangle(x, y, width, height);
            }

            private void ReturnToRight()
            {
                int x = Bounds.X + 1;
                int y = Bounds.Y;
                int width = Bounds.Width;
                int height = Bounds.Height;

                Bounds = new Rectangle(x, y, width, height);
            }

            public void Collision(Hero Hero)
            {
                if (Bounds.IntersectsWith(Hero.Character))
                    Hero.Dead = true;
            }
        }

        public static readonly int FormHeight = 627 - 42;
        public static readonly int FormWidth = 791 - 17;
        public static readonly Image GenericBlock1 = Properties.Resources.generic_block1;
        public static readonly Image GenericBlock2 = Properties.Resources.generic_block2;
        public static readonly Image GenericBackground = Properties.Resources.generic_background1;
        public List<Spike> Spikes = new List<Spike>();

        public Level1(Hero hero)
        {
            Boundaries = new List<Rectangle>();
            InitializeList();
            InitializeSpikes();
            this.Hero = hero;
        }

        private void InitializeSpikes()
        {
            // First spike
            Spikes.Add(new Spike(new Rectangle(GenericBlock1.Width, 4 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            new Rectangle(22 * GenericBlock1.Width, 4 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            4 * GenericBlock1.Height, 7 * GenericBlock1.Height, Spike.TypeOfSpike.LeftToRight, FormWidth - 3 * GenericBlock1.Width, true));

            // Second spike
            Spikes.Add(new Spike(new Rectangle(24 * GenericBlock1.Width - GenericBlock1.Width / 2, 8 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            new Rectangle(3 * GenericBlock1.Width - GenericBlock1.Width / 2, 8 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            8 * GenericBlock1.Height, 11 * GenericBlock1.Height, Spike.TypeOfSpike.RightToLeft, 3 * GenericBlock1.Width, false));

            // Third spike
            Spikes.Add(new Spike(new Rectangle(24 * GenericBlock1.Width - GenericBlock1.Width / 2, 12 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            new Rectangle(3 * GenericBlock1.Width - GenericBlock1.Width / 2, 8 * GenericBlock1.Height, GenericBlock1.Width / 2, 3 * GenericBlock1.Height),
            12 * GenericBlock1.Height, 15 * GenericBlock1.Height, Spike.TypeOfSpike.RightToLeft, FormWidth - 5 * GenericBlock1.Width, false));
        }

        private void InitializeList()
        {
            // adding walls
            Boundaries.Add(new Rectangle(0, 0, GenericBlock1.Width, FormHeight)); // left wall
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, 0, GenericBlock1.Width, FormHeight)); // right wall

            // adding cellings
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 0, 2 * GenericBlock1.Width, GenericBlock1.Height)); // top-left ceiling
            Boundaries.Add(new Rectangle(7 * GenericBlock1.Width, 0, 17 * GenericBlock1.Width, GenericBlock1.Height)); // top-right ceiling

            // adding first floor
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 3 * GenericBlock1.Height, 17 * GenericBlock1.Width, GenericBlock1.Height));// left-side
            Boundaries.Add(new Rectangle(22 * GenericBlock1.Width, 3 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // right-side

            // adding second floor
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 7 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // left-side
            Boundaries.Add(new Rectangle(7 * GenericBlock1.Width, 7 * GenericBlock1.Height, 17 * GenericBlock1.Width, GenericBlock1.Height)); // right-side

            // adding third floor
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 11 * GenericBlock1.Height, 17 * GenericBlock1.Width, GenericBlock1.Height)); // left-side
            Boundaries.Add(new Rectangle(22 * GenericBlock1.Width, 11 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // right-side

            // adding fourth floor
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 15 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // left-side
            Boundaries.Add(new Rectangle(7 * GenericBlock1.Width, 15 * GenericBlock1.Height, 17 * GenericBlock1.Width, GenericBlock1.Height)); // right-side

        }

        public override void Draw(Graphics g)
        {
            // adding generic background
            Rectangle leftBackground = new Rectangle(3 * GenericBlock1.Width, 0, 4 * GenericBlock1.Width, FormHeight);
            Rectangle rightBackground = new Rectangle(18 * GenericBlock1.Width, 0, 4 * GenericBlock1.Width, FormHeight);

       
            // repeating the same image vertically
            for (int i = 0; i < leftBackground.Height; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(leftBackground.X, i, 4 * GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 0; i < rightBackground.Height; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(rightBackground.X, i, 4 * GenericBlock1.Width, GenericBlock1.Height));
            }

            // iterating floors and cellings
            for (int j = 0; j < Boundaries.Count; j++)
            {
                if ( j <= 1) // vertical rectangles
                {
                    // drawing blocks frames inside 
                    for (int i = 0; i < Boundaries[j].Height; i += GenericBlock1.Height)
                    {
                        g.DrawImage(GenericBlock1, new Rectangle(Boundaries[j].X, i, GenericBlock1.Width, GenericBlock1.Height));
                    }
                }
                else // horizontal rectangles
                {
                    // drawing block frames inside
                    for (int i = 0; i < Boundaries[j].Width; i += GenericBlock1.Width)
                    {
                        if ( j < 4) // ceiling
                        {
                            g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[j].X, Boundaries[j].Y, GenericBlock1.Width, GenericBlock1.Height));
                        }
                        else // floors
                        {
                            g.DrawImage(GenericBlock2, new Rectangle(i + Boundaries[j].X, Boundaries[j].Y, GenericBlock1.Width, GenericBlock1.Height));
                        }
                    }
                }

            }

            foreach(Spike spike in Spikes)
            {
                spike.Draw(g);
            }

            Hero.Draw(g);
        }

        public void UpdateSpikes()
        {
            foreach(Spike spike in Spikes)
            {
                spike.Spawn(Hero);
                spike.Collision(Hero);
            }
        }

        public new void Update(bool[] arrows, bool space)
        {
            base.Update(arrows, space);
            UpdateSpikes();
        }
    }
}
