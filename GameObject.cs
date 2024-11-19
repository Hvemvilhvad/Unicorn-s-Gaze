using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public abstract class GameObject
    {
        //Fields
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        private Rectangle hitbox;
        private Vector2 position;
        protected Vector2 origin;
        protected Vector2 velocity;

        //Properties
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        public Vector2 Position { get => position; set => position = value; }

        //Methods

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
            if (this == other)
            {
                return;
            }
        }
        public virtual void Update()
        {
            //add update logic here

        }

        public virtual void Draw()
        {

        }
    }
}
