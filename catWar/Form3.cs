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
    public partial class Form3 : Form
    {
        Form2 Form2_Ref = null;
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\question.mdf;Integrated Security=True";
        public static bool correct;
        public Form3(Form2 form2_t)
        {
            InitializeComponent();
            Form2_Ref = form2_t;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            String strFind;
            if(Form2.questionLevel == 1)
            {
                string strCount = "SELECT COUNT(*) FROM Level1";//count total row
                SqlCommand countCommand = new SqlCommand(strCount, sql);
                int total = (int)countCommand.ExecuteScalar();
                Random r = new Random();
                int id = r.Next(0, total) + 1;
                strFind = "select * from Level1 where \"id\" IN (" + id.ToString() + ")";//select row              
            }
            else if (Form2.questionLevel == 2)
            {
                string strCount = "SELECT COUNT(*) FROM Level2";//count total row
                SqlCommand countCommand = new SqlCommand(strCount, sql);
                int total = (int)countCommand.ExecuteScalar();
                Random r = new Random();
                int id = r.Next(0, total) + 1;
                strFind = "select * from Level2 where \"id\" IN (" + id.ToString() + ")";//select row
            }
            else if (Form2.questionLevel == 3)
            {
                string strCount = "SELECT COUNT(*) FROM Level3";//count total row
                SqlCommand countCommand = new SqlCommand(strCount, sql);
                int total = (int)countCommand.ExecuteScalar();
                Random r = new Random();
                int id = r.Next(0, total) + 1;
                strFind = "select * from Level3 where \"id\" IN (" + id.ToString() + ")";//select row
            }
            else if (Form2.questionLevel == 4)
            {
                string strCount = "SELECT COUNT(*) FROM Level4";//count total row
                SqlCommand countCommand = new SqlCommand(strCount, sql);
                int total = (int)countCommand.ExecuteScalar();
                Random r = new Random();
                int id = r.Next(0, total) + 1;
                strFind = "select * from Level4 where \"id\" IN (" + id.ToString() + ")";//select row
            }
            else
            {
                string strCount = "SELECT COUNT(*) FROM Level5";//count total row
                SqlCommand countCommand = new SqlCommand(strCount, sql);
                int total = (int)countCommand.ExecuteScalar();
                Random r = new Random();
                int id = r.Next(0, total) + 1;
                strFind = "select * from Level5 where \"id\" IN (" + id.ToString() + ")";//select row
            }
            SqlCommand findCommand = new SqlCommand(strFind, sql);
            SqlDataReader myDataReader = findCommand.ExecuteReader();
            myDataReader.Read();
            label1.Text = myDataReader["question"].ToString();
            button1.Text = "A." + myDataReader["option1"].ToString();
            button2.Text = "B." + myDataReader["option2"].ToString();
            button3.Text = "C." + myDataReader["option3"].ToString();
            button4.Text = "D." + myDataReader["option4"].ToString();
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
            Form2_Ref.set_form3_result(correct);
            this.Close();
        }
    }
}
