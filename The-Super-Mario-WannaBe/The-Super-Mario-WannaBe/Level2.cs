﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{

    public class Level2 : Level
    {
        public static readonly int FormHeight = 627 - 38;
        public static readonly int FormWidth = 791 - 17;
        public static readonly Image GenericBlock1 = Properties.Resources.generic_block1;
        public static readonly Image GenericBlockGrass = Properties.Resources.generic_block_with_grass;
        public static readonly Image GenericBackground = Properties.Resources.generic_background1;
        public static readonly Image GenericTree = Properties.Resources.tree;
        public static readonly Image GenericCloud = Properties.Resources.cloud;

        private List<Rectangle> Trees { get; set; }
        private List<Rectangle> Clouds { get; set; }
        private List<Apple> Apples { get; set; }
        public List<Rectangle> Triggers { get; set; }

        public Level2(Hero hero)
        {
            Boundaries = new List<Rectangle>();
            InitializeList();
            InitializeTrees();
            InitializeClouds();
            InitializeApples();
            InitializeTriggers();
            Hero = hero;
        }

        private void InitializeTriggers()
        {
            Triggers = new List<Rectangle>();

            Triggers.Add(new Rectangle(Apples[0].Bounds.Left, 0, Apple.GenericApple.Width, Apples[0].Bounds.Top - 2 * GenericBlock1.Height));
            Triggers.Add(new Rectangle(Apples[1].Bounds.Left, Apples[1].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[1].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[2].Bounds.Left - 5, 0, Apple.GenericApple.Width, Apples[2].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[3].Bounds.Right - 14, Apples[3].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[3].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[3].Bounds.Right - 14, Apples[3].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[3].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[5].Bounds.Left, 0, Apple.GenericApple.Width, Apples[3].Bounds.Top - 10));
            Triggers.Add(new Rectangle(Apples[6].Bounds.Right - 15, Apples[6].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[6].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[6].Bounds.Right - 15, Apples[6].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[6].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[8].Bounds.Left, 0, Apple.GenericApple.Width, Apples[6].Bounds.Top - 20));
            Triggers.Add(new Rectangle(Apples[9].Bounds.Left - 5, Apples[9].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[9].Bounds.Top));
            Triggers.Add(new Rectangle(Triggers[9].Right + 2, Triggers[9].Y, Apple.GenericApple.Width, Triggers[9].Height));
            Triggers.Add(new Rectangle(Triggers[9].Right + 2, Triggers[9].Y, Apple.GenericApple.Width, Triggers[9].Height));
            Triggers.Add(new Rectangle(Apples[12].Bounds.Left, 0, Apple.GenericApple.Width, Apples[12].Bounds.Bottom));
            Triggers.Add(new Rectangle(Apples[13].Bounds.Left - 6, Apples[13].Bounds.Top, Apple.GenericApple.Width, FormHeight - Apples[13].Bounds.Top));
            Triggers.Add(new Rectangle(Apples[14].Bounds.Left - 2, 0, Apple.GenericApple.Width, Apples[13].Bounds.Top - 1));

            for (int i = 0; i < Triggers.Count; i++)
            {
                Apples[i].TriggerArea = Triggers[i];
            }
        }

        private void InitializeApples()
        {
            Apples = new List<Apple>();

            // Apple 1
            Apples.Add(new Apple(2 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 2
            Apples.Add(new Apple(8 * GenericBlock1.Width, FormHeight - 4 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 3
            Apples.Add(new Apple(9 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Flying));

            // Apple 4
            Apples.Add(new Apple(11 * GenericBlock1.Width - 8, FormHeight - (int)(4.5 * GenericBlock1.Height), new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 5
            Apples.Add(new Apple(12 * GenericBlock1.Width - 8, FormHeight - (int)(4.5 * GenericBlock1.Height), new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 6
            Apples.Add(new Apple(13 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height + 5, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Flying));

            // Apple 7
            Apples.Add(new Apple(13 * GenericBlock1.Width + 15, FormHeight - 4 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 8
            Apples.Add(new Apple(15 * GenericBlock1.Width - 10, FormHeight - (int)(4.5 * GenericBlock1.Height), new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 9
            Apples.Add(new Apple(16 * GenericBlock1.Width - 15, FormHeight - (int)(4.5 * GenericBlock1.Height) - 5, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Flying));

            // Apple 10
            Apples.Add(new Apple(17 * GenericBlock1.Width - 10, FormHeight - 4 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 11
            Apples.Add(new Apple(18 * GenericBlock1.Width, FormHeight - (int)(4.5 * GenericBlock1.Height) - 5, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 12
            Apples.Add(new Apple(18 * GenericBlock1.Width + 10, FormHeight - 6 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 13
            Apples.Add(new Apple(19 * GenericBlock1.Width + 5, FormHeight - 6 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Flying));

            // Apple 14
            Apples.Add(new Apple(21 * GenericBlock1.Width - 10, FormHeight - 5 * GenericBlock1.Height, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Falling));

            // Apple 15
            Apples.Add(new Apple(23 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height - 12, new Rectangle(0, 0, 10, 10), Apple.TypeOfApple.Flying));
        }

        private void InitializeClouds()
        {
            Clouds = new List<Rectangle>();

            Clouds.Add(new Rectangle(2 * GenericBlock1.Width, 3 * GenericBlock1.Height, GenericCloud.Width, GenericCloud.Height));
            Clouds.Add(new Rectangle(18 * GenericBlock1.Width, 3 * GenericBlock1.Height, GenericCloud.Width, GenericCloud.Height));
            Clouds.Add(new Rectangle(12 * GenericBlock1.Width, 5 * GenericBlock1.Height, GenericCloud.Width, GenericCloud.Height));
        }

        private void InitializeTrees()
        {
            Trees = new List<Rectangle>();

            // leftmost 
            Trees.Add(new Rectangle(GenericBlock1.Width, FormHeight - GenericBlock1.Height - GenericTree.Height, GenericTree.Width, GenericTree.Height));

            for (int i = 8 * GenericBlock1.Width; i < FormWidth - 3 * GenericBlock1.Width; i += 2 * GenericBlock1.Width)
            {
                Trees.Add(new Rectangle(i, FormHeight - GenericBlock1.Height - GenericTree.Height, GenericTree.Width, GenericTree.Height));
            }

            Trees[4] = new Rectangle(Trees[4].X, Trees[4].Y + 12, Trees[4].Width, Trees[4].Height);
            Trees[5] = new Rectangle(Trees[5].X, Trees[5].Y + 6, Trees[5].Width, Trees[5].Height);

            Trees[6] = new Rectangle(Trees[6].X + 10, Trees[6].Y, Trees[6].Width, Trees[6].Height);
            Trees[7] = new Rectangle(Trees[7].X + 10, Trees[7].Y - 5, Trees[7].Width, Trees[7].Height);
        }

        private void InitializeList()
        {
            // Right wall
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, 0, GenericBlock1.Width, FormHeight));

            // Left Wall
            Boundaries.Add(new Rectangle(0, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, 9 * GenericBlock1.Height));

            // Left Ground
            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));

            // Right Ground
            Boundaries.Add(new Rectangle(6 * GenericBlock1.Width, FormHeight - GenericBlock1.Height, 18 * GenericBlock1.Width, GenericBlock1.Height));

            // First Stair
            Boundaries.Add(new Rectangle(21 * GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height));

            // Second Stair
            Boundaries.Add(new Rectangle(22 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));

            // blocks in the air, from right to left
            Boundaries.Add(new Rectangle(23 * GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(20 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(12 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(9 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height));
            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
        }

        private void DrawClouds(Graphics g)
        {
            foreach (Rectangle cloud in Clouds)
            {
                g.DrawImage(GenericCloud, cloud);
            }
        }

        private void DrawTrees(Graphics g)
        {
            g.DrawImage(GenericTree, Trees[5]);
            g.DrawImage(GenericTree, Trees[4]);
            g.DrawImage(GenericTree, Trees[7]);
            g.DrawImage(GenericTree, Trees[6]);

            for (int i = 0; i < 4; i++)
            {
                g.DrawImage(GenericTree, Trees[i]);
            }
        }

        private void DrawRightWall(Graphics g)
        {
            for (int i = 0; i < FormHeight; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[0].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawLeftWall(Graphics g)
        {
            for (int i = 10 * GenericBlock1.Height; i < Boundaries[1].Height + 10 * GenericBlock1.Height; i += GenericBlock1.Height)
            {
                if (i == 10 * GenericBlock1.Height)
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(Boundaries[1].X, i, GenericBlock1.Width, GenericBlock1.Height));
                }
                else
                {
                    g.DrawImage(GenericBlock1, new Rectangle(Boundaries[1].X, i, GenericBlock1.Width, GenericBlock1.Height));
                }
            }
        }

        private void DrawLeftGround(Graphics g)
        {
            for (int i = 0; i < Boundaries[2].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[2].X, Boundaries[2].Y, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawRightGround(Graphics g)
        {
            for (int i = 0; i < Boundaries[3].Width; i += GenericBlock1.Width)
            {
                if (i >= 15 * GenericBlock1.Width)
                {
                    g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[3].X, Boundaries[3].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
                else
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[3].X, Boundaries[3].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }
        }

        private void DrawFirstStair(Graphics g)
        {
            for (int i = 0; i < Boundaries[4].Width; i += GenericBlock1.Width)
            {
                if (i == 0)
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[4].X, Boundaries[4].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
                else
                {
                    g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[4].X, Boundaries[4].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }
        }

        private void DrawSecondStair(Graphics g)
        {
            for (int i = 0; i < Boundaries[5].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[5].X, Boundaries[5].Y, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawBlocksInAir(Graphics g)
        {
            // from right to left
            for (int j = 6; j < Boundaries.Count - 1; j++)
            {
                for (int i = 0; i < Boundaries[j].Width; i += GenericBlock1.Width)
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[j].X, Boundaries[j].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }

            // the last block, right underneath the last block that's in the air
            g.DrawImage(GenericBlock1, new Rectangle(Boundaries[Boundaries.Count - 1].X, Boundaries[Boundaries.Count - 1].Y, GenericBlock1.Width, GenericBlock1.Height));
        }

        private void DrawBackground(Graphics g)
        {
            // background "between" the ground
            Rectangle BackgroundStripe = new Rectangle(3 * GenericBlock1.Width, FormHeight - GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height);
            for (int i = FormHeight - GenericBlock1.Height; i < FormHeight; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(BackgroundStripe.X, i, 3 * GenericBlock1.Width, GenericBackground.Height));
            }
        }

        private void DrawApples(Graphics g)
        {
            foreach (Apple apple in Apples)
            {
                apple.Draw(g);
            }
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

        public override void Update(bool[] arrows, bool space)
        {
            base.Update(arrows, space);

            DeleteUnnecessaryApples();
            foreach(Apple apple in Apples)
            {
                apple.CheckCollision(Hero);
                apple.Update(Hero);
            }

        }

        public override int ChangeLevel()
        {
            if (Hero.Character.Y > FormHeight)
            {
                this.Hero.Character = new RectangleF(this.Hero.Character.X, 1, this.Hero.Character.Width, this.Hero.Character.Height);
                return 1;
            }
            else if(Hero.Character.X < 0)
            {
                this.Hero.Character = new RectangleF(FormWidth - GenericBlock1.Width, 6 * GenericBlock1.Height, Hero.Character.Width, Hero.Character.Height);
                return 3;
            }

            return -1;
        }

        public override void Draw(Graphics g)
        {
            DrawClouds(g);
            DrawTrees(g);
            DrawRightWall(g);
            DrawLeftWall(g);
            DrawLeftGround(g);
            DrawRightGround(g);
            DrawFirstStair(g);
            DrawSecondStair(g);
            DrawBlocksInAir(g);
            DrawBackground(g);
            DrawApples(g);
            Hero.Draw(g);
        }
    }
}
