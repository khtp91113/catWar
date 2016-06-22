using System;
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
        //private static int start_point=200;
        //private static int end_point=1000;
           
        private int side;        //0:我方 1:敵方
        private int blood;       //血量
        private int position;    //位置
        //private int order;       //順序
        private int atk_ab;      //攻擊力
        private int atk_speed; //攻擊間隔
        private int dfn_ab;      //防禦力
        private int move_ab;   //移動力
        private int cycle = 0; //用來讓攻擊圖案持續久一點
        private int atkFrame;
        private int moveFrame;
        //public ProgressBar bar; //顯示士兵血量
        private int kind;
        private int parent;

        public Soldier(int arg_side, int level) 
        {
            parent = -1;
            if (arg_side == 0)
            {
                kind = level;
            }
            else if (arg_side == 1)
            {
                kind = level + 5;
            }
            //bar.Parent = p;
            init(arg_side,level);
        }

        public int getKind()
        {
            return kind;
        }

        public void init(int arg_side, int level)
        {
            if (arg_side == 0)
            {
                side = 0;
                position = 200;
                switch (level)
                {
                    case 1:
                        blood=250;
                        atk_ab=30;
                        atk_speed=50;
                        dfn_ab=2;
                        move_ab=2;
                        atkFrame = 4;
                        moveFrame = 4;
                        break;
                    case 2:
                        blood=200;
                        atk_ab=50;
                        atk_speed=50;
                        dfn_ab=1;
                        move_ab=3;
                        atkFrame = 7;
                        moveFrame = 4;
                        break;
                    case 3:
                        blood=300;
                        atk_ab=120;
                        atk_speed=100;
                        dfn_ab=3;
                        move_ab=2;
                        atkFrame = 8;
                        moveFrame = 4;
                        break;
                    case 4:
                        blood=250;
                        atk_ab=50;
                        atk_speed=25;
                        dfn_ab=2;
                        move_ab=5;
                        atkFrame = 9;
                        moveFrame = 6;
                        break;
                    case 5:
                        blood=1000;
                        atk_ab=200;
                        atk_speed=75;
                        dfn_ab=5;
                        move_ab=1;
                        atkFrame = 15;
                        moveFrame = 4;
                        break;
                }
            }
            else if (arg_side == 1)
            {
                side = 1;
                position = 850;
                switch (level)
                {
                    case 1:
                        blood=240;
                        atk_ab=30;
                        atk_speed=50;
                        dfn_ab=2;
                        move_ab= -2;
                        atkFrame = 5;
                        moveFrame = 1;
                        break;
                    case 2:
                        blood=180;
                        atk_ab=50;
                        atk_speed=50;
                        dfn_ab=1;
                        move_ab= -3;
                        atkFrame = 5;
                        moveFrame = 1;
                        break;
                    case 3:
                        blood=300;
                        atk_ab=100;
                        atk_speed=100;
                        dfn_ab=3;
                        move_ab= -2;
                        atkFrame = 5;
                        moveFrame = 1;
                        break;
                    case 4:
                        blood=250;
                        atk_ab=60;
                        atk_speed=25;
                        dfn_ab=2;
                        move_ab= -5;
                        atkFrame = 5;
                        moveFrame = 3;
                        break;
                    case 5:
                        blood=1200;
                        atk_ab=200;
                        atk_speed=75;
                        dfn_ab=5;
                        move_ab= -1;
                        atkFrame = 5;
                        moveFrame = 3;
                        break;
                    case 6:
                        blood = 2000;
                        atk_ab = 250;
                        atk_speed = 50;
                        dfn_ab = 5;
                        move_ab = -2;
                        atkFrame = 5;
                        moveFrame = 3;
                        break;
                    case 7:
                        blood = 750;
                        atk_ab = 200;
                        atk_speed = 25;
                        dfn_ab = 2;
                        move_ab = -5;
                        atkFrame = 5;
                        moveFrame = 3;
                        break;
                    case 8:
                        blood = 2500;
                        atk_ab = 700;
                        atk_speed = 100;
                        dfn_ab = 4;
                        move_ab = -1;
                        atkFrame = 5;
                        moveFrame = 3;
                        break;
                }
            }
            //bar.Maximum = blood;
            //bar.Value = blood;
        }

        public int getMoveFrame()
        {
            return moveFrame;
        }

        public int getAtkFrame()
        {
            return atkFrame;
        }

        public int get_position()
        {
            return position;
        }

        public void set_position(int x)
        {
            position = x;
        }

        public void move()
        {
        }

        public void hold()
        {
            //pic.Image = Resource1.tusky_092;//change to freeze figure
        }

        public void attack()
        {
            //pic.Image = Resource1.tusky_006;//change to attack figure
        }

        public void attacked(int x)
        {
            blood -= x;
            //if (blood < 0)
            //    bar.Value = 0;
            //else
            //    bar.Value = blood;
        }

        public bool is_dead()
        {
            if (blood <= 0)
            {
               // pic.Hide();
                //pic.Enabled = false;
                //bar.Enabled = false;
                //bar.Hide();
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

        public int get_defense()
        {
            return dfn_ab;
        }
    }
}
