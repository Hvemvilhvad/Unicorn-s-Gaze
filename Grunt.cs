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
    public class Grunt : Enemy
    {
        //Fields
        

        //Constructor
        public Grunt(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
            attackCooldown = 2;
            scale = 0.2F;
        }
      
        public Grunt(Vector2 position) : base(position)
        {
            MaxHealth = 10;
            NormalHealth = 10;
            Health = 10;
            speed = 150;
            DamageRange = new DamageRange(2, 5);
            attackCooldown = 1;
            scale = 0.2F;
        }

        public override void LoadContent(ContentManager content)
        {
            DamageRange = new DamageRange(2, 5);
            string[] colours = new string[] { "yellow", "blue", "red", "green", "pink" };
            string colour = colours[GameWorld.Random.Next(0, colours.Length)];
            string fileName = "Gummybear/gummy bear ";

            sprites[SpriteType.Standard] = content.Load<Texture2D>(fileName + colour);
            sprites[SpriteType.ChargeAttack] = content.Load<Texture2D>(fileName + "ready " + colour);
            sprites[SpriteType.Attack] = content.Load<Texture2D>(fileName + "hit " + colour);
            sprites[SpriteType.Hurt] = content.Load<Texture2D>(fileName + "attacked " + colour);
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
            if (distance <= 150)
            {
                moveCooldown = 2;
                attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity = Vector2.Zero;
                if (attackCooldown <= 0)
                {
                    GruntAttack(gameTime);
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
            if (base.OnCollision(other))
            {
                if (other is Player)
                {
                    velocity = Vector2.Zero;
                    moveCooldown = 1;
                    return true;
                }
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
        public void GruntAttack(GameTime gameTime)
        {
            Attack();
            attackCooldown = 1;
        }

        

    }
}
