using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Unicorns_Gaze
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static List<GameObject> gameObjectsToRemove;
        private static List<GameObject> gameObjectsToAdd;
        private static List<GameObject> gameObjects;
        private static Vector2 screenSize;
        private static GameWorld activeGameWorld;
        private static Random random;
        private static Player player;
        private static int topBoundary;
        private static int bottomBoundary;
        private Texture2D background;

#if DEBUG
        private Texture2D hitboxPixel;
#endif


        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> GameObjectsToAdd { get => gameObjectsToAdd; set => gameObjectsToAdd = value; }
        public static List<GameObject> GameObjectsToRemove { get => gameObjectsToRemove; set => gameObjectsToRemove = value; }
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static GameWorld ActiveGameWorld { get => activeGameWorld; private set => activeGameWorld = value; }
        public static Player Player { get => player; private set => player = value; }
        public static Random Random { get => random; private set => random = value; }

        public static int TopBoundary { get => topBoundary; }
        public static int BottomBoundary { get => topBoundary; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            Player player = new Player(10, playerPosition, 500);

            GameObjects = new List<GameObject>() { player };
            GameObjectsToRemove = new List<GameObject>();
            GameObjectsToAdd = new List<GameObject>();
            

            base.Initialize();

            activeGameWorld = this;
            Random = new Random();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            hitboxPixel = Content.Load<Texture2D>("Hitbox pixel");
            
            background= Content.Load<Texture2D>("tempBackgroundLol");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime, screenSize);

                foreach (GameObject other in GameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }

            // remove game objects
            foreach (GameObject removedObject in GameObjectsToRemove)
            {
                GameObjects.Remove(removedObject);
            }
            GameObjectsToRemove.Clear();

            // add game objects
            GameObjects.AddRange(GameObjectsToAdd);
            GameObjectsToAdd.Clear();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0, new Vector2(background.Width / 2, background.Height / 2), 1, SpriteEffects.None, 1);
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
#if DEBUG
            foreach (GameObject gameObject in GameObjects)
            {
                Rectangle hitBox = gameObject.Hitbox;
                Rectangle topline = new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, 1);
                Rectangle bottomline = new Rectangle(hitBox.X, hitBox.Y + hitBox.Height, hitBox.Width, 1);
                Rectangle rightline = new Rectangle(hitBox.X + hitBox.Width, hitBox.Y, 1, hitBox.Height);
                Rectangle leftline = new Rectangle(hitBox.X, hitBox.Y, 1, hitBox.Height);

                _spriteBatch.Draw(hitboxPixel, topline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, bottomline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, rightline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, leftline, null, Color.White);

                Vector2 position = gameObject.Position;
                Rectangle centerDot = new Rectangle((int)position.X - 1, (int)position.Y - 1, 3, 3);

                _spriteBatch.Draw(hitboxPixel, centerDot, null, Color.White);
            }
#endif

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void RemoveObject(GameObject gameObject)
        {
            GameObjectsToRemove.Add(gameObject);
        }

        public void AddObject(GameObject gameObject)
        {
            GameObjectsToAdd.Add(gameObject);
        }
    }
}
