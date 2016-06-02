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
    public partial class Form4 : Form
    {

        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\question.mdf;Integrated Security=True";
        
        public Form4()
        {
            InitializeComponent();
            this.Text = "新增題目";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("題目不得為空");
                return;
            }
            else if(textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("選項欄不得為空");
                return;
            }
            else if(textBox6.Text == "")
            {
                MessageBox.Show("答案不得為空");
                return;
            }
            else if(textBox6.Text != "1" && textBox6.Text != "2" && textBox6.Text != "3" && textBox6.Text != "4")
            {
                MessageBox.Show("答案請以數字(1~4)表示");
                return;
            }
            else
            {
                Random r = new Random();
                int level = r.Next(5) + 1;

                SqlConnection sql = new SqlConnection(connectionString);//connect
                sql.Open();

                SqlCommand sqlcom = new SqlCommand("INSERT INTO Level" + level.ToString() + " (question,option1,option2,option3,option4,ans) VALUES('" + textBox1.Text +"','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "'," + textBox6.Text + ")", sql);//insert notice: after insert if reconnect to database all insert will be recover
                sqlcom.ExecuteNonQuery();

                sql.Close();
                this.Close();
            }
        }
    }
}
