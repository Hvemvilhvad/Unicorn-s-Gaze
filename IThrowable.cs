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
    public interface IThrowable : IDamagable
    {
        public Vector2 Position { get; set; }
        public Vector2 StartPosition { get; set; }
        public bool HasBeenThrown { get; set; }
        public float ThrowTime { get; set; }


        public void UpdateThrow(GameTime gameTime, Vector2 screenSize)
        {
            if (HasBeenThrown)
            {
                ThrowTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                ThrowPositionFunction();

                if (Position.Y >= StartPosition.Y)
                {
                    TakeDamage(Health);
                }
            }
        }

        public void Throw()
        {
            StartPosition = Position;
            Position += new Vector2(0,-50);
            HasBeenThrown = true;
            ThrowTime = 0;
        }

        private void ThrowPositionFunction()
        {
            Position = new Vector2(ThrowTime, -((float)(-0.00045 * Math.Pow(ThrowTime, 2)) + 0.25F * ThrowTime + 50)) + StartPosition;
        }
    }


}
