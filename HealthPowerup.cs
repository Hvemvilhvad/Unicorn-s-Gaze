using Microsoft.Xna.Framework;
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


        public override void Use()
        {
            base.Use();
            GameWorld.Player.MaxHealth += 10;
        }
    }
}
