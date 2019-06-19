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
        public static readonly Image GenericBlock1 = Level2.GenericBlock1;
        public static readonly Image GenericBlockGrass = Level2.GenericBlockGrass;
        public static readonly Image GenericTree = Level2.GenericTree;
        public static readonly Image UprightSpike = Properties.Resources.UprightSpikeSingle;
        public static readonly Image UpsideDownSpike = Properties.Resources.SingleSpikeUpsideDown;
        public static readonly Image Water = Properties.Resources.Water;

        public static readonly int FormHeight = Level1.FormHeight + 2;
        public static readonly int FormWidth = Level1.FormWidth;

        public List<Apple> Apples { get; set; }

        public Level5(Hero hero)
        {
            this.Hero = hero;
            InitializeBoundaries();

        }

        public void InitializeBoundaries()
        {
            Boundaries = new List<Rectangle>();

            //rows are drawn from bottom to top
            Boundaries.Add(new Rectangle(0, FormHeight - GenericBlock1.Height, FormWidth, GenericBlock1.Height)); //bottom-most row
            Boundaries.Add(new Rectangle(0, FormHeight - 2 * GenericBlock1.Height, 12 * GenericBlock1.Width, GenericBlock1.Height)); //2nd row
            Boundaries.Add(new Rectangle(14 * GenericBlock1.Width, FormHeight - 2 * GenericBlock1.Height, 11 * GenericBlock1.Width, GenericBlock1.Height)); //3rd row

            Boundaries.Add(new Rectangle(0, FormHeight - 3 * GenericBlock1.Height, 12 * GenericBlock1.Width, GenericBlock1.Height)); //2nd row
            Boundaries.Add(new Rectangle(0, FormHeight - 3 * GenericBlock1.Height, 12 * GenericBlock1.Width, GenericBlock1.Height)); //2nd row
            Boundaries.Add(new Rectangle(14 * GenericBlock1.Width, FormHeight - 3 * GenericBlock1.Height, 11 * GenericBlock1.Width, GenericBlock1.Height)); //3rd row
            Boundaries.Add(new Rectangle(0, FormHeight - 4 * GenericBlock1.Height, 7 * GenericBlock1.Width, GenericBlock1.Height)); //4th row



            Boundaries.Add(new Rectangle(0, FormHeight - 5 * GenericBlock1.Height, 7 * GenericBlock1.Width, GenericBlock1.Height)); //5th row
            Boundaries.Add(new Rectangle(8 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, 3 * GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(0, FormHeight - 6 * GenericBlock1.Height, 6 * GenericBlock1.Width, GenericBlock1.Height)); //6th row
            Boundaries.Add(new Rectangle(0, FormHeight - 7 * GenericBlock1.Height, 5 * GenericBlock1.Width, GenericBlock1.Height)); //4th row

            // big block of bullshit - for refactoring 
            Boundaries.Add(new Rectangle(0, 0, GenericBlock1.Width, 6 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(GenericBlock1.Width, 0, GenericBlock1.Width, 5 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(2 * GenericBlock1.Width, 0, GenericBlock1.Width, 2 * GenericBlock1.Height));
            Boundaries.Add(new Rectangle(3 * GenericBlock1.Width, 0, GenericBlock1.Width, GenericBlock1.Height));

            // floating block
            Boundaries.Add(new Rectangle(10 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width * 2, GenericBlock1.Height)); // side block

            // middle tower
            Boundaries.Add(new Rectangle(16 * GenericBlock1.Width, FormHeight - 8 * GenericBlock1.Height, GenericBlock1.Width * 2, 7 * GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(17 * GenericBlock1.Width, FormHeight - 9 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(18 * GenericBlock1.Width, FormHeight - 4 * GenericBlock1.Height, GenericBlock1.Width * 7, GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(20 * GenericBlock1.Width, FormHeight - 5 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height)); // side block
            Boundaries.Add(new Rectangle(24 * GenericBlock1.Width, FormHeight - 6 * GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height * 2)); // side block

        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangles(new Pen(new SolidBrush(Color.Yellow)), Boundaries.ToArray());
            g.FillRectangles(new SolidBrush(Color.Black), Boundaries.ToArray());
        }
    }
}
