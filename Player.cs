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
        private IThrowable heldObject;


        //Properties
        public float CriticalMultiplier { get => criticalMultiplier; set => criticalMultiplier = value; }


        //Constructor
        public Player(int health, Vector2 position, float speed)
        {
            MaxHealth = 10;
            Health = health;
            Position = position;
            this.speed = speed;
            IsFacingRight = true;
            DamageRange = new DamageRange(2, 5);
            criticalChance = 10;
            criticalMultiplier = 1.5F;
        }



        //Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("unicorn_sprite");
            }
            Sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            HandleInput();
            Move(gameTime, screenSize);
            attackCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                IsFacingRight = false;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                IsFacingRight = true;
                velocity += new Vector2(1, 0);
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }


            if (keyState.IsKeyDown(Keys.J) & attackCooldown <= 0) //small adac
            {
                if (heldObject is null)
                {
                    MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(criticalMultiplier, criticalChance, out bool isCrit), isCrit, IsFacingRight, false, attackSprite, 0.2F);
                    attackCooldown = attack.ExistanceTime + attack.Cooldown;
                    GameWorld.GameObjectsToAdd.Add(attack);
                }
                else
                {
                    attackCooldown = 0.5F;
                    heldObject.Throw();
                    heldObject = null;
                }
            }

            if (keyState.IsKeyDown(Keys.I) & attackCooldown <= 0) //bick adac
            {
                if (heldObject is null)
                {
                    MeleeAttack attack = new MeleeAttack(this, DamageRange.GetADamageValue(criticalMultiplier, criticalChance, out bool isCrit), isCrit, IsFacingRight, true, attackSprite, 0.5F);
                    attackCooldown = attack.ExistanceTime + attack.Cooldown;
                    GameWorld.GameObjectsToAdd.Add(attack);
                }
                else
                {
                    attackCooldown = 0.5F;
                    heldObject.Throw();
                    heldObject = null;
                }
            }

            if (keyState.IsKeyDown(Keys.O) & attackCooldown <= 0) // pick up thing
            {
                if (heldObject is null)
                {
                    foreach (GameObject other in GameWorld.GameObjects)
                    {
                        if (other is IThrowable)
                        {
                            if (Distance(other) <= 100)
                            {
                                (other as IThrowable).PickUp(this);
                                heldObject = (other as IThrowable);
                                attackCooldown = 0.5F;
                            }
                        }
                    }
                }
                else
                {
                    heldObject.Throw();
                    heldObject = null;
                }

            }

        }


        /// <summary>
        /// Keeps the gameObject on the designated "street"
        /// </summary>
        public override void CheckBounds(Vector2 screenSize)
        {
            base.CheckBounds(screenSize);

            if (position.X + (Sprite.Width / 2) > screenSize.X && enteredField)
            {
                position.X = screenSize.X - (Sprite.Width / 2);
                velocity.X = 0;
            }

            if (position.X - (Sprite.Width / 2) < 0 && enteredField)
            {
                position.X = Sprite.Width / 2;
                velocity.X = 0;
            }
        }


    }
}
