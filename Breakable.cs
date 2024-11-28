using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Breakable : Environment, IDamagable
    {
        private int health;

        public int Health
        {
            get => health;
            set
            {
                if (value < 0)
                {
                    RemoveThis();
                }
                else
                {
                    health = value;
                }
            }
        }


        public Breakable(Vector2 position) : base()
        {
            Position = position;
            Health = 10;
        }

    }
}
