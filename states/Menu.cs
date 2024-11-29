using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace Unicorns_Gaze.states
{
    public class Menu : State
    {
        private Texture2D menuBackground;

        public Menu(GameWorld gameworld, ContentManager contentmanager) : base(gameworld, contentmanager)
        {

        }

        public override void LoadContent()
        {
            Texture2D startButtonTexture = contentmanager.Load<Texture2D>("tempButton");
            menuBackground = contentmanager.Load<Texture2D>("tempMenuBackground");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
