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
        }
      
        public Grunt(Vector2 position) : base(position)
        {
            MaxHealth = 10;
            Health = 10;
            speed = 150;
            DamageRange = new DamageRange(2, 5);
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[0] = content.Load<Texture2D>("gummybear");
            }
            sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Chase();
            cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldown <= 0)
            {
                cooldown = 0;
            }
            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                velocity = Vector2.Zero;
                cooldown = 2;
                if (attackCooldown <= 0)
                {
                    Attack();
                }
                
            }
            base.OnCollision(other);
        }
        /// <summary>
        /// Override of the chase method, allowing the enemy to stop if it collides with the player
        /// </summary>
        public override void Chase()
        {
            if (cooldown <= 0)
            {
                base.Chase();
            }
        }

    }
}
