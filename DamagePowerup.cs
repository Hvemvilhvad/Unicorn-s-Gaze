using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class DamagePowerup : Powerup
    {


        public override void Use()
        {
            base.Use();
            GameWorld.Player.DamageRange = GameWorld.Player.DamageRange.OffsetDamageRange(10);
        }
    }
}
