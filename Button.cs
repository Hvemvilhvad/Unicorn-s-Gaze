using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorns_Gaze.states;

namespace Unicorns_Gaze
{
    public class Button:GameObject
    {
        //Fields
        private MouseState mouseState;

        private bool isHovering;

        private GameWorld game;
        private ContentManager contentManager;
        
        private State.ButtonPurpose buttonPurpose;

        //Properties
        public bool Clicked {  get; private set; }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            }
        }

        //Constructors
        public Button(Texture2D texture,GameWorld game,State.ButtonPurpose purpose)
        {
            this.Sprite = texture;
            buttonPurpose = purpose;
            this.game = game;
            contentManager = game.Content;
            layer = 0;
            doDynamicLayer = false;
            scale = 0.1f;
        }

        //Methods
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            mouseState=Mouse.GetState();

            Rectangle mouseHitbox=new Rectangle(mouseState.X,mouseState.Y,2,2);
            isHovering=false;

            if (mouseHitbox.Intersects(Hitbox)) 
            { 
                isHovering = true;
                if (mouseState.LeftButton == ButtonState.Pressed) 
                { 
                    Clicked = true;
                }
            }

            if (isHovering)
            {
                color = Color.Gray;
                
            }
            else
            {
                color = Color.White;
            }

            if (Clicked) 
            {
                if(buttonPurpose==State.ButtonPurpose.StartGame)
                {
                    Gameplay newGame= new Gameplay(game, contentManager);
                    game.NextState = newGame;
                    GameWorld.IsAlive = true;
                }
            }
            base.Update(gameTime, screenSize);
        }

    }
}
