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
    public abstract class Character : GameObject
    {
        //Fields
        protected float speed;
        private int health;
        private int maxHealth;
        private DamageRange damageRange;
        protected bool isFacingRight;
        protected Texture2D attackSprite;


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
        public DamageRange DamageRange { get => damageRange; set => damageRange = value; }


        //Methods
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            attackSprite = content.Load<Texture2D>("tile_arara_azul");
        }

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

        public void TakeDamage(int damage)
        {

        }
    }
}
