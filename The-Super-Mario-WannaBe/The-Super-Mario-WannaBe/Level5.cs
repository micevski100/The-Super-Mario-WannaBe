using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Level5 : Level
    {
        public class Lightning
        {
            public static readonly Image GenericLightning = Properties.Resources.Lightning;
            public Rectangle Bounds { get; set; }
            public Rectangle Trigger { get; set; }
            public bool IsActive { get; set; }
            public bool InForm { get; set; }

            public Lightning(int x, int y)
            {
                Bounds = new Rectangle(x, y, GenericLightning.Width / 2, GenericLightning.Height / 2);
                this.Trigger = Trigger;
                IsActive = false;
                InForm = true;
            }

            private void CheckIfInForm()
            {
                if (IsActive && Bounds.Top > FormHeight)
                {
                    InForm = false;
                }
            }

            private void CheckTriggerArea(Hero hero)
            {
                if (hero.Character.IntersectsWith(Trigger))
                {
                    IsActive = true;
                }
            }

            private void CheckHeroCollision(Hero hero)
            {
                if (IsActive && hero.Character.IntersectsWith(Bounds))
                {
                    hero.Dead = true;
                }
            }

            public void Draw(Graphics g)
            {
                if (IsActive && InForm)
                {
                    g.DrawImage(GenericLightning, Bounds);
                }
            }

            public void Update(Hero hero)
            {
                if (InForm)
                {
                    CheckTriggerArea(hero);
                    MoveDown();
                    CheckHeroCollision(hero);
                    CheckIfInForm();
                }
            }

            private void MoveDown()
            {
                if (IsActive)
                {
                    Bounds = new Rectangle(Bounds.X, Bounds.Y + 6, Bounds.Width, Bounds.Height);
                }
            }
        }

        public static readonly Image GenericBlock1 = Level2.GenericBlock1;
        public static readonly Image GenericBlockGrass = Level2.GenericBlockGrass;
        public static readonly Image GenericTree = Level2.GenericTree;
        public static readonly Image UprightSpike = Properties.Resources.UprightSpikeSingle;
        public static readonly Image UpsideDownSpike = Properties.Resources.SingleSpikeUpsideDown;
        public static readonly Image Water = Properties.Resources.Water;
        public static readonly Image Cloud = Level2.GenericCloud;
        public static readonly int FormHeight = Level1.FormHeight + 2;
        public static readonly int FormWidth = Level1.FormWidth;


        public List<Apple> Apples { get; set; }
        public List<Rectangle> Trees { get; set; }
        public List<FallingSpike> FallingSpikes { get; set; }
        public List<Rectangle> StaticSpikes { get; set; }
        public Lightning lightning;

        public Level5(Hero hero)
        {
            this.Hero = hero;
            InitializeBoundaries();
            InitializeTrees();
            InitializeSpikes();
            InitializeApples();

            lightning = new Lightning(11 * GenericBlock1.Width + 5, 5 * GenericBlock1.Height);
            lightning.Trigger = new Rectangle(lightning.Bounds.X + 10, lightning.Bounds.Y + 15, lightning.Bounds.Width / 3, 5 * GenericBlock1.Height + 15);

            this.jumpSize = 35;
            this.Hero.Character = new RectangleF(GenericBlock1.Width, 8 * GenericBlock1.Height, this.Hero.Character.Width, this.Hero.Character.Height);

        }

        private void InitializeApples()
        {
            Apples = new List<Apple>();

            Apples.Add(new Apple(15 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height + 7, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[0].TriggerArea = new Rectangle(Apples[0].Bounds.X - 5, 0, Apple.GenericApple.Width + 5, Apples[0].Bounds.Bottom - 5);
            
            Apples.Add(new Apple(19 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[1].TriggerArea = new Rectangle(Apples[1].Bounds.X, 0, Apple.GenericApple.Width, Apples[1].Bounds.Top);
            
            Apples.Add(new Apple(20 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[2].TriggerArea = new Rectangle(Apples[2].Bounds.X, 4 * GenericBlock1.Height, Apple.GenericApple.Width, Apples[2].Bounds.Top);
            
            Apples.Add(new Apple(10 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[3].TriggerArea = new Rectangle(Apples[3].Bounds.X + 15, 0, Apple.GenericApple.Width / 2, Apples[3].Bounds.Top);
        }

        private void DrawApples(Graphics g)
        {
            foreach(Apple apple in Apples)
            {
                apple.Draw(g);
            }
        }

        private void DrawStaticSpikes(Graphics g)
        {
            g.DrawImage(UprightSpike, new Rectangle(9 * GenericBlock1.Width, FormHeight - 4 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            g.DrawImage(UprightSpike, new Rectangle(10 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height + 1, GenericBlock1.Width, GenericBlock1.Height));
        }

        private void InitializeSpikes()
        {
            // public FallingSpike(int x, int y, Rectangle Trigger, Type type)
            //falling spikes
            FallingSpikes = new List<FallingSpike>();

            Rectangle trigger = new Rectangle(8 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height / 3);
            FallingSpikes.Add(new FallingSpike(10 * GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, trigger, FallingSpike.Type.GoingDown));
            FallingSpikes.Add(new FallingSpike(11 * GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, trigger, FallingSpike.Type.GoingDown));

            //static
            StaticSpikes = new List<Rectangle>();

            // first spike
            StaticSpikes.Add(new Rectangle(9 * GenericBlock1.Width + 7, FormHeight - 4 * GenericBlock1.Height + 7, GenericBlock1.Width / 4 + 10, GenericBlock1.Height - 4));
            StaticSpikes.Add(new Rectangle(9 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height - 7, GenericBlock1.Width, GenericBlock1.Height / 4));
            
            // second spike
            StaticSpikes.Add(new Rectangle(10 * GenericBlock1.Width + 7, FormHeight - 9 * GenericBlock1.Height + 7, GenericBlock1.Width / 4 + 10, GenericBlock1.Height - 4));
            StaticSpikes.Add(new Rectangle(10 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height - 7, GenericBlock1.Width, GenericBlock1.Height / 4));
        }

        private void InitializeTrees()
        {
            Trees = new List<Rectangle>();

            Trees.Add(new Rectangle(5 * GenericBlock1.Width , 11 * GenericBlock1.Height - 10, GenericTree.Width, GenericTree.Height));
            Trees.Add(new Rectangle(9 * GenericBlock1.Width , 11 * GenericBlock1.Height - 10, GenericTree.Width, GenericTree.Height));
            Trees.Add(new Rectangle(13 * GenericBlock1.Width + 10, 11 * GenericBlock1.Height - 10, GenericTree.Width, GenericTree.Height));
            Trees.Add(new Rectangle(18 * GenericBlock1.Width + 5, 11 * GenericBlock1.Height - 10, GenericTree.Width, GenericTree.Height));
        }

        public void InitializeBoundaries()
        {
            Boundaries = new List<Rectangle>();

            //rows are drawn from bottom to top
            Boundaries.Add(new Rectangle(0, FormHeight - GenericBlock1.Height, FormWidth, GenericBlock1.Height)); //bottom-most row
            Boundaries.Add(new Rectangle(0, FormHeight - 2 * GenericBlock1.Height, 12 * GenericBlock1.Width, GenericBlock1.Height)); //2nd row
            Boundaries.Add(new Rectangle(14 * GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height, 11 * GenericBlock1.Width, GenericBlock1.Height)); //3rd row


            Boundaries.Add(new Rectangle(0, FormHeight - 3 * GenericBlock1.Height, 12 * GenericBlock1.Width, GenericBlock1.Height)); //2nd row
            Boundaries.Add(new Rectangle(14 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height, 11 * GenericBlock1.Width, GenericBlock1.Height)); //3rd row




            Boundaries.Add(new Rectangle(0, FormHeight - 4 * GenericBlock1.Height, 7 * GenericBlock1.Width, GenericBlock1.Height)); //4th row
            Boundaries.Add(new Rectangle(0, FormHeight - 5 * GenericBlock1.Height, 6 * GenericBlock1.Width, GenericBlock1.Height)); //5th row
            Boundaries.Add(new Rectangle(8 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, 3 * GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(0, FormHeight - 6 * GenericBlock1.Height, 6 * GenericBlock1.Width, GenericBlock1.Height)); //6th row
            Boundaries.Add(new Rectangle(0, FormHeight - 7 * GenericBlock1.Height, 5 * GenericBlock1.Width, GenericBlock1.Height)); //4th row
            Boundaries.Add(new Rectangle(0, FormHeight - 8 * GenericBlock1.Height, 4 * GenericBlock1.Width, GenericBlock1.Height)); //4th row

            // big block of bullshit - for refactoring 
            Boundaries.Add(new Rectangle(0, 0, GenericBlock1.Width, 8 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 0, GenericBlock1.Width, 7 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(2 * GenericBlock1.Width, 0, GenericBlock1.Width, 2 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(3 * GenericBlock1.Width, 0, GenericBlock1.Width, GenericBlock1.Height));

            // floating block
            Boundaries.Add(new Rectangle(10 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width * 2, GenericBlock1.Height)); // side block

            // middle tower
            Boundaries.Add(new Rectangle(16 * GenericBlock1.Width, FormHeight - 10 * GenericBlock1.Height, GenericBlock1.Width * 2, 7 * GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, FormHeight - 11 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(18 * GenericBlock1.Width, FormHeight - 4 * GenericBlock1.Height, GenericBlock1.Width * 7, GenericBlock1.Height)); // side block

            Boundaries.Add(new Rectangle(20 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(24 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height * 2)); // side block
            
        }

        private void DrawFirstThreeFloors(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = Boundaries[i].X; j < Boundaries[i].Width + Boundaries[i].X; j += GenericBlock1.Width)
                {
                    Rectangle temp = new Rectangle(j, Boundaries[i].Y, GenericBlock1.Width, GenericBlock1.Height);
                    if( (i == 3 && ( (j == 7 * GenericBlock1.Width + Boundaries[i].X) ||
                                    (j >= 9 * GenericBlock1.Width + Boundaries[i].X)) ) ||
                        (i == 4 && j <= 2 * GenericBlock1.Width + Boundaries[i].X))
                    { 
                        g.DrawImage(GenericBlockGrass, temp);
                    }
                    else
                    {
                        g.DrawImage(GenericBlock1, temp);
                    }
                }
            }
        }

        private void DrawStairs(Graphics g)
        {
            for (int i = 5; i <= 10; i++)
            {
                if ( i == 7)
                {
                    for (int j = Boundaries[i].Y; j < Boundaries[i].Height + Boundaries[i].Y; j += GenericBlock1.Height)
                    {
                        Rectangle temp = new Rectangle(Boundaries[i].X, j, GenericBlock1.Width, GenericBlock1.Height);
                        if (j == Boundaries[i].Y)
                        {
                            g.DrawImage(GenericBlockGrass, temp);
                        }
                        else
                        {
                            g.DrawImage(GenericBlock1, temp);
                        }
                    }
                    continue;
                }

                for (int j = Boundaries[i].X; j < Boundaries[i].Width + Boundaries[i].X; j += GenericBlock1.Width)
                {
                    Rectangle temp = new Rectangle(j, Boundaries[i].Y, GenericBlock1.Width, GenericBlock1.Height);
                    if(i == 10 || (i == 9 && j == 4 * GenericBlock1.Width) || (i == 8 && j == 5 * GenericBlock1.Width) ||
                        (i == 5 && j == 6 * GenericBlock1.Width))
                    {
                        g.DrawImage(GenericBlockGrass, temp);
                    }
                    else
                    {
                        g.DrawImage(GenericBlock1, temp);
                    }
                }
            }
        }

        private void DrawLeftFLoatingBlock(Graphics g)
        {
            for (int i = 11; i < 15; i++)
            {
                for (int j = 0; j < Boundaries[i].Height; j += GenericBlock1.Height)
                {
                    Rectangle temp = new Rectangle(Boundaries[i].X, j, GenericBlock1.Width, GenericBlock1.Height);
                    g.DrawImage(GenericBlock1, temp);
                }
            }
        }
        
        private void DrawFloatingBlock(Graphics g)
        {
            for (int j = Boundaries[15].X; j < Boundaries[15].Width + Boundaries[15].X; j += GenericBlock1.Width)
            {
                Rectangle temp = new Rectangle(j, Boundaries[15].Y, GenericBlock1.Width, GenericBlock1.Height);
                if (j == Boundaries[15].X)
                {
                    g.DrawImage(GenericBlock1, temp);
                }
                else
                {
                    g.DrawImage(GenericBlockGrass, temp);
                }
            }
        }

        private void DrawTower(Graphics g)
        {
            for (int i = Boundaries[16].X; i < Boundaries[16].Width + Boundaries[16].X; i += GenericBlock1.Width)
            {
                for (int j = Boundaries[16].Y; j < Boundaries[16].Height + Boundaries[16].Y; j += GenericBlock1.Height)
                {
                    Rectangle temp = new Rectangle(i, j, GenericBlock1.Width, GenericBlock1.Height);

                    if ( i == Boundaries[16].X && j == Boundaries[16].Y)
                    {
                        g.DrawImage(GenericBlockGrass, temp);
                    }
                    else
                    {
                        g.DrawImage(GenericBlock1, temp);
                    }
                }
            }

            g.DrawImage(GenericBlockGrass, Boundaries[17]);
        }

        private void DrawFloor3(Graphics g)
        { 
            for (int i = Boundaries[18].X; i < Boundaries[18].Width + Boundaries[18].X; i += GenericBlock1.Width)
            {
                Rectangle temp = new Rectangle(i, Boundaries[18].Y, GenericBlock1.Width, GenericBlock1.Height);

                if ( i == 2 * GenericBlock1.Width + Boundaries[18].X || i == 6 * GenericBlock1.Width + Boundaries[18].X)
                {
                    g.DrawImage(GenericBlock1, temp);
                    continue;
                }
                g.DrawImage(GenericBlockGrass, temp);
            }
        }

        private void DrawLastTwoVerticalBlocks(Graphics g)
        {
            for (int i = 19; i < 21; i++)
            {
                for (int j = Boundaries[i].Y; j < Boundaries[i].Height + Boundaries[i].Y; j += GenericBlock1.Height)
                {
                    Rectangle temp = new Rectangle(Boundaries[i].X, j, GenericBlock1.Width, GenericBlock1.Height);
                    if ( j == Boundaries[i].Y)
                    {
                        g.DrawImage(GenericBlockGrass, temp);
                    }
                    else
                    {
                        g.DrawImage(GenericBlock1, temp);
                    }
                }
            }
        }

        private void DrawTrees(Graphics g)
        {
            foreach (Rectangle tree in Trees)
            {
                g.DrawImage(GenericTree, tree);
            }
        }

        private void DrawClouds(Graphics g)
        {
            Rectangle cloud1 = new Rectangle(6 * GenericBlock1.Width, 2 * GenericBlock1.Height, Cloud.Width, Cloud.Height);
            Rectangle cloud2 = new Rectangle(11 * GenericBlock1.Width - 12, 5 * GenericBlock1.Height, Cloud.Width, Cloud.Height);

            g.DrawImage(Cloud, cloud1);
            g.DrawImage(Cloud, cloud2);
        }

        private void DrawWatah(Graphics g)
        {
            g.DrawImage(Water, new Rectangle(12 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height, Water.Width, Water.Height));
        }

        private void DrawBackground(Graphics g)
        {
            DrawClouds(g);
            DrawWatah(g);
            DrawFirstThreeFloors(g);
            DrawLeftFLoatingBlock(g);
            DrawFloatingBlock(g);
            DrawStairs(g);
            DrawTower(g);
            DrawFloor3(g);
            DrawLastTwoVerticalBlocks(g);
        }

        // fix spike picture bug
        public override void Draw(Graphics g)
        {
            DrawTrees(g);
            DrawApples(g);
            DrawBackground(g);

            foreach (FallingSpike fallingSpike in FallingSpikes)
            {
                fallingSpike.Draw(g);
            }

            DrawStaticSpikes(g);

            Hero.Draw(g);

            lightning.Draw(g);
        }

        private void DeleteUnnecessaryApples()
        {
            for (int i = 0; i < Apples.Count; i++)
            {
                if (!Apples[i].InForm)
                {
                    Apples.RemoveAt(i);
                    --i;
                }
            }
        }

        public new void Update(bool[] arrows, bool space)
        {
            DeleteUnnecessaryApples();
            foreach (Apple apple in Apples)
            {
                apple.CheckCollision(Hero);
                apple.Update(Hero);
            }

            CheckCollisionWithStaticSpikes();
            UpdateFallingSpikes();
            lightning.Update(Hero);
            base.Update(arrows, space);
        }

        private void UpdateFallingSpikes()
        {
            foreach (FallingSpike fallingSpike in FallingSpikes)
            {
                fallingSpike.Update(this.Hero);
            }
            DeleteUnnecessarySpikes();
        }

        private void DeleteUnnecessarySpikes()
        {
            for (int i = 0; i < FallingSpikes.Count; i++)
            {
                FallingSpikes[i].CheckSpikePosition();
                if (!FallingSpikes[i].InForm)
                {
                    FallingSpikes.RemoveAt(i);
                    --i;
                }
            }
        }

        private void CheckCollisionWithStaticSpikes()
        {
            foreach (Rectangle staticSpike in StaticSpikes)
            {
                if (Hero.Character.IntersectsWith(staticSpike))
                {
                    Hero.Dead = true;
                    break;
                }
            }
        }
    }
}
