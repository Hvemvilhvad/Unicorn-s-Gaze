using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Item : Environment
    {


        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
            if (other is Player)
            {
                Use();
            }
        }


        /// <summary>
        /// Uses the item and removes it from the GameWorld if it was succesfully used.
        /// </summary>
        public virtual void Use()
        {
            RemoveThis();
        }
    }
}
