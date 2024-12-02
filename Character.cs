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
    public struct DamageRange
    {
        private int lowerBound;
        private int upperBound;

        /// <summary>
        /// A DamageRange is an interval for damage-values.
        /// Its used to track and make damage-values.
        /// </summary>
        /// <param name="lowerBound">The lower inclusive bound of the interval.</param>
        /// <param name="upperBound">The upper inclusive bound of the interval.</param>
        public DamageRange(int lowerBound, int upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }


        /// <summary>
        /// Gets a random damage value.
        /// The randomization has a bellcurve distrubution.
        /// </summary>
        /// <returns>Returns a random damage value.</returns>
        public int GetADamageValue()
        {
            Random random = GameWorld.Random;
            return (int)(random.Next(lowerBound, upperBound + 1) / 2f + random.Next(lowerBound, upperBound + 1) / 2f);
        }

        /// <summary>
        /// Gets a random damage value.
        /// The randomization has a bellcurve distrubution.
        /// </summary>
        /// <param name="critMltplr">Critical multiplier.</param>
        /// <param name="critChace">Critical chance.</param>
        /// <returns>Returns a random damage value.</returns>
        public int GetADamageValue(float critMltplr, byte critChace, out bool isCrit)
        {
            Random random = GameWorld.Random;
            int damage = GetADamageValue();
            isCrit = false;
            if (random.Next(0, 101) <= critChace)
            {
                damage = (int)(damage * critMltplr);
                isCrit = true;
            }
            return damage;
        }


        /// <summary>
        /// Offsets the upper- and lower bound and returns itself.
        /// </summary>
        /// <param name="deltaDamage">The amount that is offset by.</param>
        /// <returns>Returns a copy of this DamageRange with offset bounds</returns>
        public DamageRange OffsetDamageRange(int deltaDamage)
        {
            lowerBound += deltaDamage;
            upperBound += deltaDamage;
            return this;
        }
    }

    public abstract class Character : GameObject, IDamagable
    {
        //Fields
        protected float speed;
        private int health;
        private int maxHealth;
        private DamageRange damageRange;
        protected bool isFacingRight;
        protected Texture2D attackSprite;
        protected float attackCooldown = 0.5f;


        //Properties
        /// <summary>
        /// Checks if MaxHealth is exceeded
        /// </summary>
        public int Health
        {
            get => health;
            set
            {
                if (value < 0)
                {
                    health = 0;
                  
                }
                else if (value >= MaxHealth)
                {
                    health = MaxHealth;
                }
                else
                {
                    health = value;
                }
            }
        }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public DamageRange DamageRange { get => damageRange; set => damageRange = value; }
        public float InvincibilityTimer { get => invincibilityTimer; set => invincibilityTimer = value; }
        public float InvincibilityFrames { get => invincibilityFrames ; set => invincibilityFrames = value; }
        public float HurtTimer { get => hurtTimer; set => hurtTimer = value; }
        public float HurtTime { get => hurtTime; set => hurtTime = value; }
        public bool TakingDamage { get => takingDamage; set => takingDamage = value; }
        public bool IsFacingRight { get => isFacingRight; protected set => isFacingRight = value; }


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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sprite is null)
            {
                sprite = GameWorld.NoSprite;
            }
            SpriteEffects flip = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin = new Vector2(sprite.Width / 2, sprite.Height / 2), 1, flip, layer);
        }

    }
}
