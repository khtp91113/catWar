using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace catWar
{
    class Castle
    {
        private int blood = 2000;  //血量
        private ProgressBar bar;

        public Castle(PictureBox p,int side)
        {
            bar = new ProgressBar();
            bar.Parent = p;
            bar.Maximum = blood;
            bar.Value = blood;
            bar.Width = 136;
            bar.Height = 10;
            if(side == 1)
            {
                bar.Left = 50;
                bar.Top = 170;
            }
            else
            {
                bar.Left = 990;
                bar.Top = 170;
            }
            bar.Show();
        }

        public void attacked(int x) //被攻擊
        {
            blood -= x;
            if (blood < 0)
                bar.Value = 0;
            else
                bar.Value = blood;
        }

        public bool is_dead() //死亡
        {
            if (blood <= 0)
                return true;
            return false;
        }
    }
}
