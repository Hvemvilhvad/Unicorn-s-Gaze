using Microsoft.Xna.Framework;
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
        public Background(Texture2D sprite) 
        { 
            this.Sprite = sprite;
            scale = GameWorld.ScreenSize.Y / this.Sprite.Height;
            layer = 1f;
            doDynamicLayer = false;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (position.X <= -(Sprite.Width/2))
            {
                Position = new Vector2(screenSize.X+(Sprite.Width/2), screenSize.Y/2);
            }
            base.Update(gameTime, screenSize);
        }
    }
}
