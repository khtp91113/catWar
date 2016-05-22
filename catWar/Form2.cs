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
    public partial class Form2 : Form
    {

        public class castle
        {
            private int blood;  //血量

            public void attacked(int x) //被攻擊
            {
                blood -= x;
            }

            public bool is_dead() //死亡
            {
                if (blood <= 0)
                    return true;
                return false;
            }
        }

        public class soldier
        {
            private static int start_point=200;
            private static int end_point=1000;
            private static int y_point = 300;
            private static int our_front=0;
            private static int enemy_front=100;
            
            private int clk;
            private int side;        //0:我方 1:敵方
            private int blood;       //血量
            private int position;    //位置
            private int order;       //順序
            private int atk_ab;      //攻擊力
            private int atk_speed; //攻擊間隔
            private bool atk_mode=false;
            private int dfn_ab;      //防禦力
            private int move_ab;   //移動力
            private Label label0;

            public soldier(int arg_side, int level, Form f) 
            {
                init(arg_side,level,f);
            }

            public void init(int arg_side, int level, Form f)
            {
                label0 = new Label();
                clk = 0;
                if (arg_side == 0)
                {
                    side = 0;
                    position=0;
                    order = our_num;
                    label0.Text = level.ToString();
                    label0.Location = new Point(start_point + (end_point - start_point) / 100 * position, 300);
                    switch (level)
                    {
                        case 1:
                            blood=250;
                            atk_ab=30;
                            atk_speed=500;
                            dfn_ab=10;
                            move_ab=2;
                            break;
                        case 2:
                            blood=200;
                            atk_ab=50;
                            atk_speed=500;
                            dfn_ab=5;
                            move_ab=3;
                            break;
                        case 3:
                            blood=300;
                            atk_ab=120;
                            atk_speed=1000;
                            dfn_ab=15;
                            move_ab=2;
                            break;
                        case 4:
                            blood=250;
                            atk_ab=50;
                            atk_speed=250;
                            dfn_ab=10;
                            move_ab=5;
                            break;
                        case 5:
                            blood=1000;
                            atk_ab=200;
                            atk_speed=750;
                            dfn_ab=25;
                            move_ab=1;
                            break;
                    }
                }
                else if (arg_side == 1)
                {
                    side = 1;
                    position = 100;
                    order = enemy_num;
                    label0.Text = "x";
                    label0.Location = new Point(start_point + (end_point - start_point) / 100 * position, 300);
                    switch (level)
                    {
                        case 1:
                            blood=250;
                            atk_ab=30;
                            atk_speed=500;
                            dfn_ab=10;
                            move_ab= -2;
                            break;
                        case 2:
                            blood=200;
                            atk_ab=50;
                            atk_speed=500;
                            dfn_ab=5;
                            move_ab= -3;
                            break;
                        case 3:
                            blood=300;
                            atk_ab=120;
                            atk_speed=1000;
                            dfn_ab=15;
                            move_ab= -2;
                            break;
                        case 4:
                            blood=250;
                            atk_ab=50;
                            atk_speed=250;
                            dfn_ab=10;
                            move_ab= -5;
                            break;
                        case 5:
                            blood=1000;
                            atk_ab=200;
                            atk_speed=750;
                            dfn_ab=25;
                            move_ab= -1;
                            break;
                    }
                }
                f.Controls.Add(label0);
                
            }

            public void clock()
            {
                if (clk >= 2147483646 || clk < 0)
                    clk = 0;
                clk++;
                if (atk_mode = true)
                    attack();
                if (clk % 10 == 0)
                    move();

            }

            public int get_position()
            {
                return position;
            }

            public int get_front(int side)
            {
                return (side==0)?our_front:enemy_front;
            }

            public void set_front(int side, int value)
            {
                if (side == 0)
                    our_front = value;
                else if (side == 1)
                    enemy_front = value;
            }

            public void move()
            {
                label0.BringToFront();
                position += move_ab;
                if (side == 0) 
                {
                    if (position >= enemy_front)
                    {
                        position = enemy_front - 1;
                        atk_mode = true;
                    }
                    else if (position >= 100)
                    {
                        position = 99;
                        atk_mode = true;
                    }
                }
                else if (side == 1)
                {
                    if (position <= our_front)
                    {
                        position = our_front+1;
                        atk_mode = true;
                    }
                    else if (position <=0)
                    {
                        position = 1;    
                        atk_mode = true;
                    }            
                }
                label0.Location = new Point(start_point + (end_point - start_point) / 100 * position, 300);
            }

            public void attack()
            {
            }

            public void attacked(int x)
            {
                blood -= x;
            }

            public bool is_dead()
            {
                if (blood <= 0)
                {
                    return true;
                }
                return false;
            }
        }

        public static int questionLevel;

        public static bool form3_result;

        public static int size = 100000;

        public static int our_num = 0, enemy_num = 0;

        castle our_castle = new castle();
        castle enemy_castle = new castle();
        soldier[] our_soldier = new soldier[size];
        soldier[] enemy_soldier = new soldier[size];

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button2.Click += button1_Click;
            button3.Click += button1_Click;
            button4.Click += button1_Click;
            button5.Click += button1_Click;
            timer1.Enabled = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.f1.Show();
        }

        public void set_form3_result(bool b) 
        {
            form3_result = b;
            //form3_result = true;

            label1.Text = form3_result.ToString();
            if (form3_result == true)
            {
                our_soldier[our_num] = new soldier(0, questionLevel, this);
                our_num++;
            }
        }

        public void set_front()
        {
            int temp;
            for (int i = 0; i < our_num; i++)
            {
                temp=our_soldier[i].get_position();
                if (temp > our_soldier[i].get_front(0) && temp < 100)
                    our_soldier[i].set_front(0, temp);
            }
            for (int i = 0; i < enemy_num; i++)
            {
                temp = enemy_soldier[i].get_position();
                if (temp < enemy_soldier[i].get_front(1) && temp >0)
                    enemy_soldier[i].set_front(1, temp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button press = (Button)sender;
            if (press.Equals(button1))
                questionLevel = 1;
            else if (press.Equals(button2))
                questionLevel = 2;
            else if (press.Equals(button3))
                questionLevel = 3;
            else if (press.Equals(button4))
                questionLevel = 4;
            else//測試派出敵人用
            {
                enemy_soldier[enemy_num] = new soldier(1, 4, this);
                enemy_num++;
                return;
            }
            //else
            //    questionLevel = 5;
            Form3 f3 = new Form3(this);
            f3.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < our_num; i++)
            {
                our_soldier[i].clock();
                set_front();
            }
            for (int i = 0; i < enemy_num; i++)
            {
                enemy_soldier[i].clock();
                set_front();
            }
        }
    }
}
