using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public abstract class GameObject
    {
        //Fields
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        private Rectangle hitbox;
        protected Vector2 position;
        protected Vector2 origin;
        protected Vector2 velocity;
        //layer on which the sprite is drawn (higher means further back)
        protected float layer=0.5f;

        //Properties
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        public Vector2 Position 
        {
            get => position;
            set { position = value; Hitbox = new Rectangle((int)value.X - (Hitbox.Width / 2), (int)value.Y - (Hitbox.Height / 2), Hitbox.Width, Hitbox.Height); } 
        }

        //Methods

        public void CheckCollision(GameObject other)
        {
            if (this is not Background && Hitbox.Intersects(other.Hitbox))
            {
                OnCollision(other);
            }
            
        }

        public virtual void OnCollision(GameObject other)
        {
            if (this == other)
            {
                return;
            }
        }
        public virtual void LoadContent(ContentManager content)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Hitbox = new Rectangle((int)position.X - (int)((sprite.Width / 2)), (int)position.Y - (int)((sprite.Height / 2)), (int)(sprite.Width), (int)(sprite.Height));
        }
        public virtual void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (this is not Background) 
            {
                CheckBounds(screenSize);
            }           

        }
        /// <summary>
        /// Keeps the gameObject on the designated "street"
        /// </summary>
        public void CheckBounds(Vector2 screenSize)
        {
            if (position.Y - (sprite.Height / 2) < GameWorld.TopBoundary)
            {
                position = new Vector2(position.X, GameWorld.TopBoundary + (sprite.Height / 2));
                velocity.Y = 0;
                Debug.WriteLine("Hit top boundary ("+GameWorld.TopBoundary.ToString()+")");
            }

            if (position.Y + (sprite.Height / 2) > GameWorld.BottomBoundary)
            {
                position = new Vector2(position.X, GameWorld.BottomBoundary - (sprite.Height / 2));
                velocity.Y = 0;
                Debug.WriteLine("Hit bottom boundary (" + GameWorld.TopBoundary.ToString() + ")");
            }

            if(position.X + (sprite.Width / 2) > screenSize.X)
            {
                position.X=screenSize.X- (sprite.Width / 2);
                velocity.X = 0;
            }

            if (position.X - (sprite.Width / 2) < 0)
            {
                position.X =  sprite.Width / 2;
                velocity.X = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin = new Vector2(sprite.Width / 2, sprite.Height / 2), 1, SpriteEffects.None, layer);
        }


        /// <summary>
        /// Removes this object from the GameWorld.
        /// </summary>
        public void RemoveThis()
        {
            GameWorld.ActiveGameWorld.RemoveObject(this);
        }
    }
}
