using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Brute : Enemy
    {
        public Brute(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Position = position;
            Health = health;
            this.speed = speed;
        }

        public Brute(Vector2 position) : base(position)
        {
            Position = position;
            MaxHealth = 20;
            NormalHealth = 20;
            Health = 20;
            speed = 100;
            scale = 0.2F;
        }

        public override void LoadContent(ContentManager content)
        {
            DamageRange = new DamageRange(4, 8);
            string fileName = "Marshmellow/marshmallow ";
            sprites[SpriteType.Standard] = content.Load<Texture2D>(fileName + "idle");
            sprites[SpriteType.ChargeAttack] = content.Load<Texture2D>(fileName + "ready");
            sprites[SpriteType.Attack] = content.Load<Texture2D>(fileName + "hit");
            sprites[SpriteType.Hurt] = content.Load<Texture2D>(fileName + "attacked");


            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Chase();
            moveCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moveCooldown <= 0)
            {
                moveCooldown = 0;
            }

            //Checks the distance between the player and the enemy, and runs appropriate methods
            float distance = Distance(GameWorld.Player);
            if (distance <= 180)
            {
                moveCooldown = 2;
                attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity = Vector2.Zero;
                if (attackCooldown <= 0)
                {
                    BruteAttack(gameTime);
                }

                if (attackTime <= 0 & !TakingDamage)
                {
                    spriteType = SpriteType.ChargeAttack;
                }
            }

            if (attackCooldown <= 0)
            {
                attackCooldown = 0;
            }



            base.Update(gameTime, screenSize);
        }

        public override bool OnCollision(GameObject other)
        {
            if (base.OnCollision(other) & other is Player)
            {
                velocity = Vector2.Zero;
                moveCooldown = 2;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Override of the chase method, allowing the enemy to stop if it collides with the player
        /// </summary>
        public override void Chase()
        {
            if (moveCooldown <= 0)
            {
                base.Chase();
            }
        }

        /// <summary>
        /// Runs the attack and resets attackCooldown
        /// </summary>
        /// <param name="gameTime"></param>
        public void BruteAttack(GameTime gameTime)
        {
            Attack();
            attackCooldown = 2;
        }
    }
}

