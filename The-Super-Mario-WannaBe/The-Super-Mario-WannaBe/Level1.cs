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
            public enum TypeOfSpike { LeftToRight, RightToLeft}

            private static readonly Image LeftToRightSpike = Properties.Resources.left_to_right_spike;
            private static readonly Image RightToLeftSpike = Properties.Resources.right_to_left_spike;

            public Rectangle Bounds { get; set; }
            public Rectangle Start { get; set; }
            public Rectangle Destination { get; set; }
            public int TopBoundary { get; set; }
            public int LowBoundary { get; set; }
            public int VerticalBoundary { get; set; }
            public int Timer { get; set; }
            public TypeOfSpike Type { get; set; }
            public bool IsActive { get; set; }
            public bool ArrivedAtDestination { get; set; }
            public bool ArrivedAtBeginning { get; set; }
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
                Timer = 0;
            }

            private void CheckHeroPosition(Hero Hero)
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

            public void Collision(Hero Hero)
            {
                if (Hero.Character.IntersectsWith(Bounds))
                {
                    Hero.Dead = true;
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
                    Timer = 0;
                }

            }

            public void Spawn(Hero Hero)
            {
                CheckHeroPosition(Hero);
                if (IsActive)
                {
                    Timer++;
                    if( Timer >= 100)
                    {
                        Move();
                    }
                }
                if (Timer >= 100)
                {
                    Deactivate();
                }
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

            public void Draw(Graphics g)
            {
                if (IsActive && Timer >= 100)
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
            Boundaries.Add(new Rectangle(6 * GenericBlock1.Width, 0, 18 * GenericBlock1.Width, GenericBlock1.Height)); // top-right ceiling

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

        public void UpdateSpikes()
        {
            foreach(Spike spike in Spikes)
            {
                spike.Spawn(Hero);
                spike.Collision(Hero);
            }
        }

        public override void Update(bool[] arrows, bool space)
        {
            base.Update(arrows, space);
            UpdateSpikes();
        }
        
        public override int ChangeLevel()
        {
            // Switch to level 2
            if (Hero.Character.Y < 0)
            {
                this.Hero.Character = new RectangleF(this.Hero.Character.X, FormHeight - 2 * GenericBlock1.Height, this.Hero.Character.Width, this.Hero.Character.Height);
                return 2;
            }
            // Switch to level 3
            else if (Hero.Character.Y > 4 * GenericBlock1.Height && Hero.Character.Y < 7 * GenericBlock1.Height && Hero.Character.X < GenericBlock1.Width + 5 && Spikes[0].IsActive)
            {
                this.Hero.Character = new RectangleF(FormWidth - GenericBlock1.Width + 25, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                return 3;
            }
            // Switch to level 4
            else if ( Hero.Character.Y > FormHeight)
            {
                return 4;
            }
            // Switch to level 5
            else if (Hero.Character.Y > 8 * GenericBlock1.Height && Hero.Character.Y < 11 * GenericBlock1.Height && Hero.Character.X > FormWidth - GenericBlock1.Width - 20)
            {
                this.Hero.Character = new RectangleF(0, Hero.Character.Y, Hero.Character.Width, Hero.Character.Height);
                return 5;
            }

            return -1;
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
                if (j <= 1) // vertical rectangles
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
                        if (j < 4) // ceiling
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

            foreach (Spike spike in Spikes)
            {
                spike.Draw(g);
            }

            Hero.Draw(g);
        }
    }
}
