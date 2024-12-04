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


    public class MeleeAttack : GameObject
    {
        private Character following;
        private Vector2 characterPositionOffset;
        private int damage;
        private bool isCrit;
        private bool isFacingRight;
        private bool isHeavyAttack;
        private float existanceTime;
        private float cooldown;
        private bool isPlayerAttack;

        public float ExistanceTime { get => existanceTime; private set => existanceTime = value; }
        public float Cooldown { get => cooldown; set => cooldown = value; }

        public MeleeAttack(Character followedCharacter, int damage, bool isCrit, bool isFacingRight, bool isHeavyAttack, Texture2D sprite, float cooldown, bool isPlayerAttack) : base()
        {
            following = followedCharacter;
            int reach = followedCharacter.Hitbox.Width / 2 + 25;
            characterPositionOffset = isFacingRight ? new Vector2(reach, 0) : new Vector2(-reach, 0);
            Position = followedCharacter.Position + characterPositionOffset;
            this.damage = damage;
            this.isCrit = isCrit;
            this.isFacingRight = isFacingRight;
            this.isHeavyAttack = isHeavyAttack;
            ExistanceTime = 0.1F;
            Cooldown = cooldown;
            Hitbox = new Rectangle(0, 0, 100, 100);
            this.isPlayerAttack = isPlayerAttack;

            this.sprite = sprite;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Position = following.Position + characterPositionOffset;
            base.Update(gameTime, screenSize);

            ExistanceTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ExistanceTime <= 0)
            {
                RemoveThis();
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is IDamagable)
            {
                if ((isPlayerAttack && other is Enemy) || (!isPlayerAttack && other is Player) || ((isPlayerAttack && other is Breakable)))
                {
                    ((IDamagable)other).TakeDamage(damage, true, other);
                    base.OnCollision(other);
                }
            }
        }
    }
}
