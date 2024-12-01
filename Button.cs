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

        //Properties
        public bool Clicked {  get; private set; }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(sprite.Width / 2, sprite.Height / 2);
            }
        }

        //Constructors
        public Button(Texture2D texture,GameWorld game)
        {
            this.sprite = texture;
            this.game = game;
            contentManager = game.Content;
            layer = 0;
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
                game.NextState = new Gameplay(game,contentManager);
            }
            base.Update(gameTime, screenSize);
        }

    }
}
