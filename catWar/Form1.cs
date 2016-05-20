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
        //string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\question.mdf;Integrated Security=True";
        
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
            //database code
            /*SqlConnection sql = new SqlConnection(connectionString);//connect
            sql.Open();
            
            SqlCommand sqlcom = new SqlCommand("INSERT INTO question (question,ans) VALUES('fdf',2)", sql);//insert notice: after insert if reconnect to database all insert will be recover
            sqlcom.ExecuteNonQuery();            

            string strCount = "SELECT COUNT(*) FROM question";//count total row
            SqlCommand Command = new SqlCommand(strCount, sql);
            int v = (int)Command.ExecuteScalar();
            button3.Text = v.ToString();
            
            String strSQL = "select * from question where \"id\" IN (15)";//select row
            SqlCommand myCommand = new SqlCommand(strSQL,sql);
            SqlDataReader myDataReader = myCommand.ExecuteReader();
            myDataReader.Read();
            button1.Text = myDataReader["question"].ToString();

            sql.Close();*/
            
        }



    }
}
