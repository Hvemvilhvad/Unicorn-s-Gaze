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
    public class Projectile : GameObject
    {
        private int damage;
        private float speed=5;
        private bool isCrit;
        private bool isFacingRight;
        private bool isHeavyAttack;
        private float existanceTime;
        private float cooldown;

        public float ExistanceTime { get => existanceTime; private set => existanceTime = value; }
        public float Cooldown { get => cooldown; set => cooldown = value; }

        public Projectile(Vector2 archerPosition,int damage, bool isCrit, bool isFacingRight, bool isHeavyAttack, Texture2D sprite, float cooldown) : base()
        {
            Position = archerPosition;
            this.damage = damage;
            this.isCrit = isCrit;
            this.isFacingRight = isFacingRight;
            this.isHeavyAttack = isHeavyAttack;
            Cooldown = cooldown;
            Hitbox = new Rectangle(0, 0, 50, 100);
            sprites[SpriteType.Standard] = sprite;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (isFacingRight)
            {
                position = new Vector2(position.X+speed,position.Y);
            }
            else
            {
                position = new Vector2(position.X - speed, position.Y);
            }
            base.Update(gameTime, screenSize);

        }

        public override bool OnCollision(GameObject other)
        {
            if (base.OnCollision(other) & other is Player)
            {
                ((IDamagable)other).TakeDamage(damage, true);
                RemoveThis();
                return true;
            }
            return false;
        }

        public override void CheckBounds(Vector2 screenSize)
        {
            if (position.X - (Sprite.Width / 2) < 0 || position.X + (Sprite.Width / 2) > screenSize.X || position.Y - (Sprite.Height / 2) < 0|| position.Y + (Sprite.Height / 2) > screenSize.Y)
            {
                RemoveThis();
            }
        }
    }
}

