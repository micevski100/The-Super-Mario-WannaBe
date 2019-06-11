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
        public static readonly Image GenericBlock1 = Properties.Resources.generic_block1;
        public static readonly Image GenericBlock2 = Properties.Resources.generic_block2;
        public static readonly Image GenericBackground = Properties.Resources.generic_background1;
        
        public static readonly int FormHeight = 627 - 42;
        public static readonly int FormWidth = 791 - 17;

        public Level1(Hero hero)
        {
            Boundaries = new List<Rectangle>();
            InitializeList();
            this.Hero = hero;
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

            Hero.Draw(g);
        }

    }
}
