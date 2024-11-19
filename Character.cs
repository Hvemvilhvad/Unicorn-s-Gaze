using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Character : GameObject
    {

        public void Heal(int healedHP)
        {
            Health += healedHP;
        }
    }
}
