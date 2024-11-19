using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Unicorns_Gaze
{
    public abstract class GameObject
    {
        Rectangle hitbox;

        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }

        public void Update()
        {
            //add update logic here

        }

        public void CheckCollision(GameObject other)
        {
            //add collision logic here
            if (Hitbox.Intersects(other.Hitbox))
            {
                OnCollision(other);
            }
        }

        public virtual void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// Removes this object from the GameWorld.
        /// </summary>
        public void RemoveThis()
        {
            GameWorld.ActiveGameWorld.RemoveObject(this);
        }
    }
}
