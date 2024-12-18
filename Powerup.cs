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
    public abstract class Powerup : Item
    {
        public Powerup(Vector2 position) : base(position)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            
            base.LoadContent(content);
        }


        public override void Use()
        {
            base.Use();
        }

        
       
    }
}
