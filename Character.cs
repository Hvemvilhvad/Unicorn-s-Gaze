using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public abstract class Character : GameObject
    {
        //Fields
        protected float speed;
        private int health;
        private int maxHealth;


        //Properties
        /// <summary>
        /// Checks if MaxHealth is exceeded
        /// </summary>
        public virtual int Health 
        { 
            get => health;
            set
            {
                if (value < 0)
                {
                    health = 0;
                }
                else if (value > MaxHealth)
                {
                    health = 10;
                }
                else
                {
                    health = value;
                }
            }
        }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }

        //Methods
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
        }

        /// <summary>
        /// Defines movement for character classes
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        protected void Move(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += ((velocity * speed) * deltaTime);
        }
        /// <summary>
        /// Method used for healing items
        /// </summary>
        /// <param name="healedHP"></param>
        public void Heal(int healedHP)
        {
            Health += healedHP;
        }
    }
}
