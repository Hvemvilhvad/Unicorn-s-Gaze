using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Unicorns_Gaze.states
{
    public abstract class State
    {
        protected GameWorld gameworld;
        protected ContentManager contentmanager;

        public State(GameWorld gameworld, ContentManager contentmanager)
        {
            this.gameworld = gameworld;
            this.contentmanager = contentmanager;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
    }
}
