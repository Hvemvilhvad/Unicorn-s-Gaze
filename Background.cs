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
    public class Background : Environment
    {
        private bool isGameplay;
        private Vector2 foregroundPosition;
        public override Vector2 Position
        {
            get => position;
            set
            {
                ForegroundPosition += new Vector2((value.X - position.X) * 1.2F, 0);
                position = value;
                Hitbox = new Rectangle((int)(value.X - (Hitbox.Width / 2)), (int)(((value.Y + Height) - (Hitbox.Height / 2))), (int)(Hitbox.Width), (int)(Hitbox.Height));

                if (isGameplay)
                {
                    if (position.X + ((Sprite.Width * scale) / 2) <= 0)
                    {
                        position += new Vector2((Sprite.Width * scale), 0);
                    }
                }
            }
        }
        public Vector2 ForegroundPosition 
        { 
            get => foregroundPosition;
            set
            {
                foregroundPosition = value;
                if (isGameplay)
                {
                    if (foregroundPosition.X + ((Sprite.Width * scale) / 2) <= 0)
                    {
                        foregroundPosition += new Vector2((Sprite.Width * scale), 0);
                    }
                }
            }
        }


        /// <summary>
        /// This constructor is used for gamestates with alternative backgrounds.
        /// </summary>
        /// <param name="sprite"></param>
        public Background(Texture2D sprite)
        {
            isGameplay = false;
            sprites[SpriteType.Standard] = sprite;
            scale = GameWorld.ScreenSize.Y / sprite.Height;
            layer = 1f;
            doDynamicLayer = false;
            ForegroundPosition = Position;

        }

        /// <summary>
        /// This constructor is used for the gameplay background
        /// </summary>
        public Background()
        {
            isGameplay = true;
            layer = 1f;
            doDynamicLayer = false;
        }



        public override void LoadContent(ContentManager content)
        {
            Texture2D backgroundTexture = content.Load<Texture2D>("background");
            Texture2D foregroundTexture = content.Load<Texture2D>("foreground");
            sprites[SpriteType.Standard] = backgroundTexture;
            sprites[SpriteType.Hurt] = foregroundTexture;

            scale = GameWorld.ScreenSize.Y / Sprite.Height;
            Position = new Vector2(0, GameWorld.ScreenSize.Y / 2);
            ForegroundPosition = Position;

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, layer);

            if (isGameplay)
            {
                spriteBatch.Draw(Sprite, Position + new Vector2(Sprite.Width * scale, 0), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, layer);
                spriteBatch.Draw(Sprite, Position + new Vector2(Sprite.Width * scale * 2, 0), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, layer);

                spriteType = SpriteType.Hurt;
                float yPos = GameWorld.ScreenSize.Y / 2 - 150;
                spriteBatch.Draw(Sprite, foregroundPosition + new Vector2(0, yPos), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, 0);
                spriteBatch.Draw(Sprite, foregroundPosition + new Vector2(Sprite.Width * scale, yPos), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, 0);
                spriteBatch.Draw(Sprite, foregroundPosition + new Vector2(Sprite.Width * scale * 2, yPos), null, color, 0, origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2), scale, SpriteEffects.None, 0);
                spriteType = SpriteType.Standard;
            }
        }

    }
}
