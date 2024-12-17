using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class HealthPowerup : Powerup
    {
        public HealthPowerup(Vector2 position) : base(position)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            sprites[SpriteType.Standard] = content.Load<Texture2D>("powerup teal");
            sprite = content.Load<Texture2D>("powerup teal");
            base.LoadContent(content);
        }

        public override void Use()
        {
            SplashText pickupText = new SplashText( "MAX HEALTH UP", Color.Green, GameWorld.Player);
            GameWorld.MakeObject(pickupText);
            GameWorld.Player.MaxHealth += 5;
            GameWorld.Player.Heal(5);
            base.Use();
        }
    }
}
