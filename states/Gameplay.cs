using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;

namespace Unicorns_Gaze.states
{
    public class Gameplay : State
    {
        //Fields
        private Player player;
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
        private int waveNr;
        private SpriteFont uiFont;
        private static SoundEffect hurtSound;

        //Properties
        public bool ScreenMoving { get => screenMoving; set => screenMoving = value; }
        public static int TopBoundary { get => topBoundary; }
        public static int BottomBoundary { get => bottomBoundary; }

        public static SoundEffect Hurt_Sound { get => hurtSound; set => hurtSound = value; }

        //Constructors
        public Gameplay(GameWorld gameworld, ContentManager contentmanager) : base(gameworld, contentmanager)
        {
            this.contentmanager = contentmanager;
            this.gameworld=gameworld;
            progress = 0;
        }


        //Methods
        public override void LoadContent()
        {
            screenSpeed = 3;
            Vector2 playerPosition = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);
            player = new Player(10, playerPosition, 500);
            GameWorld.Player = player;
            uiFont = contentmanager.Load<SpriteFont>("textfont_ui");

            //defines the bounds of where the player/enemies/other gameobjects can be
            topBoundary = (int)GameWorld.ScreenSize.Y / 3;
            bottomBoundary = (int)GameWorld.ScreenSize.Y - ((int)GameWorld.ScreenSize.Y / 5);
            screenMoving = true;

            Background background = new Background();
            background.LoadContent(contentmanager);

            GameWorld.GameObjectsToAdd.Add(player);
            GameWorld.GameObjectsToAdd.Add(background);
            hurtSound = contentmanager.Load<SoundEffect>("swordswing");

            foreach (GameObject item in GameWorld.GameObjectsToAdd)
            {
                item.LoadContent(contentmanager);
            }
            //to activate waves
            SpawnWave();
        }

        public override void Update(GameTime gameTime)
        {
            //move screen
            if (screenMoving && player.Position.X > (GameWorld.ScreenSize.X / 2))
            {
                foreach (GameObject gameObject in GameWorld.GameObjects)
                {
                    float xPos = gameObject.Position.X - screenSpeed;
                    gameObject.Position = new Vector2(xPos, gameObject.Position.Y);
                    progress += screenSpeed;
                }
            }

            //Waves 
            if (progress >= nextWave)
            {
                SpawnWave();
            }


            //if enemies are gone
            if (!screenMoving & !(GameWorld.GameObjects.Any((gameObject)=> gameObject.GetType().IsSubclassOf(typeof(Enemy))) | GameWorld.GameObjectsToAdd.Any((gameObject) => gameObject.GetType().IsSubclassOf(typeof(Enemy)))))
            {
                screenMoving = true;
            }

            //switch to gameover if the player is dead
            if (!GameWorld.IsAlive)
            {
                GameOver gameOver = new GameOver(gameworld, contentmanager);
                gameworld.NextState = gameOver;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(uiFont, "Health: " + player.Health, new Vector2(10, 5), Color.Gold);
        }

        /// <summary>
        /// Spawns enemies procedurally
        /// </summary>
        private void SpawnWave()
        {
            if (progress == 0)
            {
                //where the waves happen
                waves = new int[] { 2000, 4000, 4400, 10000, 12000,15000,18000 ,18800,19000,26000,30000,30500,30600,30800,38000};
                nextWave = waves[0];
            }
            else
            {
                //if we've reached the point where a wave should spawn
                if (waveNr != -1 && waveNr <= waves.Length - 1)
                {
                    
                    switch (waveNr)
                    {
                        //remember to adjust 'waves'
                        //also set screenMoving to false if the screen should stop during a wave
                        case 0:
                            //enemies & items spawn here
                            screenMoving = false;
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2)));

                            break;
                        case 1:
                            //enemies & items spawn here
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, 700)));
                            
                            break;
                        case 2:
                            //enemies & items spawn here
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, 900)));
                            break;
                        case 3:
                            //enemies & items spawn here
                            screenMoving = false;
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2+100)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2-100)));
                            GameWorld.MakeObject(new Brute(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2)));
                            break;
                        case 4:
                            screenMoving = false;
                            GameWorld.MakeObject(new Mage(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 100)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2)));
                            break;
                        case 5:
                            screenMoving = false;
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 200)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 100)));
                            break;
                        case 6:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2+150)));
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2-50)));
                            break;
                        case 7:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 )));
                            break;
                        case 8:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2+70)));
                            break;
                        case 9:
                            screenMoving = false;
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 200)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 100)));
                            GameWorld.MakeObject(new Mage(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 100)));
                            break;
                        case 10:
                            screenMoving = false;
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 200)));
                            GameWorld.MakeObject(new Brute(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2+60)));
                            GameWorld.MakeObject(new Brute(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2-60)));
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 200)));
                            break;
                        case 11:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 70)));
                            break;
                        case 12:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 150)));
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 50)));
                            break;
                        case 13:
                            GameWorld.MakeObject(new Breakable(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2)));
                            break;
                        case 14:
                            //final wave
                            screenMoving = false;
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 200)));
                            GameWorld.MakeObject(new Ranged(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 250)));
                            GameWorld.MakeObject(new Mage(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 + 100)));
                            GameWorld.MakeObject(new Brute(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 60)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 200)));
                            GameWorld.MakeObject(new Grunt(new Vector2(GameWorld.ScreenSize.X, GameWorld.ScreenSize.Y / 2 - 100)));
                            break;
                        
                        default:
                            break;
                    }
                    if (waveNr != -1 && waveNr + 1 != waves.Length)
                    {
                        nextWave = waves[waveNr + 1];
                    }
                    waveNr++;
                }
            }

        }
    }
}
