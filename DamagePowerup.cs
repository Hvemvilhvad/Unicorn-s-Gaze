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
    public class DamagePowerup : Powerup
    {

        public DamagePowerup(Vector2 position) : base(position)
        {

        }

        public override void Use()
        {
            GameWorld.Player.DamageRange = GameWorld.Player.DamageRange.OffsetDamageRange(10);
            base.Use();
        }
    }
}
