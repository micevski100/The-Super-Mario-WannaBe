using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class PeekingSpikes
    {
        public static readonly Image Spike = FallingSpike.GenericSpikeUpright;
        public int Tick { get; set; }
        public int WaitTime { get; set; }
        public bool IsActive { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle Trigger { get; set; }
        public int PeekSize { get; set; }
        public bool IsCycleFinished { get; set; }
        public int ReturnSize { get; set; }

        public PeekingSpikes(int x, int y, Rectangle Trigger)
        {
            Bounds = new Rectangle(x, y, Spike.Width + Spike.Width / 2, Spike.Height + 10);
            this.Trigger = Trigger;
            IsActive = false;
            Tick = 0;
            WaitTime = 100;
            PeekSize = ReturnSize = 30;
            IsCycleFinished = false;
        }

        private void CheckTriggerArea(Hero hero)
        {
            if (hero.Character.IntersectsWith(Trigger))
            {
                IsActive = true;
            }
        }

        public void Update(Hero hero)
        {
            CheckTriggerArea(hero);
            Move();
            CheckCollisionWithHero(hero);
        }

        private void Move()
        {
            if (IsActive)
            {
                if (ReturnSize <= 0)
                {
                    IsCycleFinished = true;
                }
                else
                {
                    if (PeekSize > 0)
                    {
                        MoveUp();
                        --PeekSize;
                    }
                    else
                    {
                        if (Tick >= WaitTime && ReturnSize > 0)
                        {
                            MoveDown();
                            --ReturnSize;
                        }
                        else
                        {
                            ++Tick;
                        }
                    }
                }
            }
        }

        private void MoveUp()
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y - 1, Bounds.Width, Bounds.Height);
        }

        private void MoveDown()
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y + 1, Bounds.Width, Bounds.Height);
        }

        public void Draw(Graphics g)
        {
            if (!IsCycleFinished && IsActive)
            {
                g.DrawImage(Spike, Bounds);
            }
        }

        private void CheckCollisionWithHero(Hero hero)
        {
            if (!IsCycleFinished && hero.Character.IntersectsWith(Bounds))
            {
                hero.Dead = true;
            }
        }
    }
}
