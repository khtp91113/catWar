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
                if (our_soldier[i].get_position() + our_soldier[i].getMoveAbility() + size >= enemy_front && enemy_soldier.Count != 0)//soldier can attack at least one enemy
                {
                    //if(our_soldier[i].get_position() + size < enemy_front)
                    //    our_soldier[i].pic.Left = enemy_front - size;
                    
                    if (our_soldier[i].get_cycle() <= our_soldier[i].get_atk_speed())//let attack image last longer
                    {
                        if (our_soldier[i].get_cycle() < our_soldier[i].getAtkFrame())//attack
                        {
                            Image temp;
                            if(our_soldier[i].getKind() == 1)
                            {
                                switch (our_soldier[i].get_cycle() % 4)
                                {
                                    case 0: temp = Resource1.ch1attack1; break;
                                    case 1: temp = Resource1.ch1attack2; break;
                                    case 2: temp = Resource1.ch1attack3; break;
                                    default: temp = Resource1.ch1attack4; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 2)
                            {
                                switch (our_soldier[i].get_cycle() % 7)
                                {
                                    case 0: temp = Resource1.ch2attack1; break;
                                    case 1: temp = Resource1.ch2attack2; break;
                                    case 2: temp = Resource1.ch2attack3; break;
                                    case 3: temp = Resource1.ch2attack4; break;
                                    case 4: temp = Resource1.ch2attack5; break;
                                    case 5: temp = Resource1.ch2attack6; break;
                                    default: temp = Resource1.ch2attack7; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 3)
                            {
                                switch (our_soldier[i].get_cycle() % 8)
                                {
                                    case 0: temp = Resource1.ch3attack1; break;
                                    case 1: temp = Resource1.ch3attack2; break;
                                    case 2: temp = Resource1.ch3attack3; break;
                                    case 3: temp = Resource1.ch3attack4; break;
                                    case 4: temp = Resource1.ch3attack5; break;
                                    case 5: temp = Resource1.ch3attack6; break;
                                    case 6: temp = Resource1.ch3attack7; break;
                                    default: temp = Resource1.ch3attack8; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 4)
                            {
                                switch (our_soldier[i].get_cycle() % 9)
                                {
                                    case 0: temp = Resource1.ch4attack1; break;
                                    case 1: temp = Resource1.ch4attack2; break;
                                    case 2: temp = Resource1.ch4attack3; break;
                                    case 3: temp = Resource1.ch4attack4; break;
                                    case 4: temp = Resource1.ch4attack5; break;
                                    case 5: temp = Resource1.ch4attack6; break;
                                    case 6: temp = Resource1.ch4attack7; break;
                                    case 7: temp = Resource1.ch4attack8; break;
                                    default: temp = Resource1.ch4attack9; break;
                                }
                            }
                            else
                            {
                                switch (our_soldier[i].get_cycle() % 9)
                                {
                                    case 0: temp = Resource1.ch5attack1; break;
                                    case 1: temp = Resource1.ch5attack2; break;
                                    case 2: temp = Resource1.ch5attack3; break;
                                    case 3: temp = Resource1.ch5attack4; break;
                                    case 4: temp = Resource1.ch5attack5; break;
                                    case 5: temp = Resource1.ch5attack6; break;
                                    case 6: temp = Resource1.ch5attack7; break;
                                    case 7: temp = Resource1.ch5attack8; break;
                                    case 8: temp = Resource1.ch5attack9; break;
                                    case 9: temp = Resource1.ch5attack10; break;
                                    case 10: temp = Resource1.ch5attack11; break;
                                    case 11: temp = Resource1.ch5attack12; break;
                                    case 12: temp = Resource1.ch5attack13; break;
                                    case 13: temp = Resource1.ch5attack14; break;
                                    default: temp = Resource1.ch5attack15; break;
                                }
                            }
                            
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
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
                            Image temp;
                            switch (our_soldier[i].getKind())
                            {
                                case 1: temp = Resource1.ch1attack1; break;
                                case 2: temp = Resource1.ch2attack1; break;
                                case 3: temp = Resource1.ch3attack1; break;
                                case 4: temp = Resource1.ch4attack1; break;
                                default: temp = Resource1.ch5attack1; break;
                            }
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                    }
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else if(our_soldier[i].get_position() + our_soldier[i].getMoveAbility() + size >= enemyCastleStart - 1 && enemy_soldier.Count == 0)//get to enemy castle and there's no enemy, attack castle
                {
                    if (our_soldier[i].get_cycle() <= our_soldier[i].get_atk_speed())//let attack image last longer
                    {
                        if (our_soldier[i].get_cycle() <= our_soldier[i].getAtkFrame())//attack
                        {
                            Image temp;
                            if (our_soldier[i].getKind() == 1)
                            {
                                switch (our_soldier[i].get_cycle() % 4)
                                {
                                    case 0: temp = Resource1.ch1attack1; break;
                                    case 1: temp = Resource1.ch1attack2; break;
                                    case 2: temp = Resource1.ch1attack3; break;
                                    default: temp = Resource1.ch1attack4; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 2)
                            {
                                switch (our_soldier[i].get_cycle() % 7)
                                {
                                    case 0: temp = Resource1.ch2attack1; break;
                                    case 1: temp = Resource1.ch2attack2; break;
                                    case 2: temp = Resource1.ch2attack3; break;
                                    case 3: temp = Resource1.ch2attack4; break;
                                    case 4: temp = Resource1.ch2attack5; break;
                                    case 5: temp = Resource1.ch2attack6; break;
                                    default: temp = Resource1.ch2attack7; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 3)
                            {
                                switch (our_soldier[i].get_cycle() % 8)
                                {
                                    case 0: temp = Resource1.ch3attack1; break;
                                    case 1: temp = Resource1.ch3attack2; break;
                                    case 2: temp = Resource1.ch3attack3; break;
                                    case 3: temp = Resource1.ch3attack4; break;
                                    case 4: temp = Resource1.ch3attack5; break;
                                    case 5: temp = Resource1.ch3attack6; break;
                                    case 6: temp = Resource1.ch3attack7; break;
                                    default: temp = Resource1.ch3attack8; break;
                                }
                            }
                            else if (our_soldier[i].getKind() == 4)
                            {
                                switch (our_soldier[i].get_cycle() % 9)
                                {
                                    case 0: temp = Resource1.ch4attack1; break;
                                    case 1: temp = Resource1.ch4attack2; break;
                                    case 2: temp = Resource1.ch4attack3; break;
                                    case 3: temp = Resource1.ch4attack4; break;
                                    case 4: temp = Resource1.ch4attack5; break;
                                    case 5: temp = Resource1.ch4attack6; break;
                                    case 6: temp = Resource1.ch4attack7; break;
                                    case 7: temp = Resource1.ch4attack8; break;
                                    default: temp = Resource1.ch4attack9; break;
                                }
                                Console.WriteLine(our_soldier[i].get_cycle() % 9);
                            }
                            else
                            {
                                switch (our_soldier[i].get_cycle() % 9)
                                {
                                    case 0: temp = Resource1.ch5attack1; break;
                                    case 1: temp = Resource1.ch5attack2; break;
                                    case 2: temp = Resource1.ch5attack3; break;
                                    case 3: temp = Resource1.ch5attack4; break;
                                    case 4: temp = Resource1.ch5attack5; break;
                                    case 5: temp = Resource1.ch5attack6; break;
                                    case 6: temp = Resource1.ch5attack7; break;
                                    case 7: temp = Resource1.ch5attack8; break;
                                    case 8: temp = Resource1.ch5attack9; break;
                                    case 9: temp = Resource1.ch5attack10; break;
                                    case 10: temp = Resource1.ch5attack11; break;
                                    case 11: temp = Resource1.ch5attack12; break;
                                    case 12: temp = Resource1.ch5attack13; break;
                                    case 13: temp = Resource1.ch5attack14; break;
                                    default: temp = Resource1.ch5attack15; break;
                                }
                            }
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                        else
                        {
                            Image temp;
                            switch (our_soldier[i].getKind())
                            {
                                case 1: temp = Resource1.ch1attack1; break;
                                case 2: temp = Resource1.ch2attack1; break;
                                case 3: temp = Resource1.ch3attack1; break;
                                case 4: temp = Resource1.ch4attack1; break;
                                default: temp = Resource1.ch5attack1; break;
                            }
                            g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                        our_soldier[i].attack();
                        
                        if (our_soldier[i].get_cycle() == 0 && mode != 5)//enemy castle lose health
                            enemy_castle.attacked(our_soldier[i].getAttackPower());

                        if (our_soldier[i].get_cycle() == 0 && mode == 5)//enemy castle lose health
                        {
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
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle() + 1);
                }
                else//not enough attack range, move
                {
                    our_soldier[i].move();
                    our_soldier[i].set_cycle(our_soldier[i].get_cycle()+1);
                    //our_soldier[i].bar.Left = our_soldier[i].pic.Left;
                    our_soldier[i].set_position(our_soldier[i].get_position() + our_soldier[i].getMoveAbility());//update solier's position value
                    set_our_front();//update first soldier's location
                    Image temp;
                    if(our_soldier[i].getKind() == 1)
                    {
                        switch(our_soldier[i].get_cycle() % our_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.ch1move1; break;
                            case 1: temp = Resource1.ch1move2; break;
                            case 2: temp = Resource1.ch1move3; break;
                            default: temp = Resource1.ch1move4; break;
                        }
                    }
                    else if (our_soldier[i].getKind() == 2)
                    {
                        switch (our_soldier[i].get_cycle() % our_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.ch2move1; break;
                            case 1: temp = Resource1.ch2move2; break;
                            case 2: temp = Resource1.ch2move3; break;
                            default: temp = Resource1.ch2move4; break;
                        }
                    }
                    else if (our_soldier[i].getKind() == 3)
                    {
                        switch (our_soldier[i].get_cycle() % our_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.ch3move1; break;
                            case 1: temp = Resource1.ch3move2; break;
                            case 2: temp = Resource1.ch3move3; break;
                            default: temp = Resource1.ch3move4; break;
                        }
                    }
                    else if (our_soldier[i].getKind() == 4)
                    {
                        switch (our_soldier[i].get_cycle() % our_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.ch4move1; break;
                            case 1: temp = Resource1.ch4move2; break;
                            case 2: temp = Resource1.ch4move3; break;
                            case 3: temp = Resource1.ch4move4; break;
                            case 4: temp = Resource1.ch4move5; break;
                            default: temp = Resource1.ch4move6; break;
                        }
                    }
                    else
                    {
                        switch (our_soldier[i].get_cycle() % our_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.ch5move1; break;
                            case 1: temp = Resource1.ch5move2; break;
                            case 2: temp = Resource1.ch5move3; break;
                            default: temp = Resource1.ch5move4; break;
                        }
                    }
                    g.DrawImage(temp, new Rectangle(new Point(our_soldier[i].get_position(), 368), new Size(100, 100)));
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
                    if (enemy_soldier[i].get_cycle() <= enemy_soldier[i].get_atk_speed())//let attack image last longer
                    {
                        if (enemy_soldier[i].get_cycle() < enemy_soldier[i].getAtkFrame())
                        {
                            Image temp;
                            if (enemy_soldier[i].getKind() == 6)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m1attack1; break;
                                    case 1: temp = Resource1.m1attack2; break;
                                    case 2: temp = Resource1.m1attack3; break;
                                    case 3: temp = Resource1.m1attack4; break;
                                    default: temp = Resource1.m1attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 7)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m2attack1; break;
                                    case 1: temp = Resource1.m2attack2; break;
                                    case 2: temp = Resource1.m2attack3; break;
                                    case 3: temp = Resource1.m2attack4; break;
                                    default: temp = Resource1.m2attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 8)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m3attack1; break;
                                    case 1: temp = Resource1.m3attack2; break;
                                    case 2: temp = Resource1.m3attack3; break;
                                    case 3: temp = Resource1.m3attack4; break;
                                    default: temp = Resource1.m3attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 9)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m4attack1; break;
                                    case 1: temp = Resource1.m4attack2; break;
                                    case 2: temp = Resource1.m4attack3; break;
                                    case 3: temp = Resource1.m4attack4; break;
                                    default: temp = Resource1.m4attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 10)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m5attack1; break;
                                    case 1: temp = Resource1.m5attack2; break;
                                    case 2: temp = Resource1.m5attack3; break;
                                    case 3: temp = Resource1.m5attack4; break;
                                    default: temp = Resource1.m5attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 6)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m6attack1; break;
                                    case 1: temp = Resource1.m6attack2; break;
                                    case 2: temp = Resource1.m6attack3; break;
                                    case 3: temp = Resource1.m6attack4; break;
                                    default: temp = Resource1.m6attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 7)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m7attack1; break;
                                    case 1: temp = Resource1.m7attack2; break;
                                    case 2: temp = Resource1.m7attack3; break;
                                    case 3: temp = Resource1.m7attack4; break;
                                    default: temp = Resource1.m7attack5; break;
                                }
                            }
                            else
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m8attack1; break;
                                    case 1: temp = Resource1.m8attack2; break;
                                    case 2: temp = Resource1.m8attack3; break;
                                    case 3: temp = Resource1.m8attack4; break;
                                    default: temp = Resource1.m8attack5; break;
                                }
                            }

                            g.DrawImage(temp, new Rectangle(new Point(enemy_soldier[i].get_position(), 368), new Size(100, 100)));


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
                        else
                        {
                            Image temp;
                            switch (enemy_soldier[i].getKind())
                            {
                                case 6: temp = Resource1.m1attack1; break;
                                case 7: temp = Resource1.m2attack1; break;
                                case 8: temp = Resource1.m3attack1; break;
                                case 9: temp = Resource1.m4attack1; break;
                                case 10: temp = Resource1.m5attack1; break;
                                case 11: temp = Resource1.m6attack1; break;
                                case 12: temp = Resource1.m7attack1; break;
                                default: temp = Resource1.m8attack1; break;
                            }
                            g.DrawImage(temp, new Rectangle(new Point(enemy_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                    }
                    /*else//between attack interval
                    {
                        enemy_soldier[i].hold();
                    }*/
                    enemy_soldier[i].set_cycle(enemy_soldier[i].get_cycle() + 1);
                }
                else if(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility() <= ourCastleStart + 1 && our_soldier.Count == 0)//attack castle
                {
                   // if (enemy_soldier[i].get_position() > ourCastleStart + 1)
                     //   enemy_soldier[i].pic.Left = ourCastleStart + 1;
                    if (enemy_soldier[i].get_cycle() <= enemy_soldier[i].get_atk_speed())
                    {
                        if(enemy_soldier[i].get_cycle() <= enemy_soldier[i].getAtkFrame())
                        {
                            Image temp;
                            if (enemy_soldier[i].getKind() == 6)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m1attack1; break;
                                    case 1: temp = Resource1.m1attack2; break;
                                    case 2: temp = Resource1.m1attack3; break;
                                    case 3: temp = Resource1.m1attack4; break;
                                    default: temp = Resource1.m1attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 7)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m2attack1; break;
                                    case 1: temp = Resource1.m2attack2; break;
                                    case 2: temp = Resource1.m2attack3; break;
                                    case 3: temp = Resource1.m2attack4; break;
                                    default: temp = Resource1.m2attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 8)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m3attack1; break;
                                    case 1: temp = Resource1.m3attack2; break;
                                    case 2: temp = Resource1.m3attack3; break;
                                    case 3: temp = Resource1.m3attack4; break;
                                    default: temp = Resource1.m3attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 9)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m4attack1; break;
                                    case 1: temp = Resource1.m4attack2; break;
                                    case 2: temp = Resource1.m4attack3; break;
                                    case 3: temp = Resource1.m4attack4; break;
                                    default: temp = Resource1.m4attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 10)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m5attack1; break;
                                    case 1: temp = Resource1.m5attack2; break;
                                    case 2: temp = Resource1.m5attack3; break;
                                    case 3: temp = Resource1.m5attack4; break;
                                    default: temp = Resource1.m5attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 11)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m6attack1; break;
                                    case 1: temp = Resource1.m6attack2; break;
                                    case 2: temp = Resource1.m6attack3; break;
                                    case 3: temp = Resource1.m6attack4; break;
                                    default: temp = Resource1.m6attack5; break;
                                }
                            }
                            else if (enemy_soldier[i].getKind() == 12)
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m7attack1; break;
                                    case 1: temp = Resource1.m7attack2; break;
                                    case 2: temp = Resource1.m7attack3; break;
                                    case 3: temp = Resource1.m7attack4; break;
                                    default: temp = Resource1.m7attack5; break;
                                }
                            }
                            else
                            {
                                switch (enemy_soldier[i].get_cycle() % 5)
                                {
                                    case 0: temp = Resource1.m8attack1; break;
                                    case 1: temp = Resource1.m8attack2; break;
                                    case 2: temp = Resource1.m8attack3; break;
                                    case 3: temp = Resource1.m8attack4; break;
                                    default: temp = Resource1.m8attack5; break;
                                }
                            }
                            g.DrawImage(temp, new Rectangle(new Point(enemy_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
                        else
                        {
                            Image temp;
                            switch (enemy_soldier[i].getKind())
                            {
                                case 6: temp = Resource1.m1attack1; break;
                                case 7: temp = Resource1.m2attack1; break;
                                case 8: temp = Resource1.m3attack1; break;
                                case 9: temp = Resource1.m4attack1; break;
                                case 10: temp = Resource1.m5attack1; break;
                                case 11: temp = Resource1.m6attack1; break;
                                case 12: temp = Resource1.m7attack1; break;
                                default: temp = Resource1.m8attack1; break;
                            }
                            g.DrawImage(temp, new Rectangle(new Point(enemy_soldier[i].get_position(), 368), new Size(100, 100)));
                        }
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
                    enemy_soldier[i].set_cycle(enemy_soldier[i].get_cycle() + 1);
                    //enemy_soldier[i].bar.Left = enemy_soldier[i].pic.Left;
                    enemy_soldier[i].set_position(enemy_soldier[i].get_position() + enemy_soldier[i].getMoveAbility());
                    set_enemy_front();
                    Image temp;
                    if (enemy_soldier[i].getKind() == 6)
                    {
                        temp = Resource1.m1move1;
                    }
                    else if (enemy_soldier[i].getKind() == 7)
                    {
                        temp = Resource1.m2move1;
                    }
                    else if (enemy_soldier[i].getKind() == 8)
                    {
                        temp = Resource1.m3move1;
                    }
                    else if (enemy_soldier[i].getKind() == 9)
                    {
                        Console.WriteLine(enemy_soldier[i].get_cycle());
                        switch (enemy_soldier[i].get_cycle() % enemy_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.m4move1; break;
                            case 1: temp = Resource1.m4move2; break;
                            default: temp = Resource1.m4move3; break;
                        }
                    }
                    else if (enemy_soldier[i].getKind() == 10)
                    {
                        switch (enemy_soldier[i].get_cycle() % enemy_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.m5move1; break;
                            case 1: temp = Resource1.m5move2; break;
                            default: temp = Resource1.m5move3; break;
                        }
                    }
                    else if (enemy_soldier[i].getKind() == 11)
                    {
                        switch (enemy_soldier[i].get_cycle() % enemy_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.m6move1; break;
                            case 1: temp = Resource1.m6move2; break;
                            default: temp = Resource1.m6move3; break;
                        }
                    }
                    else if (enemy_soldier[i].getKind() == 12)
                    {
                        switch (enemy_soldier[i].get_cycle() % enemy_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.m7move1; break;
                            case 1: temp = Resource1.m7move2; break;
                            default: temp = Resource1.m7move3; break;
                        }
                    }
                    else
                    {
                        switch (enemy_soldier[i].get_cycle() % enemy_soldier[i].getMoveFrame())
                        {
                            case 0: temp = Resource1.m4move1; break;
                            case 1: temp = Resource1.m4move2; break;
                            default: temp = Resource1.m4move3; break;
                        }
                    }
                    g.DrawImage(temp, new Rectangle(new Point(enemy_soldier[i].get_position(), 368), new Size(100, 100)));
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

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

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
