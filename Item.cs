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
    public class Item : Environment
    {
        private float rotation;
        private bool doFlip;
        protected Texture2D critPowerupSprite;
        protected Texture2D dmgPowerupSprite;
        protected Texture2D hpPowerupSprite;
        protected Texture2D hpPickupSprite;

        public float Rotation
        {
            get => rotation;
            set
            {
                if (value < 0)
                {
                    rotation = value + (float)Math.PI;
                    doFlip = !doFlip;
                }
                else if (value > Math.PI)
                {
                    rotation = value - (float)Math.PI;
                    doFlip = !doFlip;
                }
                else
                {
                    rotation = value;
                }
            }
        }

        public Item(Vector2 position) : base()
        {
            Position = position;
            doShadow = true;
            scale=0.2f;
        }

        public override void LoadContent(ContentManager content)
        {
            critPowerupSprite = content.Load<Texture2D>("powerup purple");
            dmgPowerupSprite = content.Load<Texture2D>("powerup orange");
            hpPowerupSprite = content.Load<Texture2D>("powerup teal");
            hpPickupSprite = content.Load<Texture2D>("powerup green");
            base.LoadContent(content);
        }

        /// <summary>
        /// Rotates
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Rotation += 0.05F;
            base.Update(gameTime, screenSize);
        }


        public override bool OnCollision(GameObject other)
        {
            if (base.OnCollision(other))
            {
                if (other is Player)
                {
                    Use();
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Uses the item and removes it from the GameWorld if it was succesfully used.
        /// </summary>
        public virtual void Use()
        {
            RemoveThis();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle((int)position.X, (int)position.Y, (int)((Sprite.Width * Math.Sin(Rotation)*scale)), (int)(Sprite.Height*scale)), null, Color.White, 0, origin = new Vector2((Sprite.Width) / 2, (Sprite.Height) / 2), doFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, layer);
            DrawShadow(spriteBatch);
        }

        public static Item GetRandomItem(Vector2 pos)
        {
            switch (GameWorld.Random.Next(0, 4))
            {
                case 0:
                    return new HealthPickup(pos);
                case 1:
                    return new HealthPowerup(pos);
                case 2:
                    return new CriticalPowerup(pos);
                case 3:
                    return new DamagePowerup(pos);
                default:
                    return new HealthPickup(pos);
            }
        }
    }
}
