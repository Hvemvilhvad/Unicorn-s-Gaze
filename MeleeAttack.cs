using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class MeleeAttack : GameObject
    {
        private Character following;
        private Vector2 characterPositionOffset;
        private int damage;
        private bool isCrit;
        private bool isFacingRight;
        private bool isHeavyAttack;
        private float existanceTime;

        public MeleeAttack(Character followedCharacter, int damage, bool isCrit, bool isFacingRight, bool isHeavyAttack, Texture2D sprite) : base()
        {
            following = followedCharacter;
            Position = followedCharacter.Position + characterPositionOffset;
            this.damage = damage;
            this.isCrit = isCrit;
            this.isFacingRight = isFacingRight;
            this.isHeavyAttack = isHeavyAttack;
            characterPositionOffset = isFacingRight ? new Vector2(30, 0) : new Vector2(-30, 0);

            this.sprite = sprite;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Position = following.Position + characterPositionOffset;
            base.Update(gameTime, screenSize);

            existanceTime -= gameTime.ElapsedGameTime.Seconds;
            if (existanceTime <= 0)
            {
                RemoveThis();
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy & following is Player)
            {
                ((Character)other).TakeDamage(damage);
            }
            else if (other is Player & following is Enemy)
            {
                ((Character)other).TakeDamage(damage);
                RemoveThis();
            }
            base.OnCollision(other);
        }
    }
}
