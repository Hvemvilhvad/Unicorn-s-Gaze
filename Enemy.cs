using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Enemy : Character
    {
        //Fields
        private DamageRange enemyRange;

        public virtual void Attack()
        {
            MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(), false, isFacingRight, false, attackSprite);
            GameWorld.GameObjectsToAdd.Add(attack);
        }

    }
}
