using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public abstract class Powerup : Item
    {


        public override void Use()
        {
            base.Use();
        }

        /// <summary>
        /// Makes a random Powerup.
        /// </summary>
        /// <returns>A random Powerup.</returns>
        public static Powerup GetRandomPowerUp()
        {
            int whichPowerUp = GameWorld.Random.Next(0, 3);

            switch (whichPowerUp)
            {
                case 0:
                    return new HealthPowerup();
                case 1:
                    return new DamagePowerup();
                case 2:
                    return new CriticalPowerup();
                default:
                    return new HealthPowerup();
            }
        }
    }
}
