using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Unicorns_Gaze
{
    public class GameWorld : Game
    {
        //Fields
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
        private static bool screenMoving;
        private static int screenSpeed;
        private Texture2D backgroundSprite;
        private static int progress;
        //x-positions at which the screen stops moving until enemies are defeated
        //Where enemies spawn
        private static int[] waves;
        private static int nextWave;
        private static int currentWave;

        private static Texture2D noSprite;


#if DEBUG
        private Texture2D hitboxPixel;
#endif


        public bool ScreenMoving { get => screenMoving; set => screenMoving = value; }

        //Properties
        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> GameObjectsToAdd { get => gameObjectsToAdd; set => gameObjectsToAdd = value; }
        public static List<GameObject> GameObjectsToRemove { get => gameObjectsToRemove; set => gameObjectsToRemove = value; }
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static GameWorld ActiveGameWorld { get => activeGameWorld; private set => activeGameWorld = value; }
        public static Player Player { get => player; private set => player = value; }
        public static Random Random { get => random; private set => random = value; }
        public static int TopBoundary { get => topBoundary; }
        public static int BottomBoundary { get => bottomBoundary; }
        public static Texture2D NoSprite { get => noSprite; private set => noSprite = value; }


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
            screenSpeed = 3;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            player = new Player(10, playerPosition, 500);
            
            Vector2 someTempPosition = new Vector2(ScreenSize.X / 2 + 300, ScreenSize.Y / 2 + 300);
            Breakable tempBreakable = new Breakable(someTempPosition);

            GameObjects = new List<GameObject>() { player, tempBreakable };
            GameObjectsToRemove = new List<GameObject>();
            GameObjectsToAdd = new List<GameObject>();

            //defines the bounds of where the player/enemies/other gameobjects can be
            topBoundary = _graphics.PreferredBackBufferHeight / 3;
            bottomBoundary = _graphics.PreferredBackBufferHeight- (_graphics.PreferredBackBufferHeight / 5);

            screenMoving = true;

            base.Initialize();

            activeGameWorld = this;
            Random = new Random();
            nextWave = waves[0];
        }

        /// <summary>
        /// Loads textures
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            NoSprite = Content.Load<Texture2D>("notexture");
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            hitboxPixel = Content.Load<Texture2D>("Hitbox pixel");
            backgroundSprite = Content.Load<Texture2D>("tempBackgroundLol");
            Background background = new Background(backgroundSprite);
            background.Position = new Vector2(0, screenSize.Y/2);
            Background background2 = new Background(backgroundSprite);
            background2.Position = new Vector2(screenSize.X, screenSize.Y / 2);

            gameObjectsToAdd.Add(background);
            gameObjectsToAdd.Add(background2);
            //to activate waves
            SpawnWave();
        }

        
        /// <summary>
        /// Runs every frame and handles a lot of the methods
        /// </summary>
        /// <param name="gameTime"></param>
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

            //move screen
            if (screenMoving && Player.Position.X>(screenSize.X/2))
            {
                foreach (GameObject gameObject in GameObjects)
                {
                    float xPos=gameObject.Position.X-screenSpeed;
                    gameObject.Position=new Vector2(xPos, gameObject.Position.Y);
                    progress += screenSpeed;
                }
            }

            //Waves 
            if (progress >= nextWave) 
            {
                SpawnWave();
            }
            //if enemies are gone
            if(!screenMoving && !gameObjects.OfType<Enemy>().Any())
            {
                screenMoving = true;
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
        public void AddObject(GameObject gameObject)
        {
            GameObjectsToAdd.Add(gameObject);
        }

        private void SpawnWave()
        {
            if (progress==0) 
            {
                //where the waves happen
                waves = new int[] { 50, 200 };
                nextWave = waves[0];
            }
            else
            {
                //if we've reached the point where a wave should spawn
                int temp = Array.FindIndex(waves, (item) => item == nextWave);
                switch (temp)
                {
                    //remember to adjust 'waves'
                    //also set screenMoving to false if the screen should stop during a wave
                    case 0:
                        //enemies & items spawn here
                        break;
                    case 1:
                        //enemies & items spawn here
                        break;
                    case 2:
                        //enemies & items spawn here
                        break;
                    case 3:
                        //enemies & items spawn here
                        break;
                    default:
                        break;
                }

                if (temp != -1 && temp + 1 != waves.Length)
                {
                    nextWave = waves[temp + 1];
                }
            }
            
        }
    }
}
