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

        public int size = 50;//picture width

        Castle our_castle = new Castle();
        Castle enemy_castle = new Castle();
        List<Soldier> our_soldier, enemy_soldier;
        int our_front, enemy_front;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            our_soldier = new List<Soldier>();
            enemy_soldier = new List<Soldier>();            
            our_front = 199;//our front save first soldier's x location, if there is no soldier let it be start_point-1
            enemy_front = 1001;//enemy_front save first enemy's x location, if there is no enemy let it be end_point+1
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

            if (form3_result == true)
            {
                Soldier temp = new Soldier(0, questionLevel, this);//generate soldier
                our_soldier.Add(temp);
            }
        }

        public void set_enemy_front()
        {
            if(enemy_soldier.Count == 0)
            {
                enemy_front = 1001;
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
                our_front = 199;
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
            for (int i=0; i < our_soldier.Count ; i++)//soldier's moving time
            {
                if (our_soldier[i].get_position() + our_soldier[i].getMoveAbility() + size >= enemy_front && enemy_soldier.Count != 0)//soldier can attack at least one enemy
                {
                    if (our_soldier[i].get_cycle() <= (our_soldier[i].get_atk_speed() / 4))//let attack image last longer
                    {
                        if (our_soldier[i].get_cycle() == 0)//attack
                        {
                            our_soldier[i].attack();//change to attack picture
                            for(int j=0 ; j<enemy_soldier.Count ; j++)//find the enemy whose position will be attacked
                            {
                                if(enemy_soldier[j].get_position() == enemy_front)
                                {
                                    enemy_soldier[j].attacked(our_soldier[i].getAttackPower());//enemy lose health
                                    if(enemy_soldier[j].is_dead())
                                    {
                                        enemy_soldier.RemoveAt(j);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else//between attack interval
                    {
                        our_soldier[i].hold();//change picture to stand freeze
                    }
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else if(our_soldier[i].get_position() + our_soldier[i].getMoveAbility() >= 1000 && enemy_soldier.Count == 0)//get to enemy castle and there's no enemy, attack castle
                {
                    if (our_soldier[i].get_cycle() <= (our_soldier[i].get_atk_speed() / 4))//let attack image last longer
                    {
                        our_soldier[i].attack();
                        
                        if(our_soldier[i].get_cycle() == 0)//enemy castle lose health
                            enemy_castle.attacked(our_soldier[i].getAttackPower());
                        if(enemy_castle.is_dead())//destroy enemy castle, win
                        {
                            timer1.Enabled = false;
                            DialogResult d = MessageBox.Show("win","Victory",MessageBoxButtons.OK);
                            if(d == DialogResult.OK)
                            {
                                this.Close();
                            }
                        }
                    }
                    else//between attack interval
                    {
                        our_soldier[i].hold();
                    }
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else//not enough attack range, move
                {
                    our_soldier[i].move();
                    our_soldier[i].set_cycle(0);
                    our_soldier[i].pic.Left += our_soldier[i].getMoveAbility();//move soldier
                    our_soldier[i].set_position(our_soldier[i].get_position() + our_soldier[i].getMoveAbility());//update solier's position value
                    set_our_front();//update first soldier's location
                }
            }

            for (int i = 0; i < enemy_soldier.Count ; i++)//enemy's moving time
            {
                if (enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility() <= our_front + size && our_soldier.Count != 0)//enough attack range
                {
                    if (enemy_soldier[i].get_cycle() <= (enemy_soldier[i].get_atk_speed() / 4))//let attack image last longer
                    {
                        if (enemy_soldier[i].get_cycle() == 0)
                        {
                            enemy_soldier[i].attack();
                            for (int j = 0; j < our_soldier.Count; j++)
                            {
                                if (our_soldier[j].get_position() == our_front)
                                {
                                    our_soldier[j].attacked(enemy_soldier[i].getAttackPower());
                                    if (our_soldier[j].is_dead())
                                    {
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
                else if(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility() <= 200 && our_soldier.Count == 0)//attack castle
                {
                    if (enemy_soldier[i].get_cycle() <= (enemy_soldier[i].get_atk_speed() / 4))
                    {
                        enemy_soldier[i].attack();
                        
                        if(enemy_soldier[i].get_cycle() == 0)
                            our_castle.attacked(enemy_soldier[i].getAttackPower());
                        if(our_castle.is_dead())
                        {
                            timer1.Enabled = false;
                            DialogResult d = MessageBox.Show("lose","Defeat",MessageBoxButtons.OK);
                            if(d == DialogResult.OK)
                            {
                                this.Close();
                            }
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
                    enemy_soldier[i].set_position(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility());
                    set_enemy_front();
                }
            
            }
        }

        private void button6_Click(object sender, EventArgs e)//for test
        {
            Soldier temp = new Soldier(1, 4, this);
            enemy_soldier.Add(temp);
        }
    }
}
