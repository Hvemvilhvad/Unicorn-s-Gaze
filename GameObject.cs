using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
            if (Hitbox.IntersectsWith(other.Hitbox))
            {
                OnCollision(other);
            }
        }

        public void OnCollision(GameObject other)
        {

        }
    }
}
