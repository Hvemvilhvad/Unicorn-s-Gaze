﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Unicorns_Gaze
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static List<GameObject> gameObjectsToRemove;
        private static List<GameObject> gameObjectsToAdd;
        private static List<GameObject> gameObjects;
        private static GameWorld activeGameWorld;
        private static Random random;
        private static Player player;


        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> GameObjectsToAdd { get => gameObjectsToAdd; set => gameObjectsToAdd = value; }
        public static List<GameObject> GameObjectsToRemove { get => gameObjectsToRemove; set => gameObjectsToRemove = value; }
        public static GameWorld ActiveGameWorld { get => activeGameWorld; private set => activeGameWorld = value; }
        public static Player Player { get => player; private set => player = value; }
        public static Random Random { get => random; private set => random = value; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            activeGameWorld = this;
            Random = new Random();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update();

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

            // TODO: Add your drawing code here

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
