using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public abstract class Character : GameObject
    {
        //Fields
        protected float speed;
        private int health;


        //Properties
        public int Health { get => health; set => health = value; }

        //Methods
        public override void Update()
        {

        }

    }
}
