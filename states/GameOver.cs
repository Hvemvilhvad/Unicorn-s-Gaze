using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Unicorns_Gaze.states
{
    public class GameOver:State
    {
        public GameOver(GameWorld gameworld, ContentManager contentmanager) : base(gameworld, contentmanager)
        {

        }

        public override void LoadContent()
        {
            Texture2D startButtonTexture = contentmanager.Load<Texture2D>("tempButton");
            Button menuButton = new Button(startButtonTexture, gameworld, ButtonPurpose.StartGame);
            menuButton.Position = new Vector2(GameWorld.ScreenSize.X / 2, 800);
            GameWorld.GameObjectsToAdd.Add(menuButton);
            menuButton.LoadContent(contentmanager);
            Texture2D backgroundTexture = contentmanager.Load<Texture2D>("game over temp img");
            Background gameOverBackground = new Background(backgroundTexture);
            GameWorld.GameObjectsToAdd.Add(gameOverBackground);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
