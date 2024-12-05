﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Mage : Enemy
    {
        //Fields
        private int buffRange = 300;
        private bool withinRange = false;
        private Enemy nearbyEnemy;
        private Texture2D buffSprite;

        //Constructors
        public Mage(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Mage(Vector2 position) : base(position)
        {
            Position = position;
            speed = 150;
        }

        public override void LoadContent(ContentManager content)
        {
            DamageRange = new DamageRange(2, 5);
            sprites = new Texture2D[1];
            buffSprite = content.Load<Texture2D>("notexture");
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[0] = content.Load<Texture2D>("notexture");
            }
            sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            velocity = Vector2.Zero;
            moveCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;            
            if (moveCooldown <= 0)
            {
                moveCooldown = 0;
            }

            if (nearbyEnemy==null||nearbyEnemy.Position.X-position.X>(buffRange-100) || nearbyEnemy.Position.X - position.X < -(buffRange - 100) || nearbyEnemy.Position.Y-position.Y> (buffRange - 100) || nearbyEnemy.Position.Y - position.Y < -(buffRange - 100))
            {
                Chase();
            }

            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                velocity = Vector2.Zero;
                moveCooldown = 2;
            }
            base.OnCollision(other);
        }
        /// <summary>
        /// Override of the chase method, which makes this enemy chase other enemies
        /// </summary>
        public override void Chase()
        {
            List<Enemy> enemyList = new List<Enemy>();
            foreach(GameObject item in GameWorld.GameObjects)
            {
                if(item is Enemy&& item !=this)
                {
                    enemyList.Add((Enemy)item);
                }
            }
            Enemy closestEnemy = enemyList[0];
            Vector2 direction = new Vector2(closestEnemy.Position.X - position.X, closestEnemy.Position.Y - position.Y);
            foreach (Enemy enemy in enemyList)
            {
                if((enemy.Position.X-position.X)+(enemy.Position.Y-position.Y)<(direction.X-position.X)+(direction.Y-position.Y))
                {
                    closestEnemy = enemy;
                }
                if(enemy.Position.X - position.X >= -(buffRange) && enemy.Position.X - position.X <= (buffRange) && enemy.Position.Y - position.Y <= (buffRange) && enemy.Position.Y - position.Y >= -(buffRange - 100))
                {
                    enemy.BuffEnemy(this);
                }
                else
                {
                    enemy.BeingBuffed = false;
                    Debug.WriteLine(this + " is not buffed");
                }
            }
            nearbyEnemy=closestEnemy;
            direction = new Vector2(closestEnemy.Position.X - position.X, closestEnemy.Position.Y - position.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
            velocity.Normalize();
            isFacingRight = velocity.X >= 0;
        }
    }
}
