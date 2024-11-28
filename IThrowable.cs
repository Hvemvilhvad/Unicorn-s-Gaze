using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public interface IThrowable
    {


        public void Update(GameTime gameTime, Vector2 screenSize)
        {
            //((IDamagable)this).Update(gameTime, screenSize);
        }
    }


}
