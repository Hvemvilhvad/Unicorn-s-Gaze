﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
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
            Hitbox = new Rectangle((int)position.X, (int)position.Y, (int)(sprite.Width*scale), (int)(sprite.Height * scale));
            sprites[SpriteType.Standard] = sprite;
            doShadow = true;
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (isFacingRight)
            {
                position = new Vector2(position.X + speed,position.Y);
            }
            else
            {
                position = new Vector2(position.X - speed, position.Y);
            }
            Hitbox = new Rectangle((int)(position.X - (Hitbox.Width / 2)), (int)(((position.Y + Height) - (Hitbox.Height / 2))), (int)(Hitbox.Width), (int)(Hitbox.Height));
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

