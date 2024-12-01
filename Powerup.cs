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
    public abstract class Powerup : Item
    {
        public Powerup(Vector2 position) : base(position)
        {

        }


        public override void Use()
        {
            base.Use();
        }

        /// <summary>
        /// Makes a random Powerup.
        /// </summary>
        /// <returns>A random Powerup.</returns>
        public static Powerup GetRandomPowerUp(Vector2 position)
        {
            int whichPowerUp = GameWorld.Random.Next(0, 3);

            switch (whichPowerUp)
            {
                case 0:
                    return new HealthPowerup(position);
                case 1:
                    return new DamagePowerup(position);
                case 2:
                    return new CriticalPowerup(position);
                default:
                    return new HealthPowerup(position);
            }
        }
    }
}
