﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Brute : Enemy
    {
        public Brute(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Brute(Vector2 position) : base(position)
        {
            Position = position;
        }
    }
}
