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
            this.sprite = sprite;
            scale = GameWorld.ScreenSize.Y / this.sprite.Height;
            layer = 0.9f;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (position.X <= -(sprite.Width/2))
            {
                Position = new Vector2(screenSize.X+(sprite.Width/2), screenSize.Y / 2);
            }
            base.Update(gameTime, screenSize);
        }
    }
}
