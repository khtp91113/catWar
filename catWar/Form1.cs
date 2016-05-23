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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Hide();
            button4.Hide();
            String[] difficulty = new String[] { "簡單", "普通", "困難", "地獄" };
            comboBox1.Items.AddRange(difficulty);
            comboBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Hide();
            button4.Hide();
            comboBox1.Hide();
            DialogResult dialog = MessageBox.Show("確定要結束遊戲?", "離開遊戲", MessageBoxButtons.OKCancel);
            if (dialog == DialogResult.OK)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Show();
            button4.Show();
            label1.Text = "普通模式\n說明: ...\n請選擇難度";
            comboBox1.SelectedIndex = 0;
            comboBox1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f1 = this;
            this.Hide();
            f2.Show();
            label1.Hide();
            button4.Hide();
            comboBox1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Hide();
            label1.Show();
            button4.Show();
            label1.Text = "無盡模式\n說明: ...";            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }
}
