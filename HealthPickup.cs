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
    public class HealthPickup : Item
    {
        private int HealAmount;

        public HealthPickup(Vector2 position) : base(position)
        {
            HealAmount = 7;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);           
            sprites[SpriteType.Standard] = content.Load<Texture2D>("powerup green");
            sprite = content.Load<Texture2D>("powerup green");
        }

        public override void Use()
        {
            if (GameWorld.Player.Health != GameWorld.Player.MaxHealth)
            {
                SplashText pickupText = new SplashText(HealAmount + " HEALED", Color.Green, GameWorld.Player);
                GameWorld.MakeObject(pickupText);
                GameWorld.Player.Heal(HealAmount);
                base.Use();
            }
        }
    }
}
