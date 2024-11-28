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

        public override void Use()
        {
            GameWorld.Player.Heal(HealAmount);
            base.Use();
        }
    }
}
