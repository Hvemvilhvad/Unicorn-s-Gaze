using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{

    public class Player : Character
    {
        //Fields
        private float criticalMultiplier;
        private byte criticalChance;


        //Properties
        public float CriticalMultiplier { get => criticalMultiplier; set => criticalMultiplier = value; }


        //Constructor
        public Player(int health, Vector2 position, float speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
            MaxHealth = 10;
            isFacingRight = true;
        }


        //Methods

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("unicorn_sprite");
            }
            sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            HandleInput();
            Move(gameTime, screenSize);
            base.Update(gameTime, screenSize);
        }

        /// <summary>
        /// Handles player input
        /// </summary>
        private void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
                isFacingRight = false;
            }
            
            if (keyState.IsKeyDown(Keys.D))
            {
                isFacingRight = true;
                velocity += new Vector2(1, 0);
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }


            if (keyState.IsKeyDown(Keys.J)) //small adac
            {
                MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(criticalMultiplier, criticalChance, out bool isCrit), isCrit, isFacingRight, false, attackSprite);
                GameWorld.GameObjectsToAdd.Add(attack);
            }

            if (keyState.IsKeyDown(Keys.I)) //bick adac
            {
                MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(criticalMultiplier, criticalChance, out bool isCrit), isCrit, isFacingRight, true, attackSprite);
                GameWorld.GameObjectsToAdd.Add(attack);
            }

            if (keyState.IsKeyDown(Keys.O)) // pick up thing
            {

            }
        }


    }
}
