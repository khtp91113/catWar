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
        public static int questionLevel;
        
        public bool form3_result;

        public int size = 100;//picture width

        Castle our_castle, enemy_castle;
        List<Soldier> our_soldier, enemy_soldier;
        int our_front, enemy_front;
        int [] button_clock = new int [6];
        private int gameResult;
        private int ourCastleStart, enemyCastleStart;

        int mode;
        //0: 1:簡單 2:普通 3:困難 4: 5:地獄
        public Form2(int m)
        {
            InitializeComponent();
            mode = m;
            button6.Text = mode.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ourCastleStart = pictureBox1.Left + pictureBox1.Width - 1;
            enemyCastleStart = pictureBox2.Left + 1;
            //pictureBox1.Parent = pictureBox3;
            pictureBox1.Parent = this;
            //pictureBox2.Parent = pictureBox3;
            pictureBox2.Parent = this;
            gameResult = 0;
            our_castle = new Castle(this,1);
            enemy_castle = new Castle(this,2);
            DoubleBuffered = true;
            our_soldier = new List<Soldier>();
            enemy_soldier = new List<Soldier>();            
            //our_front = pictureBox1.Left + pictureBox1.Width - 1;//our front save first soldier's x location, if there is no soldier let it be start_point-1
            //enemy_front = pictureBox2.Left + 1;//enemy_front save first enemy's x location, if there is no enemy let it be end_point+1
            our_front = ourCastleStart;
            enemy_front = enemyCastleStart;

            button2.Click += button1_Click;
            button3.Click += button1_Click;
            button4.Click += button1_Click;
            button5.Click += button1_Click;
            timer1.Interval = 15;
            timer1.Enabled = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.f1.Show();
        }

        public void set_form3_result(bool b) 
        {
            
            form3_result = b;
            
            if (form3_result == true)
            {
                switch (questionLevel)
                {
                    case 1:
                        button_clock[questionLevel] += 60; button1.Enabled = false; button1.BackColor= Color.Black; break;
                    case 2:
                        button_clock[questionLevel] += 100; button2.Enabled = false; button2.BackColor= Color.Black; break;
                    case 3:
                        button_clock[questionLevel] += 140; button3.Enabled = false; button3.BackColor= Color.Black; break;
                    case 4:
                        button_clock[questionLevel] += 220; button4.Enabled = false; button4.BackColor= Color.Black; break;
                    case 5:
                        button_clock[questionLevel] += 350; button5.Enabled = false; button5.BackColor= Color.Black; break;
                }

                //Soldier temp = new Soldier(0, questionLevel, pictureBox3);//generate soldier
                Soldier temp = new Soldier(0, questionLevel);
                
                our_soldier.Add(temp);
            }
            else 
            {
                switch (questionLevel)
                {
                    case 1:
                        button_clock[questionLevel] += 120; button1.Enabled = false; button1.BackColor = Color.Black; break;
                    case 2:
                        button_clock[questionLevel] += 200; button2.Enabled = false; button2.BackColor = Color.Black; break;
                    case 3:
                        button_clock[questionLevel] += 280; button3.Enabled = false; button3.BackColor = Color.Black; break;
                    case 4:
                        button_clock[questionLevel] += 440; button4.Enabled = false; button4.BackColor = Color.Black; break;
                    case 5:
                        button_clock[questionLevel] += 700; button5.Enabled = false; button5.BackColor = Color.Black; break;
                }           
            }
        }

        public void set_enemy_front()
        {
            if(enemy_soldier.Count == 0)
            {
                enemy_front = enemyCastleStart;
            }
            else
            {
                enemy_front = enemy_soldier[0].get_position();
                for (int i = 1; i < enemy_soldier.Count; i++)
                {
                    enemy_front = enemy_front > enemy_soldier[i].get_position() ? enemy_soldier[i].get_position() : enemy_front;
                }
            }
        }

        public void set_our_front()
        {
            if (our_soldier.Count == 0)
            {
                our_front = ourCastleStart;
            }
            else
            {
                our_front = our_soldier[0].get_position();
                for (int i = 1; i < our_soldier.Count; i++)
                {
                    our_front = our_front < our_soldier[i].get_position() ? our_soldier[i].get_position() : our_front;
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
            else
                questionLevel = 5;
            Form3 f3 = new Form3(this);
            f3.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap canvas = new Bitmap(1200, 700);
            Graphics gtemp = Graphics.FromImage(canvas);
            gtemp.DrawImage(Resource1.bg_2, 0, 0, 1200, 700);
                        
            Graphics g = Graphics.FromImage(canvas);
            this.BackgroundImage = canvas;
            
            for (int i=0; i < our_soldier.Count ; i++)//soldier's moving time
            {
                our_soldier[i].pic.SizeMode = PictureBoxSizeMode.StretchImage;
                if (our_soldier[i].get_position() + our_soldier[i].getMoveAbility() + size >= enemy_front && enemy_soldier.Count != 0)//soldier can attack at least one enemy
                {
                    if(our_soldier[i].get_position() + size < enemy_front)
                        our_soldier[i].pic.Left = enemy_front - size;
                    
                    if (our_soldier[i].get_cycle() <= (our_soldier[i].get_atk_speed() / 2))//let attack image last longer
                    {
                        if (our_soldier[i].get_cycle() < 4)//attack
                        //if(our_soldier[i].get_cycle() % 4)
                        {
                            Image temp;
                            switch (our_soldier[i].get_cycle() % 4)
                            {
                                case 0: temp = Resource1.ch1attack1; break;
                                case 1: temp = Resource1.ch1attack2; break;
                                case 2: temp = Resource1.ch1attack3; break;
                                default: temp = Resource1.ch1attack4; break;
                            }
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                            //pictureBox3.Image = canvas;
                            //our_soldier[i].attack();//change to attack picture
                            for (int j = 0; j < enemy_soldier.Count; j++)//find the enemy whose position will be attacked
                            {
                                if (enemy_soldier[j].get_position() == enemy_front)
                                {
                                    enemy_soldier[j].attacked(our_soldier[i].getAttackPower() - enemy_soldier[j].get_defense());//enemy lose health
                                    if (enemy_soldier[j].is_dead())
                                    {
                                        enemy_soldier[j] = null;
                                        enemy_soldier.RemoveAt(j);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Image temp = Resource1.ch1attack1;
                            
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                    }
                    else//between attack interval
                    {
                        our_soldier[i].hold();//change picture to stand freeze
                        Image temp = Resource1.ch1attack1;
                        g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                    }
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else if(our_soldier[i].get_position() + our_soldier[i].getMoveAbility() + size >= enemyCastleStart - 1 && enemy_soldier.Count == 0)//get to enemy castle and there's no enemy, attack castle
                {
                    if(our_soldier[i].get_position() + size < enemyCastleStart - 1)
                        our_soldier[i].pic.Left = enemyCastleStart - 2 - size;

                    if (our_soldier[i].get_cycle() <= (our_soldier[i].get_atk_speed() / 2))//let attack image last longer
                    {
                        if (our_soldier[i].get_cycle() < 4)//attack
                        {
                            Image tem;
                            switch (our_soldier[i].get_cycle() % 4)
                            {
                                case 0: tem = Resource1.ch1attack1; break;
                                case 1: tem = Resource1.ch1attack2; break;
                                case 2: tem = Resource1.ch1attack3; break;
                                default: tem = Resource1.ch1attack4; break;
                            }
                            g.DrawImage(tem, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                        else
                        {
                            Image temp = Resource1.ch1attack1;

                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                        our_soldier[i].attack();
                        
                        if (our_soldier[i].get_cycle() == 0 && mode != 5)//enemy castle lose health
                            enemy_castle.attacked(our_soldier[i].getAttackPower());

                        if (our_soldier[i].get_cycle() == 0 && mode == 5)//enemy castle lose health
                        {
                            //Soldier temp = new Soldier(1, 8, pictureBox3);
                            Soldier temp = new Soldier(1, 8);
                            enemy_soldier.Add(temp);
                        }
                        if(enemy_castle.is_dead()&&mode!=5)//destroy enemy castle, win
                        {
                            timer1.Enabled = false;
                            gameResult = 1;
                            break;
                        }
                    }
                    else//between attack interval
                    {
                        our_soldier[i].hold();
                        {
                            Image temp = Resource1.ch1attack1;

                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                    }
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else//not enough attack range, move
                {
                    our_soldier[i].move();
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle()+1);
                    our_soldier[i].pic.Left += our_soldier[i].getMoveAbility();//move soldier
                    //our_soldier[i].bar.Left = our_soldier[i].pic.Left;
                    our_soldier[i].set_position(our_soldier[i].get_position() + our_soldier[i].getMoveAbility());//update solier's position value
                    set_our_front();//update first soldier's location
                    Image temp;
                    if (our_soldier[i].get_cycle() % 4 ==1)
                        temp = Resource1.ch1move1;
                    else if (our_soldier[i].get_cycle() % 4 ==2)
                        temp = Resource1.ch1move2;
                    else if (our_soldier[i].get_cycle() % 4 == 3)
                        temp = Resource1.ch1move3;
                    else
                        temp = Resource1.ch1move4;
                    g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                    //pictureBox3.Image = canvas;
                }
            }
            if(gameResult == 1)
            {
                DialogResult d = MessageBox.Show("win", "Victory", MessageBoxButtons.OK);
                if (d == DialogResult.OK)
                {
                    this.Close();
                }
            }

            for (int i = 0; i < enemy_soldier.Count ; i++)//enemy's moving time
            {
                if (enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility() <= our_front + size && our_soldier.Count != 0)//enough attack range
                {
                    if (enemy_soldier[i].get_position() - size > our_front)
                        enemy_soldier[i].pic.Left = our_front + size;
                    if (enemy_soldier[i].get_cycle() <= (enemy_soldier[i].get_atk_speed() / 2))//let attack image last longer
                    {
                        if (enemy_soldier[i].get_cycle() == 0)
                        {
                            enemy_soldier[i].attack();
                            for (int j = 0; j < our_soldier.Count; j++)
                            {
                                if (our_soldier[j].get_position() == our_front)
                                {
                                    our_soldier[j].attacked(enemy_soldier[i].getAttackPower() - our_soldier[j].get_defense());
                                    if (our_soldier[j].is_dead())
                                    {
                                        our_soldier[j] = null;
                                        our_soldier.RemoveAt(j);
                                    }
                                    break;
                                }
                            }
                        }
                            
                    }
                    else//between attack interval
                    {
                        enemy_soldier[i].hold();
                    }
                    enemy_soldier[i].set_cycle(enemy_soldier[i].get_cycle() + 1);
                }
                else if(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility() <= ourCastleStart + 1 && our_soldier.Count == 0)//attack castle
                {
                    if (enemy_soldier[i].get_position() > ourCastleStart + 1)
                        enemy_soldier[i].pic.Left = ourCastleStart + 1;
                    if (enemy_soldier[i].get_cycle() <= (enemy_soldier[i].get_atk_speed() / 2))
                    {
                        enemy_soldier[i].attack();
                        
                        if(enemy_soldier[i].get_cycle() == 0)
                            our_castle.attacked(enemy_soldier[i].getAttackPower());
                        if(our_castle.is_dead())
                        {
                            timer1.Enabled = false;
                            gameResult = 2;
                            break;
                            
                        }
                    }
                    else//in attack interval
                    {
                        enemy_soldier[i].hold();
                    }
                    enemy_soldier[i].set_cycle(enemy_soldier[i].get_cycle() + 1);
                }
                else//not enough attack range , move
                {
                    enemy_soldier[i].move();
                    enemy_soldier[i].set_cycle(0);
                    enemy_soldier[i].pic.Left += enemy_soldier[i].getMoveAbility();
                    //enemy_soldier[i].bar.Left = enemy_soldier[i].pic.Left;
                    enemy_soldier[i].set_position(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility());
                    set_enemy_front();
                }

            }
            if(gameResult == 2)
            {
                DialogResult d = MessageBox.Show("lose", "Defeat", MessageBoxButtons.OK);
                if (d == DialogResult.OK)
                {
                    this.Close();
                }
            }

            for (int i = 1; i < 6; i++)
            {
                if (button_clock[i] <= 0)
                {
                    switch (i)
                    {
                        case 1:
                            button1.Enabled = true; button1.BackColor= Color.White; break;
                        case 2:
                            button2.Enabled = true; button2.BackColor= Color.White; break;
                        case 3:
                            button3.Enabled = true; button3.BackColor= Color.White; break;
                        case 4:
                            button4.Enabled = true; button4.BackColor= Color.White; break;
                        case 5:
                            button5.Enabled = true; button5.BackColor = Color.White; break;
                    }
                }
                else
                    button_clock[i]--;
            }
            Random r = new Random();
            int arg=150;
            int max_level=8;
            switch (mode) 
            {
                case 1: arg = 500; max_level = 3; break;
                case 2: arg = 300; max_level = 5; break;
                case 3: arg = 225; max_level = 7; break;
                case 4: arg = 100; max_level = 8; break;
                default: break;
            }
            for(int j=mode;j>0;j--)
            {
                if(r.Next(99999)%(arg*j) == 0)
                {
                    //Soldier temp = new Soldier(1, j, pictureBox3);
                    Soldier temp = new Soldier(1, j);
                    enemy_soldier.Add(temp);
                    if(mode==5&&arg>50)
                        arg--;
                    break;
                }                
            }
            
        }

        private void button6_Click(object sender, EventArgs e)//for test
        {
            //Soldier temp = new Soldier(1, 4, pictureBox3);
            Soldier temp = new Soldier(1, 4);
            enemy_soldier.Add(temp);
        }

    }
}
