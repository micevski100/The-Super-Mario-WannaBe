using System;
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

        public Level2(Hero hero)
        {
            Boundaries = new List<Rectangle>();
            InitializeList();
            InitializeTrees();
            InitializeClouds();
            this.Hero = hero;
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
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, 0, GenericBlock1.Width, FormHeight)); // right wall

            Boundaries.Add(new Rectangle(0, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, 9 * GenericBlock1.Height)); // left wall

            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // left ground

            Boundaries.Add(new Rectangle(6 * GenericBlock1.Width, FormHeight - GenericBlock1.Height, 18 * GenericBlock1.Width, GenericBlock1.Height)); // right ground

            Boundaries.Add(new Rectangle(21 * GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height)); // first stair

            Boundaries.Add(new Rectangle(22 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // second stair

            // blocks in the air, from right to left
            Boundaries.Add(new Rectangle(23 * GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(20 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(12 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(9 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - 7 * GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height));

            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
        }

        public override void Draw(Graphics g)
        {
            // drawing clouds first
            foreach(Rectangle cloud in Clouds)
            {
                g.DrawImage(GenericCloud, cloud);
            }

            // drawing trees second
            g.DrawImage(GenericTree, Trees[5]);
            g.DrawImage(GenericTree, Trees[4]);
            g.DrawImage(GenericTree, Trees[7]);
            g.DrawImage(GenericTree, Trees[6]);

            for (int i = 0; i < 4; i++)
            {
                g.DrawImage(GenericTree, Trees[i]);
            }

            // right wall
            for (int i = 0; i < FormHeight; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[0].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }

            // left wall
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


            // left ground
            for (int i = 0; i < Boundaries[2].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[2].X, Boundaries[2].Y, GenericBlock1.Width, GenericBlock1.Height));
            }

            // right ground
            for (int i = 0; i < Boundaries[3].Width; i += GenericBlock1.Width)
            {
                if ( i >= 15 * GenericBlock1.Width)
                {
                    g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[3].X, Boundaries[3].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
                else
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[3].X, Boundaries[3].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }

            // first stair
            for (int i = 0; i < Boundaries[4].Width; i += GenericBlock1.Width)
            {
                if ( i == 0)
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[4].X, Boundaries[4].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
                else
                {
                    g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[4].X, Boundaries[4].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }

            // second stair
            for (int i = 0; i < Boundaries[5].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[5].X, Boundaries[5].Y, GenericBlock1.Width, GenericBlock1.Height));
            }

            // blocks in the air, from right to left
            for (int j = 6; j < Boundaries.Count - 1; j++)
            {
                for (int i = 0; i < Boundaries[j].Width; i += GenericBlock1.Width)
                {
                    g.DrawImage(GenericBlockGrass, new Rectangle(i + Boundaries[j].X, Boundaries[j].Y, GenericBlock1.Width, GenericBlock1.Height));
                }
            }

            // the last block, right underneath the last block that's in the air
            g.DrawImage(GenericBlock1, new Rectangle(Boundaries[Boundaries.Count-1].X, Boundaries[Boundaries.Count - 1].Y, GenericBlock1.Width, GenericBlock1.Height));

            // background "between" the ground
            Rectangle BackgroundStripe = new Rectangle(3 * GenericBlock1.Width, FormHeight - GenericBlock1.Height, 3 * GenericBlock1.Width, GenericBlock1.Height);
            for (int i = FormHeight - GenericBlock1.Height; i < FormHeight; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(BackgroundStripe.X, i, 3 * GenericBlock1.Width, GenericBackground.Height));
            }
        }
    }
}
