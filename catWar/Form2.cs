using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace catWar
{
    public partial class Form2 : Form
    {

        /*public class castle
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
        }*/

        /*public class soldier
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
        }*/

        public static int questionLevel;

        public bool form3_result;

        public static int size = 100;

        public int our_num = 0, enemy_num = 0;

        Castle our_castle = new Castle();
        Castle enemy_castle = new Castle();
        Soldier[] our_soldier = new Soldier[size];
        Soldier[] enemy_soldier = new Soldier[size];
        List<int>[] our_axis,enemy_axis;
        //List<int> our_num, enemy_num;
        int our_front=200, enemy_front=1000;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            our_axis = new List<int>[1001];
            enemy_axis = new List<int>[1001];
            for (int i = 0; i < 1001; i++)
            {
                our_axis[i] = new List<int>();
                enemy_axis[i] = new List<int>();
            }
                
            our_front = 200;
            enemy_front = 1000;
            button2.Click += button1_Click;
            button3.Click += button1_Click;
            button4.Click += button1_Click;
            button5.Click += button1_Click;
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.f1.Show();
        }

        public void set_form3_result(bool b) 
        {
            form3_result = b;

            //label1.Text = form3_result.ToString();
            if (form3_result == true)
            {
                our_soldier[our_num] = new Soldier(0, questionLevel, this);
                //enemy_soldier[enemy_num] = new Soldier(0, questionLevel, this);
                
                //our_axis[200].Add(our_soldier[our_num].index);
                our_num++;
                //enemy_num++;
            }
        }

        public void set_enemy_front()
        {
            for (int i = 200; i <= 1000; i++)
            {
                if (enemy_axis[i].Count != 0)
                {
                    enemy_front = i;
                    break;
                } 
            }
        }

        public void set_our_front()
        {
            for(int i = 1000 ; i >= 200 ; i--)
            {
                if(our_axis[i].Count != 0)
                {
                    our_front = i;
                    break;
                }
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
            /*else//測試派出敵人用
            {
                enemy_soldier[enemy_num] = new soldier(1, 4);
                enemy_num++;
                return;
            }*/
            else
                questionLevel = 5;
            Form3 f3 = new Form3(this);
            f3.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < our_num; i++)
            {
                if (our_soldier[i].get_position() + our_soldier[i].getMoveAbility() >= enemy_front)//enough attack range
                {
                    if (our_soldier[i].get_cycle() % our_soldier[i].get_atk_speed() == 0)//can attack
                    {
                        our_soldier[i].attack();
                        int[] tempArray = enemy_axis[enemy_front].ToArray();
                        enemy_soldier[tempArray[0]].attacked(our_soldier[i].getAttackPower());
                        if (enemy_soldier[tempArray[0]].is_dead())
                        {
                            enemy_axis[enemy_front].RemoveAt(0);
                            if (enemy_axis[enemy_front].Count == 0)
                                set_enemy_front();
                        }
                    }
                    else//in attack interval
                    {
                        our_soldier[i].hold();
                    }
                }
                else//not enough attack range , move
                {
                    our_soldier[i].set_cycle(0);
                    our_soldier[i].pic.Left += our_soldier[i].getMoveAbility();
                    our_soldier[i].set_position(our_soldier[i].get_position() + our_soldier[i].getMoveAbility());
                    set_our_front();
                }
            }
            for (int i = 0; i < enemy_num; i++)
            {
                if (enemy_soldier[i].get_position() - enemy_soldier[i].getMoveAbility() <= our_front)//enough attack range
                {
                    if (enemy_soldier[i].get_cycle() % enemy_soldier[i].get_atk_speed() == 0)//can attack
                    {
                        enemy_soldier[i].attack();
                        int[] tempArray = our_axis[our_front].ToArray();
                        our_soldier[tempArray[0]].attacked(enemy_soldier[i].getAttackPower());
                        if (our_soldier[tempArray[0]].is_dead())
                        {
                            our_axis[our_front].RemoveAt(0);
                            if (our_axis[our_front].Count == 0)
                                set_our_front();
                        }
                    }
                    else//in attack interval
                    {
                        enemy_soldier[i].hold();
                    }
                }
                else//not enough attack range , move
                {
                    enemy_soldier[i].set_cycle(0);
                    enemy_soldier[i].pic.Left -= enemy_soldier[i].getMoveAbility();
                    enemy_soldier[i].set_position(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility());
                    set_enemy_front();
                }
               // enemy_soldier[i].clock();
                //set_front();
            }
        }
    }
}
