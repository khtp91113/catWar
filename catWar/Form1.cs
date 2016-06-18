using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace catWar
{
    public partial class Form1 : Form
    {
        static public Form1 f1;
        private bool endless;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            endless = false;
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            label1.Hide();
            button4.Hide();
            String[] difficulty = new String[] { "簡單", "普通", "困難", "地獄" };
            comboBox1.Items.AddRange(difficulty);
            comboBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            endless = false;
            label1.Hide();
            button4.Hide();
            comboBox1.Hide();
            DialogResult dialog = MessageBox.Show("確定要結束遊戲?", "離開遊戲", MessageBoxButtons.OKCancel);
            if (dialog == DialogResult.OK)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            endless = false;
            label1.Show();
            button4.Show();
            label1.Text = "普通模式\n說明: ...\n請選擇難度";
            comboBox1.SelectedIndex = 0;
            comboBox1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2;
            if (!endless)
            {
                switch (comboBox1.Text)
                {
                    case "簡單": f2 = new Form2(1); break;
                    case "普通": f2 = new Form2(2); break;
                    case "困難": f2 = new Form2(3); break;
                    default: f2 = new Form2(4); break;
                }
            }
            else
                f2 = new Form2(5);

            f1 = this;
            this.Hide();
            f2.Show();
            label1.Hide();
            button4.Hide();
            comboBox1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            endless = true;
            comboBox1.Hide();
            label1.Show();
            button4.Show();
            label1.Text = "無盡模式\n說明: ...";            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            endless = false;
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.BackgroundImage = Resource1.button1_hover;  
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Resource1.button1;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.BackgroundImage = Resource1.button2_hover;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Resource1.button2;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button5.BackgroundImage = Resource1.button3_hover;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Resource1.button3;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button3.BackgroundImage = Resource1.button4_hover;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = Resource1.button4;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button4.BackgroundImage = Resource1.button5_hover;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackgroundImage = Resource1.button5;
        }
    }
}
