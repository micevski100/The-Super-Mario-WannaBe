using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Super_Mario_WannaBe
{
    public class Hero
    {
        public enum DIRECTION { Left, Right, Up, None }

        public static readonly Image GameOverMessage = Properties.Resources.GameOverMessage;
        private static readonly int framesBeforeChange = 5;

        public static List<Image> StandingLeftFrames { get; set; }
        public static List<Image> StandingRightFrames { get; set; }
        public static List<Image> RunningLeftFrames { get; set; }
        public static List<Image> RunningRightFrames { get; set; }
        public static List<Image> JumpingLeftFrames { get; set; }
        public static List<Image> JumpingRightFrames { get; set; }

        public RectangleF Character { get; set; }
        public bool OnGround { get; set; }
        public bool Dead { get; set; }
        public int CurrentStandingFrame { get; set; }
        public int CurrentJumpingFrame { get; set; }
        public int CurrentRunningFrame { get; set; }
        public Image CurrentFrame { get; set; }
        public DIRECTION PreviousFrame { get; set; }


        public Hero()
        {
            Character = new RectangleF((int) (4.8 * Level1.GenericBlock1.Width), Level1.GenericBlock1.Height, Level1.GenericBlock1.Width / 2, Level1.GenericBlock1.Height / 2);
            Dead = false;

            CurrentFrame = Properties.Resources.standing1r;
            PreviousFrame = DIRECTION.Right;

            InitializeStandingLeftFrames();
            InitializeStandingRightFrames();

            InitializeJumpingLeftFrames();
            InitializeJumpingRightFrames();

            InitializeRunningLeftFrames();
            InitializeRunningRightFrames();
        }

        private void InitializeStandingLeftFrames()
        {
            StandingLeftFrames = new List<Image>();

            StandingLeftFrames.Add(Properties.Resources.standing1);
            StandingLeftFrames.Add(Properties.Resources.standing1);
            StandingLeftFrames.Add(Properties.Resources.standing2);
            StandingLeftFrames.Add(Properties.Resources.standing4);
        }

        private void InitializeStandingRightFrames()
        {
            StandingRightFrames = new List<Image>();

            StandingRightFrames.Add(Properties.Resources.standing1r);
            StandingRightFrames.Add(Properties.Resources.standing2r);
            StandingRightFrames.Add(Properties.Resources.standing3r);
            StandingRightFrames.Add(Properties.Resources.standing4r);
        }

        private void InitializeRunningLeftFrames()
        {
            RunningLeftFrames = new List<Image>();

            RunningLeftFrames.Add(Properties.Resources.running1);
            RunningLeftFrames.Add(Properties.Resources.running2);
            RunningLeftFrames.Add(Properties.Resources.running3);
            RunningLeftFrames.Add(Properties.Resources.running4);
            RunningLeftFrames.Add(Properties.Resources.running5);
        }

        private void InitializeRunningRightFrames()
        {
            RunningRightFrames = new List<Image>();

            RunningRightFrames.Add(Properties.Resources.running1r);
            RunningRightFrames.Add(Properties.Resources.running2r);
            RunningRightFrames.Add(Properties.Resources.running3r);
            RunningRightFrames.Add(Properties.Resources.running4r);
            RunningRightFrames.Add(Properties.Resources.running5r);
        }

        private void InitializeJumpingLeftFrames()
        {
            JumpingLeftFrames = new List<Image>();

            JumpingLeftFrames.Add(Properties.Resources.jumping1);
            JumpingLeftFrames.Add(Properties.Resources.jumping2);
        }

        private void InitializeJumpingRightFrames()
        {
            JumpingRightFrames = new List<Image>();

            JumpingRightFrames.Add(Properties.Resources.jumping1r);
            JumpingRightFrames.Add(Properties.Resources.jumping2r);
        }

        public void Draw(Graphics g)
        {
            if (!Dead)
            {
                g.DrawImage(CurrentFrame, Character);
            }
            else
            {
                g.DrawImage(GameOverMessage, new Point(175, 200));
            }
        }

        public void Fall()
        {
            float y = Character.Y + 3.0f;
            float x = Character.X;
            float width = Character.Width;
            float height = Character.Height;

            Character = new RectangleF(x, y, width, height);
        }
        
        private RectangleF MoveLeft()
        {
            float x = Character.X - 1.5f;
            float y = Character.Y;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        private RectangleF MoveUp()
        {
            float x = Character.X;
            float y = Character.Y - 3.0f;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        private RectangleF MoveRight()
        {
            float x = Character.X + 1.5f;
            float y = Character.Y;
            float width = Character.Width;
            float height = Character.Height;

            return new RectangleF(x, y, width, height);
        }

        public void Move(DIRECTION direction)
        {
            if (direction.Equals(DIRECTION.Left))
            {
                if (OnGround)
                {
                    CurrentFrame = RunningLeftFrames[CurrentRunningFrame / framesBeforeChange];
                    ++CurrentRunningFrame;
                    CurrentRunningFrame %= 5 * framesBeforeChange;
                }
                Character = MoveLeft();
                PreviousFrame = DIRECTION.Left;
            }
            
            if (direction.Equals(DIRECTION.Right))
            {
                if (OnGround)
                {
                    CurrentFrame = RunningRightFrames[CurrentRunningFrame / framesBeforeChange];
                    ++CurrentRunningFrame;
                    CurrentRunningFrame %= 5 * framesBeforeChange;
                }
                Character = MoveRight();
                PreviousFrame = DIRECTION.Right;
            }

            if (direction.Equals(DIRECTION.Up))
            {
                if (PreviousFrame == DIRECTION.Left)
                {
                    CurrentFrame = JumpingLeftFrames[CurrentJumpingFrame / framesBeforeChange];
                    ++CurrentJumpingFrame;
                    CurrentJumpingFrame %= 2 * framesBeforeChange;
                }
                else
                {
                    CurrentFrame = JumpingRightFrames[CurrentJumpingFrame / framesBeforeChange];
                    ++CurrentJumpingFrame;
                    CurrentJumpingFrame %= 2 * framesBeforeChange;
                }
                Character = MoveUp();
            }
        }

        public void ResetFrame()
        {
            if (PreviousFrame == DIRECTION.Right)
            {
                CurrentFrame = StandingRightFrames[CurrentStandingFrame / framesBeforeChange];
                ++CurrentStandingFrame;
            }
            else if (PreviousFrame == DIRECTION.Left)
            {
                CurrentFrame = StandingLeftFrames[CurrentStandingFrame / framesBeforeChange];
                ++CurrentStandingFrame;
            }

            CurrentStandingFrame %= 4 * framesBeforeChange;
            CurrentRunningFrame = 0;
            CurrentJumpingFrame = 0;
        }
    }
}
