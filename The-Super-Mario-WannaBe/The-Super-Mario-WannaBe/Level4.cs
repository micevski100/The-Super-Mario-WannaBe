using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    //Impossible level
    // idejata na ati, mission impossible scene
    public class Level4 : Level
    {
        public static readonly Image GenericBlock1 = Level1.GenericBlock1;
        public static readonly Image GenericBackground = Level1.GenericBackground;
        public static readonly Image RightToLeftHorizontalSpike = Properties.Resources.RightToLeftHorizontalSpike;
        public static readonly Image LeftToRightHorizontalSpike = Properties.Resources.LeftToRightHorizontalSpike;
        public static readonly Image UprightSpike = Properties.Resources.UpRightSpikesLevel4;
        public static int FormHeight = Level1.FormHeight + 2;
        public static int FormWidth = Level1.FormWidth;
        public Rectangle leftBackground = new Rectangle(3 * GenericBlock1.Width, 0, 4 * GenericBlock1.Width, FormHeight);
        public Rectangle rightBackground = new Rectangle(18 * GenericBlock1.Width, 0, 4 * GenericBlock1.Width, FormHeight);

        public List<Rectangle> LeftSpikes { get; set; }
        public List<Rectangle> RightSpikes { get; set; }
        public List<Rectangle> BottomSpikes { get; set; }
        public List<Rectangle> Triggers { get; set; }

        public Level4(Hero hero)
        {
            InitializeBoundaries();
            InitializeSpikes();
            InitializeTriggers();
            this.Hero = hero;
            this.Hero.Character = new RectangleF(FormWidth / 2, 0.5f, this.Hero.Character.Width, this.Hero.Character.Height);
        }

        private void InitializeTriggers()
        {
            Triggers = new List<Rectangle>();

            foreach(Rectangle spike in LeftSpikes)
            {
                Triggers.Add(new Rectangle(spike.X, spike.Y + 9, spike.Width - 4, spike.Height / 2 - 4));
            }

            foreach (Rectangle spike in RightSpikes)
            {
                Triggers.Add(new Rectangle(spike.X + 4, spike.Y + 9, spike.Width - 4, spike.Height / 2 - 4));
            }

            foreach (Rectangle spike in BottomSpikes)
            {
                Triggers.Add(new Rectangle(spike.X + 9, spike.Y + 2, spike.Width / 2 - 4, spike.Height - 2));
            }

            Triggers.Add(new Rectangle(GenericBlock1.Width, FormHeight - GenericBlock1.Height - 8, FormWidth - 2 * GenericBlock1.Width, GenericBlock1.Height / 3));
        }

        private void InitializeSpikes()
        {
            InitalizeLeftSpikes();
            InitalizeRightSpikes();
            InitalizeBottomSpikes();
        }

        private void InitalizeBottomSpikes()
        {
            BottomSpikes = new List<Rectangle>();

            for (int i = GenericBlock1.Width; i < Boundaries[2].Width - GenericBlock1.Width; i += GenericBlock1.Width)
            {
                BottomSpikes.Add(new Rectangle(i + Boundaries[2].X, Boundaries[2].Y - GenericBlock1.Height, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void InitalizeRightSpikes()
        {
            RightSpikes = new List<Rectangle>();

            for (int i = 0; i < Boundaries[1].Height - GenericBlock1.Height; i += GenericBlock1.Height)
            {
                RightSpikes.Add(new Rectangle(Boundaries[1].X - GenericBlock1.Width, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void InitalizeLeftSpikes()
        {
            LeftSpikes = new List<Rectangle>();

            for (int i = 0; i < Boundaries[0].Height - GenericBlock1.Height; i += GenericBlock1.Height)
            {
                LeftSpikes.Add(new Rectangle(GenericBlock1.Width, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void InitializeBoundaries()
        {
            Boundaries = new List<Rectangle>();

            Boundaries.Add(new Rectangle(0, 0, GenericBlock1.Width, FormHeight - GenericBlock1.Height)); // left wall
            Boundaries.Add(new Rectangle(FormWidth - GenericBlock1.Width, 0, GenericBlock1.Width, FormHeight - GenericBlock1.Height)); // right wall
            Boundaries.Add(new Rectangle(0, FormHeight - GenericBlock1.Height, FormWidth, GenericBlock1.Height)); // bottom
        }

        private void DrawBackground(Graphics g)
        {
            for (int i = 0; i < leftBackground.Height; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(leftBackground.X, i, 4 * GenericBlock1.Width, GenericBlock1.Height));
            }

            for (int i = 0; i < rightBackground.Height; i += GenericBackground.Height)
            {
                g.DrawImage(GenericBackground, new Rectangle(rightBackground.X, i, 4 * GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawLeftWall(Graphics g)
        {
            for (int i = 0; i < Boundaries[0].Height; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[0].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawBottom(Graphics g)
        {
            for (int i = 0; i < Boundaries[2].Width; i += GenericBlock1.Width)
            {
                g.DrawImage(GenericBlock1, new Rectangle(i + Boundaries[2].X, Boundaries[2].Y, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawRightWall(Graphics g)
        {
            for (int i = 0; i < Boundaries[1].Height; i += GenericBlock1.Height)
            {
                g.DrawImage(GenericBlock1, new Rectangle(Boundaries[1].X, i, GenericBlock1.Width, GenericBlock1.Height));
            }
        }

        private void DrawLeftWallSpikes(Graphics g)
        {
            foreach(Rectangle spike in LeftSpikes)
            {
                g.DrawImage(RightToLeftHorizontalSpike, spike);
            }
        }

        private void DrawRightWallSpikes(Graphics g)
        {
            foreach (Rectangle spike in RightSpikes)
            {
                g.DrawImage(LeftToRightHorizontalSpike, spike);
            }
        }

        private void DrawBottomSpikes(Graphics g)
        {
            foreach (Rectangle spike in BottomSpikes)
            {
                g.DrawImage(UprightSpike, spike);
            }
        }

        public override void Update(bool[] arrows, bool space)
        {
            base.Update(arrows, space);
            CheckCollisionWithStaticSpikes();
        }

        public override void Draw(Graphics g)
        {
            DrawBackgroundAndBlocks(g);
            DrawSpikes(g);
            Hero.Draw(g);
        }

        private void CheckCollisionWithStaticSpikes()
        {
            foreach(Rectangle spike in Triggers)
            {
                if (Hero.Character.IntersectsWith(spike))
                {
                    Hero.Dead = true;
                    break;
                }
            }
        }

        private void DrawBackgroundAndBlocks(Graphics g)
        {
            DrawBackground(g);
            DrawLeftWall(g);
            DrawBottom(g);
            DrawRightWall(g);
        }

        private void DrawSpikes(Graphics g)
        {
            DrawLeftWallSpikes(g);
            DrawBottomSpikes(g);
            DrawRightWallSpikes(g);
        }

        public override int ChangeLevel()
        {
            return -1;
        }
    }
}
