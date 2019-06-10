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
        public Level1 TestLevel1 { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            TestLevel1 = new Level1();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            TestLevel1.Draw(e.Graphics);
        }
    }
}
