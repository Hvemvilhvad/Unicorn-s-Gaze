﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Enemy : Character
    {
        //Fields
        protected Texture2D rangedAttackSprite;
        protected float moveCooldown;

        //Constructor
        public Enemy(int health, Vector2 position, float speed)
        {
            MaxHealth = 10;
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Enemy(Vector2 position)
        {
            Position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            rangedAttackSprite = content.Load<Texture2D>("notexture");
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            depth = Position.Y / 864;
            Move(gameTime, screenSize);
            Stun();

            if (GameWorld.Player.Position.X > Position.X)
            {
                IsFacingRight = true;
            }
            else if (GameWorld.Player.Position.X < Position.X)
            {
                IsFacingRight = false;
            }

            base.Update(gameTime, screenSize);
        }

        //Methods
        /// <summary>
        /// Instantiates the enemies attack
        /// </summary>
        public virtual void Attack()
        {
            MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(), false, IsFacingRight, false, attackSprite, 1, false);
            attackCooldown = attack.ExistanceTime + attack.Cooldown;
            GameWorld.GameObjectsToAdd.Add(attack);
        }

        public virtual void RangedAttack()
        {
            Projectile projectile = new Projectile(position, DamageRange.GetADamageValue(), false, IsFacingRight, false, rangedAttackSprite, 1.5f);
            attackCooldown = projectile.Cooldown;
            GameWorld.GameObjectsToAdd.Add(projectile);
        }
        
        /// <summary>
        /// Allows enemies to move in the direction of the player
        /// </summary>
        public virtual void Chase()
        {
            Vector2 direction = new Vector2(GameWorld.Player.Position.X - position.X, GameWorld.Player.Position.Y - position.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
            velocity.Normalize();
            
        }

        /// <summary>
        /// Checks if the enemy is taking damage, and delays their own attack
        /// </summary>
        public void Stun()
        {
            if (takingDamage)
            {
                attackCooldown = 1;
                moveCooldown = 1;
                takingDamage = false;
            }
        }

        
    }
}
