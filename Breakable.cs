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
                    SpawnItem();
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


        /// <summary>
        /// When a breakable is broken there is a chance it will spawn an item.
        /// </summary>
        public void SpawnItem()
        {
            //60% chance
            if (GameWorld.Random.Next(0, 3+1) >= 2)
            {
                GameWorld.GameObjectsToAdd.Add(Item.GetRandomItem(Position));
            }
        }
    }
}
