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
            public static int blood;  //血量

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
            public static int side;        //0:我方 1:敵方
            public static int blood;       //血量
            public static float position;    //位置
            public static int order;       //順序
            public static int atk_ab;      //攻擊力
            public static int atk_speed; //攻擊間隔(毫秒)
            public static Timer tmr_atk = new Timer();
            public static int dfn_ab;      //防禦力
            public static float move_ab;   //移動力(每秒走多遠)
            public static Timer tmr_move = new Timer();
            public static Label label0 = new Label();
            
            public void tmr_atk_Tick(object sender, EventArgs e)
            {
                attack();
            }

            public void tmr_move_Tick(object sender, EventArgs e)
            {
                move();
            }

            public soldier() 
            {
                //init(arg_side, level);
            }

            public void init(int arg_side, int level, Form f)
            {
                if (arg_side == 0)
                {
                    side = 0;
                    position=0;
                    order=our_num;
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
                label0.Location = new Point(200 + (int)position, 300);
                label0.Text = level.ToString();
                f.Controls.Add(label0);
                

                tmr_atk.Interval = atk_speed;
                tmr_move.Interval = 1000;
                tmr_atk.Enabled = true;
                tmr_move.Enabled = true;
            }
            
            public void move()
            {
                position += move_ab;
            }

            public void attack()
            {
                // .attacked(atk_ab);
            }

            public void attacked(int x)
            {
                blood -= x;
            }

            public bool is_dead()
            {
                if (blood <= 0)
                {
                    tmr_atk.Enabled = false;
                    tmr_move.Enabled = false;
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
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.f1.Show();
        }

        public void set_form3_result(bool b) 
        {
            //form3_result = b;
            form3_result = true;

            label1.Text = form3_result.ToString();
            if (form3_result == true)
            {
                our_soldier[our_num] = new soldier();
                our_soldier[our_num++].init(0,questionLevel, this);
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
            else
                questionLevel = 5;
            Form3 f3 = new Form3(this);
            f3.ShowDialog();
        }
    }
}
