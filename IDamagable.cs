﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public interface IDamagable
    {
        public int Health { get; set; }
        public float InvincibilityTimer { get ; set; }
        public float InvincibilityFrames { get; set; }
        public float HurtTimer { get; set; }
        public float HurtTime { get; set; }
        public bool TakingDamage { get; set; }

        public virtual void GiveInvincibilityFrames(float invincibilityTime = 0)
        {
            InvincibilityTimer = invincibilityTime == 0 ? InvincibilityFrames : invincibilityTime;
        }


        /// <summary>
        /// Lowers the health of the Damagable when it takes damage.
        /// </summary>
        /// <param name="damage">The amount to lower it by.</param>
        virtual void TakeDamage(int damage, bool IsMeleeAttack)
        {
            if (InvincibilityTimer <= 0)
            {
                Health -= damage;
                SplashText damageText = new SplashText(damage + " taken", Color.Red, GameWorld.Player);
                GameWorld.MakeObject(damageText);
                if (IsMeleeAttack)
                {
                    GiveInvincibilityFrames();
                }
                TakingDamage = true;
                HurtTimer = HurtTime;
                
                if (Health <= 0)
                {
                    if (this is Enemy || this is Breakable)
                    {
                        GameWorld.ActiveGameWorld.RemoveObject((GameObject)this);
                    }
                    else
                    {
                        GameWorld.ActiveGameWorld.RemoveObject((GameObject)this);
                        GameWorld.IsAlive = false;
                    }
                }
                
                
            }
        }

    }
}
