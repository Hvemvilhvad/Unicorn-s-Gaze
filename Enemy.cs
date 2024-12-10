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
        private static Texture2D[] rangedAttackSprites;
        protected float moveCooldown;
        protected DamageRange baseDamageRange;
        protected DamageRange buffedDamageRange;
        private Mage buffingMage;

        public bool BeingBuffed { get => beingBuffed; set => beingBuffed = value; }
        private static Texture2D RangedAttackSprite { get => rangedAttackSprites[GameWorld.Random.Next(0, rangedAttackSprites.Length)]; }

        //Constructor
        public Enemy(int health, Vector2 position, float speed)
        {
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
            string fileName = "Gumballer/Projektile/gumball ";
            rangedAttackSprites = new Texture2D[]
            {
                content.Load<Texture2D>(fileName + "yellow"),
                content.Load<Texture2D>(fileName + "blue"),
                content.Load<Texture2D>(fileName + "red"),
                content.Load<Texture2D>(fileName + "green"),
                content.Load<Texture2D>(fileName + "pink"),
            };
            baseDamageRange = damageRange;
            buffedDamageRange = new DamageRange(baseDamageRange.LowerBound + 2, baseDamageRange.UpperBound + 2);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (beingBuffed)
            {
                Health = (int)((MaxHealth * 1.5f) - (MaxHealth - NormalHealth));
                damageRange = buffedDamageRange;
                //only used to mark buff, remove if another marker is used
                normalColor = Color.LightBlue;

                bool mageAlive = false;
                foreach (GameObject item in GameWorld.GameObjects)
                {
                    if (item == buffingMage)
                    {
                        mageAlive = true;
                        break;
                    }
                }

                //if mage is killed or out of range
                if (!mageAlive || buffingMage.Position.X - position.X > 300 || buffingMage.Position.Y - position.Y > 300)
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

            if (GameWorld.Player.Position.X > Position.X)
            {
                IsFacingRight = true;
            }
            else if (GameWorld.Player.Position.X < Position.X)
            {
                IsFacingRight = false;
            }

            base.Update(gameTime, screenSize);
        }

        //Methods
        /// <summary>
        /// Instantiates the enemies attack
        /// </summary>
        public virtual void Attack()
        {
            MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(), false, IsFacingRight, false, attackSprite, 1, false);
            attackCooldown = attack.ExistanceTime + attack.Cooldown;
            attackTime = attack.ExistanceTime;
            GameWorld.GameObjectsToAdd.Add(attack);
            spriteType = SpriteType.Attack;
        }

        /// <summary>
        /// Instantiates enemy ranged attack
        /// </summary>
        public virtual void RangedAttack()
        {
            Projectile projectile = new Projectile(Position + new Vector2(-40,20), DamageRange.GetADamageValue(), false, IsFacingRight, false, RangedAttackSprite, 1.5f);
            attackCooldown = projectile.Cooldown;
            GameWorld.GameObjectsToAdd.Add(projectile);
            spriteType = SpriteType.Attack;
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
            spriteType = SpriteType.Standard;
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
                spriteType = SpriteType.Hurt;
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
