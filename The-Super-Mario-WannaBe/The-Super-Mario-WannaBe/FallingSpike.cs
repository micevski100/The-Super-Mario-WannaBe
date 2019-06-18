using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class FallingSpike
    {
        public enum Type
        {
            GoingDown,
            GoingUp
        }
        public static readonly Image GenericSpikeUpside = Properties.Resources.SingleSpikeUpsideDown;
        public static readonly Image GenericSpikeUpright = Properties.Resources.UprightSpikeSingle;

        public Image GenericSpike { get; set; }

        public Rectangle Bounds { get; set; }
        public Rectangle Trigger { get; set; }
        public bool IsActive;
        public bool InForm;
        public int Speed { get; set; }

        public FallingSpike(int x, int y, Rectangle Trigger, Type type)
        {
            this.Bounds = new Rectangle(x, y, GenericSpikeUpside.Width + GenericSpikeUpside.Width / 2, GenericSpikeUpside.Height + 10);
            this.Trigger = Trigger;
            IsActive = false;
            InForm = true;
            
            if (type == Type.GoingDown)
            {
                Speed = 8;
                GenericSpike = GenericSpikeUpside;
            }
            else
            {
                Speed = -8;
                GenericSpike = GenericSpikeUpright;
            }
        }

        public void CheckSpikePosition()
        {
            if (IsActive)
            {
                InForm = !(Bounds.Top > Level3.FormHeight || Bounds.Bottom < 0);
            }
        }

        private void CheckTriggerArea(Hero hero)
        {
            if (hero.Character.IntersectsWith(Trigger))
            {
                IsActive = true;
            }
        }

        private bool CheckCollisionWithHero(Hero hero)
        {
            return hero.Character.IntersectsWith(Bounds);
        }
        
        public void Update(Hero hero)
        {
            CheckTriggerArea(hero);
            Move();
            if (IsActive && CheckCollisionWithHero(hero))
            {
                hero.Dead = true;
            }
        }

        public void Move()
        {
            if (IsActive)
            { 
                Bounds = new Rectangle(Bounds.X, Bounds.Y + Speed, Bounds.Width, Bounds.Height);
            }
        }

        public void Draw(Graphics g)
        {
            //g.DrawRectangle(new Pen(new SolidBrush(Color.Red)), Bounds);
            g.DrawImage(GenericSpike, Bounds);
        }
    }
}
