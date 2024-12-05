using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Enemy : Character
    {
        //Fields
        protected Texture2D rangedAttackSprite;
        protected float moveCooldown;
        protected DamageRange baseDamageRange;
        protected DamageRange buffedDamageRange;
        private Mage buffingMage;

        public bool BeingBuffed { get => beingBuffed; set => beingBuffed = value; }
        //Constructor
        public Enemy(int health, Vector2 position, float speed)
        {
            MaxHealth = 10;
            NormalHealth = health;
            Position = position;
            this.speed = speed;
        }

        public Enemy(Vector2 position)
        {
            Position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            rangedAttackSprite = content.Load<Texture2D>("notexture");
            baseDamageRange = damageRange;
            buffedDamageRange=new DamageRange(baseDamageRange.LowerBound +2, baseDamageRange.UpperBound +2);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (beingBuffed)
            {
                Health = (int)((MaxHealth*1.5f)-(MaxHealth-NormalHealth));
                damageRange = buffedDamageRange;
                //only used to mark buff, remove if another marker is used
                normalColor = Color.Blue;

                //if mage is killed or out of range
                if (buffingMage == null|| buffingMage.Position.X - position.X > 300 || buffingMage.Position.Y - position.Y > 300)
                {
                    beingBuffed = false;
                }
            }
            else
            {
                Health = NormalHealth;
                DamageRange = baseDamageRange;
                normalColor = Color.White;
            }
            depth = Position.Y / 864;
            Move(gameTime, screenSize);
            Stun();

            base.Update(gameTime, screenSize);
        }

        //Methods
        /// <summary>
        /// Instantiates the enemies attack
        /// </summary>
        public virtual void Attack()
        {
            MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(), false, IsFacingRight, false, attackSprite, 1);
            attackCooldown = attack.ExistanceTime + attack.Cooldown;
            GameWorld.GameObjectsToAdd.Add(attack);
        }

        /// <summary>
        /// Instantiates enemy ranged attack
        /// </summary>
        public virtual void RangedAttack()
        {
            Projectile projectile = new Projectile(position, DamageRange.GetADamageValue(), false, IsFacingRight, false, rangedAttackSprite, 1.5f);
            attackCooldown = projectile.Cooldown;
            GameWorld.GameObjectsToAdd.Add(projectile);
        }
        
        /// <summary>
        /// Allows enemies to move in the direction of the player
        /// </summary>
        public virtual void Chase()
        {
            Vector2 direction = new Vector2(GameWorld.Player.Position.X - position.X, GameWorld.Player.Position.Y - position.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
            velocity.Normalize();
            isFacingRight = velocity.X >= 0;
        }

        /// <summary>
        /// Checks if the enemy is taking damage, and delays their own attack
        /// </summary>
        public void Stun()
        {
            if (takingDamage)
            {
                attackCooldown = 1;
                moveCooldown = 1;
            }
        }

        /// <summary>
        /// Activates buff effect on enemy
        /// </summary>
        /// <param name="mage"></param>
        public virtual void BuffEnemy(Mage mage)
        {
            //maybe add effect here?
            beingBuffed = true;
            buffingMage = mage;
        }
    }
}
