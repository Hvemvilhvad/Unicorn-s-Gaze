﻿using Microsoft.Xna.Framework;
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
            sprites[SpriteType.Standard] = sprite;
            scale = GameWorld.ScreenSize.Y / sprite.Height;
            layer = 1f;
            doDynamicLayer = false;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (position.X <= -(Sprite.Width*scale/2))
            {
                Position = new Vector2((int)(Sprite.Width * scale*2.5f), screenSize.Y/2);
            }
            base.Update(gameTime, screenSize);
        }
    }
}
