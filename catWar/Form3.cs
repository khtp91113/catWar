using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catWar
{
    public partial class Form3 : Form
    {
        public static bool correct;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button2.Click += button1_Click;
            button3.Click += button1_Click;
            button4.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button press = (Button)sender;
            int ans = 0;//ans read from file
            if(press.Equals(button1) && ans == 1)
            {
                correct = true;
            }
            else if(press.Equals(button2) && ans == 2)
            {
                correct = true;
            }
            else if(press.Equals(button3) && ans == 3)
            {
                correct = true;
            }
            else if(press.Equals(button4) && ans == 4)
            {
                correct = true;
            }
            else//wrong
            {
                correct = false;
            }
        }
    }
}
