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
        public bool IsGoingRight { get; set; }
        public bool PickedUp { get; set; }
        public Character Following { get; set; }


        public void UpdateThrow(GameTime gameTime, Vector2 screenSize)
        {
            if (HasBeenThrown)
            {
                ThrowTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                ThrowPositionFunction();

                if (Position.Y >= StartPosition.Y)
                {
                    TakeDamage(Health, false);
                }
            }
            else if (PickedUp)
            {
                int holdReach = Following.Hitbox.Width / 2 + ((GameObject)this).Hitbox.Width / 2;
                Position = Following.Position + (Following.IsFacingRight ? new Vector2(holdReach, -50) : new Vector2(-holdReach, -50));
            }
        }

        public void OnThrownCollision(GameObject other)
        {
            if (HasBeenThrown)
            {
                TakeDamage(Health, false);
                if (other is IDamagable)
                {
                    (other as IDamagable).TakeDamage(10, true);
                }
            }
        }

        private void ThrowPositionFunction()
        {
            float xPosition = ThrowTime * (IsGoingRight ? 1 : -1);
            Position = new Vector2(xPosition, -((float)(-0.00045 * Math.Pow(ThrowTime, 2)) + 0.25F * ThrowTime + 50)) + StartPosition;
        }

        public void Throw()
        {
            StartPosition = Position;
            Position += new Vector2(0, -50);
            HasBeenThrown = true;
            ThrowTime = 0;
            IsGoingRight = Following.IsFacingRight;
        }

        public void PickUp(Character pickUpper)
        {
            Following = pickUpper;
            PickedUp = true;
        }


    }


}
