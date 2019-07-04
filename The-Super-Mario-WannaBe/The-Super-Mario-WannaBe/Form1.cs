using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Super_Mario_WannaBe
{

    public partial class Form1 : Form
    {
        public bool[] arrows;
        public bool space;
        public bool spacePress;
        public Level CurrentLevel { get; set; }
        public int restartAt { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            CurrentLevel = new Level1(new Hero());

            arrows = new bool[]{ false, false };
            space = false;
            spacePress = false;
            restartAt = 1;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightBlue);
            CurrentLevel.Draw(e.Graphics);
        }

        private void ChangeLevel()
        {
            int nextLevel = CurrentLevel.ChangeLevel();

            if (nextLevel == -1)
            {
                return;
            }
            else if (nextLevel == 1)
            {
                CurrentLevel = new Level1(CurrentLevel.Hero);
                restartAt = 1;
            }
            else if (nextLevel == 2)
            {
                CurrentLevel = new Level2(CurrentLevel.Hero);
                restartAt = 2;
            }
            else if (nextLevel == 3)
            {
                CurrentLevel = new Level3(CurrentLevel.Hero);
                restartAt = 3;
            }
            else if (nextLevel == 4)
            {
                CurrentLevel = new Level4(CurrentLevel.Hero);
                restartAt = 4;
            }
            else if (nextLevel == 5)
            {
                CurrentLevel = new Level5(CurrentLevel.Hero);
                restartAt = 5;
            }
            else if (nextLevel == 6)
            {
                CurrentLevel = new Level6(CurrentLevel.Hero);
                restartAt = 6;
            }
        }

        private void GravityTimer_Tick(object sender, EventArgs e)
        {
            CurrentLevel.Update(arrows, spacePress);
            Invalidate();
            spacePress = false;
            ChangeLevel();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                CurrentLevel.Hero.Dead = false;
                if (restartAt == 1 || restartAt == 4)
                {
                    CurrentLevel.Hero.Character = new RectangleF((int)(4.8 * Level1.GenericBlock1.Width), Level1.GenericBlock1.Height, CurrentLevel.Hero.Character.Width, CurrentLevel.Hero.Character.Height);
                    CurrentLevel = new Level1(CurrentLevel.Hero);
                }
                else if (restartAt == 2)
                {
                    CurrentLevel.Hero.Character = new RectangleF(6 * Level2.GenericBlock1.Width, Level2.FormHeight - 2 * Level2.GenericBlock1.Height, CurrentLevel.Hero.Character.Width, CurrentLevel.Hero.Character.Height);
                    CurrentLevel = new Level2(CurrentLevel.Hero);
                }
                else if (restartAt == 3)
                {
                    CurrentLevel.Hero.Character = new RectangleF(Level3.FormWidth - Level3.GenericBlock1.Width, 6 * Level3.GenericBlock1.Height, CurrentLevel.Hero.Character.Width, CurrentLevel.Hero.Character.Height);
                    CurrentLevel = new Level3(CurrentLevel.Hero);
                }
                else if (restartAt == 5 || restartAt == 6)
                {
                    CurrentLevel.Hero.Character = new RectangleF(Level5.GenericBlock1.Width, 9 * Level5.GenericBlock1.Height, CurrentLevel.Hero.Character.Width, CurrentLevel.Hero.Character.Height);
                    CurrentLevel = new Level5(CurrentLevel.Hero);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                arrows[0] = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                arrows[1] = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (space == false)
                {
                    spacePress = true;
                }
                space = true;
            }
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                arrows[0] = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                arrows[1] = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                space = false;
            }
            Invalidate();
        }
    }
}
