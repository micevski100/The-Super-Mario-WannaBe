using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Level6 : Level
    {
        public static readonly int FormHeight = Level5.FormHeight + 1;
        public static readonly int FormWidth = Level5.FormWidth;
        public static readonly Image GenericBlock1 = Level2.GenericBlock1;
        public static readonly Image GenericBlockGrass = Level2.GenericBlockGrass;
        public static readonly Image GenericTree = Level2.GenericTree;
        public static readonly Image UprightSpike = Properties.Resources.UprightSpikeSingle;
        public static readonly Image Cloud = Level2.GenericCloud;

        public List<Rectangle> Trees { get; set; }
        public List<Rectangle> Clouds { get; set; }
        public List<Rectangle> StaticSpikes { get; set; }
        public List<Apple> Apples { get; set; }

        public Level6(Hero hero)
        {
            this.Hero = hero;
            this.Hero.Character = new RectangleF(10, 12 * GenericBlock1.Height, this.Hero.Character.Width, this.Hero.Character.Height);
            InitializeBoundaries();
            InitializeTrees();
            InitializeClouds();
            InitializeStaticSpikes();
            InitializeApples();
        }

        private void InitializeApples()
        {
            Apples = new List<Apple>();

            Apples.Add(new Apple(4 * GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[0].TriggerArea = new Rectangle(Apples[0].Bounds.X, 7 * GenericBlock1.Height, Apples[0].Bounds.Width, 4 * GenericBlock1.Height);

            Apples.Add(new Apple(10 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[1].TriggerArea = new Rectangle(Apples[1].Bounds.X + 10, 8 * GenericBlock1.Height, Apples[1].Bounds.Width, 5 * GenericBlock1.Height);

            Apples.Add(new Apple(11 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, Rectangle.Empty, Apple.TypeOfApple.Flying));
            Apples[2].TriggerArea = new Rectangle(Apples[2].Bounds.X - 10, 8 * GenericBlock1.Height, Apples[2].Bounds.Width, 5 * GenericBlock1.Height);
        }

        private void InitializeStaticSpikes()
        {
            StaticSpikes = new List<Rectangle>();

            for (int i = 2 * GenericBlock1.Width + 8; i < 13 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                StaticSpikes.Add(new Rectangle(i, FormHeight - 3 * GenericBlock1.Height + 5, GenericBlock1.Width / 2, GenericBlock1.Height - 5));
            }

            StaticSpikes.Add(new Rectangle(2 * GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height - (GenericBlock1.Width / 4), 11 * GenericBlock1.Width, GenericBlock1.Height / 4));

        }

        private void InitializeClouds()
        {
            Clouds = new List<Rectangle>();

            Clouds.Add(new Rectangle(7 * GenericBlock1.Width, 2 * GenericBlock1.Height, Cloud.Width, Cloud.Height));
            Clouds.Add(new Rectangle(13 * GenericBlock1.Width, 5 * GenericBlock1.Height, Cloud.Width, Cloud.Height));
        }

        private void InitializeTrees()
        {
            Trees = new List<Rectangle>();

            Trees.Add(new Rectangle(2 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height + 13, GenericTree.Width, GenericTree.Height));
            Trees.Add(new Rectangle(9 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height + 13, GenericTree.Width, GenericTree.Height));
        }

        private void InitializeBoundaries()
        {
            Boundaries = new List<Rectangle>();

            //adding "unbreakable blocks"
            //2 bottom most layers
            Boundaries.Add(new Rectangle(0, FormHeight - 2 * GenericBlock1.Height, FormWidth, 2 * GenericBlock1.Height));

            //3rd and 4th right layers
            Boundaries.Add(new Rectangle(13 * GenericBlock1.Width, FormHeight - 4 * GenericBlock1.Height, FormWidth - 13 * GenericBlock1.Width, 2 * GenericBlock1.Height));

            //3rd and 4th left layers
            Boundaries.Add(new Rectangle(0, FormHeight - 4 * GenericBlock1.Height, 2 * GenericBlock1.Width, 2 * GenericBlock1.Height));

            //single block above 3rd and 4th left layers
            Boundaries.Add(new Rectangle(0, FormHeight - 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            //5th layer
            Boundaries.Add(new Rectangle(14 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, FormWidth - 14 * GenericBlock1.Width, GenericBlock1.Height));

            //6th layer left
            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height));

            //6th layer right
            Boundaries.Add(new Rectangle(21 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, 4 * GenericBlock1.Width, GenericBlock1.Height));

            //
            //adding "breakable blocks" (7th index / [7]), starting left to right
            Boundaries.Add(new Rectangle(0, Boundaries[3].Top - GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            for (int i = Boundaries[3].Top; i > Boundaries[3].Top - 5 * GenericBlock1.Height; i -= GenericBlock1.Height)
            {
                Boundaries.Add(new Rectangle(Boundaries[2].X + GenericBlock1.Width, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            Boundaries.Add(new Rectangle(Boundaries[Boundaries.Count - 1].X, Boundaries[Boundaries.Count - 1].Y - 3 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            for (int i = 4 * GenericBlock1.Width; i < 6 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 4 * GenericBlock1.Width; i < 6 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 15 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 8 * GenericBlock1.Width; i < 10 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            Boundaries.Add(new Rectangle(13 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            for (int i = 14 * GenericBlock1.Width; i < 17 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            Boundaries.Add(new Rectangle(Boundaries[Boundaries.Count - 1].X + 4 * GenericBlock1.Width, Boundaries[Boundaries.Count - 1].Y, GenericBlock1.Width, GenericBlock1.Height));

            for (int i = 17 * GenericBlock1.Width; i < FormWidth; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 7 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 18 * GenericBlock1.Width; i < FormWidth; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 21 * GenericBlock1.Width; i < FormWidth; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 21 * GenericBlock1.Width; i < FormWidth; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 10 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 22 * GenericBlock1.Width; i < FormWidth; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, FormHeight - 11 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            //
            for (int i = 0; i < 8 * GenericBlock1.Height; i += GenericBlock1.Height)
            {
                Boundaries.Add(new Rectangle(24 * GenericBlock1.Width, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 21 * GenericBlock1.Width; i < FormWidth - GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 20 * GenericBlock1.Width; i < FormWidth - GenericBlock1.Width; i+= GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, 2 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 15 * GenericBlock1.Width; i < 19 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, 7 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 16 * GenericBlock1.Width; i < 18 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                Boundaries.Add(new Rectangle(i, 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }

            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
        }

        private void DrawBreakableBlocks(Graphics g)
        {
            g.DrawImage(GenericBlockGrass, Boundaries[7]);

            for (int i = 8; i < 12; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            for (int i = 12; i < 24; i++)
            {
                g.DrawImage(GenericBlockGrass, Boundaries[i]);
            }

            g.DrawImage(GenericBlock1, Boundaries[24]);

            g.DrawImage(GenericBlockGrass, Boundaries[25]);

            for (int i = 26; i < 33; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            for (int i = 33; i < 36; i++)
            {
                g.DrawImage(GenericBlockGrass, Boundaries[i]);
            }

            for (int i = 36; i < 44; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            g.DrawImage(GenericBlockGrass, Boundaries[44]);

            for (int i = 45; i < 48; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            for (int i = 48; i < 50; i++)
            {
                g.DrawImage(GenericBlockGrass, Boundaries[i]);
            }

            g.DrawImage(GenericBlock1, Boundaries[50]);

            g.DrawImage(GenericBlockGrass, Boundaries[51]);

            for (int i = 52; i < 58; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            for (int i = 58; i < 63; i++)
            {
                g.DrawImage(GenericBlockGrass, Boundaries[i]);
            }

            for (int i = 63; i < 66; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            g.DrawImage(GenericBlockGrass, Boundaries[66]);

            for (int i = 67; i < 69; i++)
            {
                g.DrawImage(GenericBlock1, Boundaries[i]);
            }

            for (int i = 69; i < 71; i++)
            {
                g.DrawImage(GenericBlockGrass, Boundaries[i]);
            }

            g.DrawImage(GenericBlock1, Boundaries[71]);

            g.DrawImage(GenericBlockGrass, Boundaries[72]);
        }

        private void DrawSolidBlocks(Graphics g)
        {
            for (int z = 0; z < 7; z++)
            {
                for (int i = Boundaries[z].X; i < Boundaries[z].Width + Boundaries[z].X; i += GenericBlock1.Width)
                {
                    for (int j = Boundaries[z].Y; j < Boundaries[z].Height + Boundaries[z].Y; j += GenericBlock1.Height)
                    {
                        g.DrawImage(GenericBlock1, new Rectangle(i, j, GenericBlock1.Width, GenericBlock1.Height));
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
            foreach (Rectangle cloud in Clouds)
            {
                g.DrawImage(Cloud, cloud);
            }
        }

        // fix image (delete white pixels)
        private void DrawStaticSpikes(Graphics g)
        {
            for (int i = 2 * GenericBlock1.Width; i < 13 * GenericBlock1.Width; i += GenericBlock1.Width)
            {
                g.DrawImage(UprightSpike, new Rectangle(i, FormHeight - 3 * GenericBlock1.Height + 1, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawBackground(Graphics g)
        {
            DrawClouds(g);
            DrawTrees(g);
            DrawSolidBlocks(g);
            DrawBreakableBlocks(g);
            DrawStaticSpikes(g);
            DrawApples(g);
        }

        private void DrawApples(Graphics g)
        {
            foreach (Apple apple in Apples)
            {
                apple.Draw(g);
            }
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangles(new SolidBrush(Color.Black), Boundaries.ToArray());
            DrawBackground(g);
            Hero.Draw(g);
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
            base.Update(arrows, space);
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
