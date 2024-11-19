using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class HealthPickup : Item
    {
        private int HealAmount;

        public HealthPickup()
        {
            HealAmount = 7;
        }

        public override void Use()
        {
            GameWorld.Player.Heal(HealAmount);
        }
    }
}
