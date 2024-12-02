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
        private GameWorld world;

        public Menu(GameWorld gameworld, ContentManager contentmanager) : base(gameworld, contentmanager)
        {
            world = gameworld;
        }

        public override void LoadContent()
        {
            Texture2D startButtonTexture = contentmanager.Load<Texture2D>("tempButton");
            Button menuButton=new Button(startButtonTexture,world,ButtonPurpose.StartGame);
            menuButton.Position = new Vector2(GameWorld.ScreenSize.X / 2, 800);
            GameWorld.GameObjectsToAdd.Add(menuButton);
            menuButton.LoadContent(contentmanager);
            menuBackground = contentmanager.Load<Texture2D>("tempMenuBackground");
            Background background=new Background(menuBackground);
            background.Position=new Vector2(GameWorld.ScreenSize.X/2,GameWorld.ScreenSize.Y/2);
            GameWorld.GameObjectsToAdd.Add(background);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
