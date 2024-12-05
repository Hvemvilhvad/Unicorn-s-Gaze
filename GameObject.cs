using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Unicorns_Gaze.states;

namespace Unicorns_Gaze
{
    public enum SpriteType
    {
        None=0,
        Standard=1,
        ChargeAttack=2,
        Attack=3,
        Hurt=4,
    }

    public abstract class GameObject
    {
        //Fields
        private Texture2D sprite;
        protected Dictionary<SpriteType, Texture2D> Sprites;
        private Rectangle hitbox;
        protected Vector2 position;
        protected Vector2 origin;
        protected Vector2 velocity;
        protected bool enteredField;
        protected Color color = Color.White;
        protected float scale = 1;
        //layer on which the sprite is drawn (higher means further back)
        protected float layer;
        protected bool doDynamicLayer = true;
        private float height = 0;
        protected bool doShadow;
        protected float invincibilityFrames = 0.3f;
        protected float invincibilityTimer;
        protected bool takingDamage = false;
        protected float hurtTimer;
        protected float hurtTime = 0.15f;
        protected float depth;

        //Properties
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        public Vector2 Position
        {
            get => position;
            set { position = value; Hitbox = new Rectangle((int)(value.X - (Hitbox.Width / 2) * scale), (int)(((value.Y + Height) - (Hitbox.Height / 2)) * scale), (int)(Hitbox.Width * scale), (int)(Hitbox.Height * scale)); }
        }
        public float Height { get => height; set => height = value; }
        public Texture2D Sprite { get => sprite; private set => sprite = value; }

        

        public GameObject()
        {
            doShadow = false;
        }


        //Methods
        /// <summary>
        /// Checks whether hitboxes are colliding
        /// </summary>
        /// <param name="other"></param>
        public void CheckCollision(GameObject other)
        {
            if (this is not Background && Hitbox.Intersects(other.Hitbox))
            {
                OnCollision(other);
            }

        }

        public virtual bool OnCollision(GameObject other)
        {
            if (Math.Abs(layer - other.layer) > 0.2F | this == other | other is Background)
            {
                return false;
            }
            return true;
        }

        public virtual void LoadContent(ContentManager content)
        {
            UpdateSprite();
            origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Hitbox = new Rectangle((int)position.X - (int)((Sprite.Width / 2) * scale), (int)position.Y - (int)((Sprite.Height / 2) * scale), (int)(Sprite.Width * scale), (int)(Sprite.Height * scale));

        }

        public virtual void Update(GameTime gameTime, Vector2 screenSize)
        {
            depth = Position.Y / 864;
            if (this is not Background)
            {
                CheckBounds(screenSize);
            }

            if (Position.X > 0 && Position.X < GameWorld.ScreenSize.X && Position.Y > Gameplay.TopBoundary && position.Y < Gameplay.BottomBoundary && !enteredField)
            {
                enteredField = true;
            }

            invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (invincibilityTimer <= 0 && takingDamage)
            {
                takingDamage = false;
            }
            if (takingDamage)
            {
                color = Color.Red;
            }
            else if (this is not Button)
            {
                color = Color.White;
            }

            if (doDynamicLayer)
            {
                layer = Math.Abs(1 - (Gameplay.TopBoundary - Position.Y) / (Gameplay.TopBoundary - Gameplay.BottomBoundary));
            }
        }

        /// <summary>
        /// Keeps the gameObject on the designated "street"
        /// </summary>
        public virtual void CheckBounds(Vector2 screenSize)
        {
            if (position.Y - (Sprite.Height / 2) < Gameplay.TopBoundary && enteredField)
            {
                position = new Vector2(position.X, Gameplay.TopBoundary + (Sprite.Height / 2));
                velocity.Y = 0;
            }

            if (position.Y + (Sprite.Height / 2) > Gameplay.BottomBoundary && enteredField)
            {
                position = new Vector2(position.X, Gameplay.BottomBoundary - (Sprite.Height / 2));
                velocity.Y = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite is null)
            {
                Sprite = GameWorld.NoSprite;
            }
            spriteBatch.Draw(Sprite, position + new Vector2(0, Height), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, layer);
            DrawShadow(spriteBatch);
        }

        /// <summary>
        /// Draws a shadow beneath the object if needed.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawShadow(SpriteBatch spriteBatch)
        {
            if (doShadow)
            {
                spriteBatch.Draw(GameWorld.ShadowSprite, position + new Vector2(0, Sprite.Height / 2), null, color, 0, origin = new Vector2(GameWorld.ShadowSprite.Width / 2, GameWorld.ShadowSprite.Height / 2), scale, SpriteEffects.None, layer + 0.0001F);
            }
        }

        /// <summary>
        /// Removes this object from the GameWorld.
        /// </summary>
        public void RemoveThis()
        {
            GameWorld.ActiveGameWorld.RemoveObject(this);
        }

        public virtual void GiveInvincibilityFrames(float invincibilityTime = 0)
        {
            invincibilityTimer = invincibilityTime == 0 ? invincibilityFrames : invincibilityTime;
        }

        public float Distance(GameObject other)
        {
            Vector2 difference = other.Position - Position;
            return (float)Math.Sqrt(Math.Pow(difference.X, 2) + Math.Pow(difference.Y, 2));
        }


        protected void UpdateSprite()
        {
            if (animations.Length == 0)
            {
                Sprite = GameWorld.NoSprite;
            }
            else
            {
                Sprite = animations[selectedAnimation][animationIndex];
            }
        }




    }

}
