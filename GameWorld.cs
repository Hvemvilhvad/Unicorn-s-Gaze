using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection.Metadata;
using Unicorns_Gaze.states;

namespace Unicorns_Gaze
{
    public class GameWorld : Game
    {
        //Fields
        private static Player player;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static List<GameObject> gameObjectsToRemove;
        private static List<GameObject> gameObjectsToAdd;
        private static List<GameObject> gameObjects;
        private static Vector2 screenSize;
        private static GameWorld activeGameWorld;
        private static Random random;
        private static Texture2D noSprite;
        private static Vector2 playerLocation;
        private static bool isAlive = true;
        //states
        private State currentState;
        private State nextState;


#if DEBUG
        private Texture2D hitboxPixel;
#endif

        //Properties
        public static Random Random { get => random; private set => random = value; }

        public static Player Player { get => player; set => player = value; }

        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> GameObjectsToAdd { get => gameObjectsToAdd; set => gameObjectsToAdd = value; }
        public static List<GameObject> GameObjectsToRemove { get => gameObjectsToRemove; set => gameObjectsToRemove = value; }
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static GameWorld ActiveGameWorld { get => activeGameWorld; private set => activeGameWorld = value; }

        public State NextState { set => nextState = value; }
        public static Texture2D NoSprite { get => noSprite; private set => noSprite = value; }
        public static Vector2 PlayerLocation { get => playerLocation; set => playerLocation = value; }
        public static bool IsAlive { get => isAlive; set => isAlive = value; }



        /// <summary>
        /// GameWorld constructor
        /// </summary>
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the screen size and instantiates lists
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            GameObjects = new List<GameObject>();
            GameObjectsToRemove = new List<GameObject>();
            GameObjectsToAdd = new List<GameObject>();
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Vector2 someTempPosition = new Vector2(ScreenSize.X / 2 + 300, ScreenSize.Y / 2 + 300);
            Breakable tempBreakable = new Breakable(someTempPosition);

            base.Initialize();

            activeGameWorld = this;
            Random = new Random();
            
        }

        /// <summary>
        /// Loads textures
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new Menu(this,Content);
            currentState.LoadContent();

            NoSprite = Content.Load<Texture2D>("notexture");
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            hitboxPixel = Content.Load<Texture2D>("Hitbox pixel");
        }


        /// <summary>
        /// Runs every frame and handles a lot of the methods
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState.Update(gameTime);

            Vector2 screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime, screenSize);

                foreach (GameObject other in GameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }

            //change states if necessary
            if (nextState != null)
            {
                currentState = nextState;
                if(currentState is not GameOver)
                {
                    foreach (GameObject item in gameObjects)
                    {
                        gameObjectsToRemove.Add(item);
                    }
                }
                
                currentState.LoadContent();
                nextState = null;                
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

            currentState.Update(gameTime);
            base.Update(gameTime);
            
        }

        /// <summary>
        /// Draws out the gameObjects to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront);
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

        /// <summary>
        /// Method used for removing gameObjects
        /// </summary>
        /// <param name="gameObject"></param>
        public void RemoveObject(GameObject gameObject)
        {
            GameObjectsToRemove.Add(gameObject);
        }

        /// <summary>
        /// Methods used for adding gameObjects
        /// </summary>
        /// <param name="gameObject"></param>
        public static void MakeObject(GameObject gameObject)
        {
            gameObject.LoadContent(ActiveGameWorld.Content);
            GameObjectsToAdd.Add(gameObject);
        }

    }
}
