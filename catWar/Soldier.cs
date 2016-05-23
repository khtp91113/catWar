﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catWar
{
    class Soldier
    {
        private static int start_point=200;
        private static int end_point=1000;
           
        private int side;        //0:我方 1:敵方
        private int blood;       //血量
        private int position;    //位置
        //private int order;       //順序
        private int atk_ab;      //攻擊力
        private int atk_speed; //攻擊間隔
        private int dfn_ab;      //防禦力
        private int move_ab;   //移動力
        private int cycle = 0; //用來讓攻擊圖案持續久一點
        public PictureBox pic;
        
        public Soldier(int arg_side, int level, Form f) 
        {
            pic = new PictureBox();
            pic.Size = new Size(50, 50);
            pic.Image = Resource1.tusky_005;
            pic.Parent = f; 
            init(arg_side,level);
        }

        public void init(int arg_side, int level)
        {
            if (arg_side == 0)
            {
                side = 0;
                position = 200;
                pic.Left = 200;
                pic.Top = 300;
                //Point(start_point + (end_point - start_point) / 100 * position, 300)
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
                position = 1000;
                pic.Left = 1000;
                pic.Top = 300;
                //Point(start_point + (end_point - start_point) / 100 * position, 300);
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
            pic.Show();
        }

        /*public void clock()
        {
            if (clk >= 2147483646 || clk < 0)
                clk = 0;
            clk++;
            if (atk_mode = true)
                attack();
            if (clk % 10 == 0)
                move();
        }*/

        public int get_position()
        {
            return position;
        }

        public void set_position(int x)
        {
            position = x;
        }

        /*public int get_front(int side)
        {
            return (side==0)?our_front:enemy_front;
        }*/

        /*public void set_front(int side, int value)
        {
            if (side == 0)
                our_front = value;
            else if (side == 1)
                enemy_front = value;
        }*/

        public void move()
        {
            pic.Image = Resource1.tusky_005;//change to move figure
            //position = x;
            //label0.BringToFront();
            //position += move_ab;
            /*if (side == 0) 
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
            }*/
            //label0.Location = new Point(start_point + (end_point - start_point) / 100 * position, 300);
        }

        public void hold()
        {
            pic.Image = Resource1.tusky_092;//change to freeze figure
        }

        public void attack()
        {
            pic.Image = Resource1.tusky_006;//change to attack figure
        }

        public void attacked(int x)
        {
            blood -= x;
        }

        public bool is_dead()
        {
            if (blood <= 0)
            {
                pic.Hide();
                return true;
            }
            return false;
        }

        public int getMoveAbility()
        {
            return move_ab;
        }

        public int getAttackPower()
        {
            return atk_ab;
        }

        public void set_cycle(int x)
        {
            cycle = x;
            if (cycle >= atk_speed)
                cycle = 0;
        }

        public int get_cycle()
        {
            return cycle;
        }

        public int get_atk_speed()
        {
            return atk_speed;
        }
    }
}
