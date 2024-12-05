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
        private int walkState;
        private float walkStateUpdateCountdown;
        private float spriteRotation;
        private float spriteYOffset;
        private bool doWalkAnimation;


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
        public float InvincibilityFrames { get => invincibilityFrames; set => invincibilityFrames = value; }
        public float HurtTimer { get => hurtTimer; set => hurtTimer = value; }
        public float HurtTime { get => hurtTime; set => hurtTime = value; }
        public bool TakingDamage { get => takingDamage; set => takingDamage = value; }
        public bool IsFacingRight { get => isFacingRight; protected set => isFacingRight = value; }
        protected int WalkState
        {
            get => walkState;
            set
            {
                if (value > 4)
                {
                    walkState = 1;
                }
                else
                {
                    walkState = value;
                }
            }
        }
        public float WalkStateUpdateCountdown
        {
            get => walkStateUpdateCountdown;
            set
            {
                if (value <= 0)
                {
                    WalkStateUpdateCountdown = 0.15F;
                    NextWalkState();
                }
                else
                {
                    walkStateUpdateCountdown = value;
                }

            }
        }


        public Character()
        {
            doShadow = true;
        }

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
        /// Changes the state of the bobbing when the character is moving.
        /// </summary>
        private void NextWalkState()
        {
            WalkState++;
            spriteRotation = ((float)GameWorld.Random.NextDouble() / 4F) - 0.125F;
            spriteYOffset = 0;
            switch (walkState)
            {
                case 1:
                    spriteRotation -= 0.25F;
                    break;
                case 3:
                    spriteRotation += 0.25F;
                    break;
                default:
                    spriteYOffset = -15;
                    break;
            }
        }

        /// <summary>
        /// Defines movement for character classes
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        protected void Move(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 change = ((velocity * speed) * deltaTime);
            Position += change;

            doWalkAnimation = change != Vector2.Zero;
            if (doWalkAnimation)
            {
                WalkStateUpdateCountdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
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
            if (Sprite is null)
            {
                Sprite = GameWorld.NoSprite;
            }
            SpriteEffects flip = isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 offset = doWalkAnimation ? new Vector2(0, spriteYOffset) : Vector2.Zero;
            float rotation = doWalkAnimation ? spriteRotation : 0;
            spriteBatch.Draw(Sprite, position + offset, null, Color.White, rotation, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), 1, flip, layer);
            DrawShadow(spriteBatch);
        }

    }
}
