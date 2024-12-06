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
    public class Ranged : Enemy
    {
        //Fields
        private int aimRange = 10;

        //Constructor
        public Ranged(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Ranged(Vector2 position) : base(position)
        {
            Position = position;
            MaxHealth = 10;
            Health = 10;
            speed = 150;
        }

        public override void LoadContent(ContentManager content)
        {
            DamageRange = new DamageRange(2, 5);
            sprites[SpriteType.Standard] = content.Load<Texture2D>("notexture");
            sprites[SpriteType.ChargeAttack] = content.Load<Texture2D>("notexture");
            sprites[SpriteType.Attack] = content.Load<Texture2D>("notexture");
            sprites[SpriteType.Hurt] = content.Load<Texture2D>("notexture");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            velocity = Vector2.Zero;
            moveCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moveCooldown <= 0)
            {
                moveCooldown = 0;
            }

            if (attackCooldown <= 0)
            {
                attackCooldown = 0;
            }

            if (GameWorld.Player.Position.Y > position.Y+aimRange|| GameWorld.Player.Position.Y < position.Y - aimRange) 
            {
                Chase();
                if (GameWorld.Player.Position.X > position.X) 
                { 
                    IsFacingRight = true;
                }
                else if(GameWorld.Player.Position.X < position.X)
                {
                    IsFacingRight = false; 
                }
            }

            if (attackCooldown <= 0&& GameWorld.Player.Position.Y > position.Y - aimRange&& GameWorld.Player.Position.Y < position.Y + aimRange) 
            {
                RangedAttack(gameTime);
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
        /// Override of the chase method, which makes this enemy only move up & down
        /// </summary>
        public override void Chase()
        {
            if (moveCooldown <= 0)
            {
                Vector2 direction = new Vector2(GameWorld.Player.Position.X-position.Y, GameWorld.Player.Position.Y - position.Y);
                double test = Math.Atan2(direction.Y, direction.X);
                float YDirection = (float)Math.Sin(test);
                direction = new Vector2(0, YDirection);
                velocity = (direction);
                velocity.Normalize();
            }
        }

        /// <summary>
        /// Runs the attack and resets attackCooldown
        /// </summary>
        /// <param name="gameTime"></param>
        public void RangedAttack(GameTime gameTime)
        {
            Attack();
            attackCooldown = 2;
        }



    }
}
