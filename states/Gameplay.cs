using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Unicorns_Gaze.states
{
    public class Gameplay: State
    {
        public Gameplay(GameWorld gameworld, ContentManager contentmanager) : base(gameworld, contentmanager)
        {
            this.contentmanager = contentmanager;
            this.gameworld=gameworld;
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
