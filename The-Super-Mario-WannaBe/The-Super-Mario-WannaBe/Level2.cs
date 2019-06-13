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
        public static readonly int FormHeight = 627 - 39;
        public static readonly int FormWidth = 791 - 17;
        public static readonly Image GenericBlock1 = Properties.Resources.generic_block1;
        public static readonly Image GenericBlockGrass = Properties.Resources.generic_block_with_grass;
        public static readonly Image GenericBackground = Properties.Resources.generic_background1;

        public Level2(Hero hero)
        {
            Boundaries = new List<Rectangle>();
            InitializeList();
            this.Hero = hero;
        }

        private void InitializeList()
        {
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, 0, GenericBlock1.Width, FormHeight)); // right wall

            Boundaries.Add(new Rectangle(0, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, 9 * GenericBlock1.Height)); // left wall

            Boundaries.Add(new Rectangle(GenericBlock1.Width, FormHeight - GenericBlock1.Height, 2 * GenericBlock1.Width, GenericBlock1.Height)); // left "first" floor

            Boundaries.Add(new Rectangle(6 * GenericBlock1.Width, FormHeight - GenericBlock1.Height, 18 * GenericBlock1.Width, GenericBlock1.Height)); // right "first" floor

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
            g.FillRectangles(new SolidBrush(Color.Black), Boundaries.ToArray());

            for (int i = 0; i < FormHeight; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[0].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }




        }
    }
}
