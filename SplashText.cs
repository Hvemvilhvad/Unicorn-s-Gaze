using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    internal class SplashText : GameObject
    {
        private SpriteFont splashFont;
        private string text;
        private Color textColor;
        private GameObject target;
        private float textTimer = 2;
        public string Text { get => text; set => text = value; }
        public Color TextColor { get => textColor; set => textColor = value; }
        public GameObject Target { get => target; set => target = value; }

        public SplashText(string text, Color color, GameObject target)
        {
            Text = text;
            TextColor = color;
            Target = target;
            Position = target.Position + new Vector2(0, -100);
        }

        public override void LoadContent(ContentManager content)
        {
            splashFont = content.Load<SpriteFont>("textfont_ui");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            textTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (textTimer <= 0)
            {
                RemoveThis();
            }
            base.Update(gameTime, screenSize);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(splashFont, Text, Position, TextColor);
        }
    }
}
