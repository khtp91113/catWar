using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catWar
{
    class Castle
    {
        private int blood = 2000;  //血量

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
}
