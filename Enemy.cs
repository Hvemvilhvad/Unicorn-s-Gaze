using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private DamageRange enemyRange;

        //Constructor
        public Enemy(int health, Vector2 position, float speed)
        {
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
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Move(gameTime, screenSize);
            base.Update(gameTime, screenSize);
        }

        //Methods
        /// <summary>
        /// Instantiates the enemies attack
        /// </summary>
        public virtual void Attack()
        {
            MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(), false, isFacingRight, false, attackSprite);
            GameWorld.GameObjectsToAdd.Add(attack);
        }

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

    }
}
