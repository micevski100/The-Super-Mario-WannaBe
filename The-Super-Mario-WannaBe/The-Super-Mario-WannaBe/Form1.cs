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
        public Level1 TestLevel1 { get; set; }
        public Level2 TestLevel2 { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //TestLevel1 = new Level1(new Hero());
            TestLevel2 = new Level2(new Hero());
            arrows = new bool[]{ false, false };
            space = false;
            spacePress = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightBlue);
            TestLevel2.Draw(e.Graphics);
        }

        private void GravityTimer_Tick(object sender, EventArgs e)
        {
            //TestLevel1.Update(arrows, spacePress);
            Invalidate();
            spacePress = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
