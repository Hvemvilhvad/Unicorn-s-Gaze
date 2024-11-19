using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public struct DamageRange
    {
        private int lowerBound;
        private int upperBound;

        /// <summary>
        /// A DamageRange is an interval for damage-values.
        /// Its used to track and make damage-values.
        /// </summary>
        /// <param name="lowerBound">The lower inclusive bound of the interval.</param>
        /// <param name="upperBound">The upper inclusive bound of the interval.</param>
        public DamageRange(int lowerBound, int upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }


        /// <summary>
        /// Gets a random damage value.
        /// The randomization has a bellcurve distrubution.
        /// </summary>
        /// <returns>Returns a random damage value.</returns>
        public int GetADamageValue()
        {
            Random random = GameWorld.Random;
            return (int)(random.Next(lowerBound, upperBound + 1) / 2f + random.Next(lowerBound, upperBound + 1) / 2f);
        }

        /// <summary>
        /// Offsets the upper- and lower bound and returns itself.
        /// </summary>
        /// <param name="deltaDamage">The amount that is offset by.</param>
        /// <returns>Returns a copy of this DamageRange with offset bounds</returns>
        public DamageRange OffsetDamageRange(int deltaDamage)
        {
            lowerBound += deltaDamage;
            upperBound += deltaDamage;
            return this;
        }
    }

    public class Player : Character
    {
        //Fields
        private float criticalMultiplier;
        private DamageRange damageRange;
        
        
        //Properties
        public float CriticalMultiplier { get => criticalMultiplier; set => criticalMultiplier = value; }
        public DamageRange DamageRange { get => damageRange; set => damageRange = value; }
        

        //Constructor
        public Player(int health, Vector2 position, float speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
            MaxHealth = 10;
        }


        //Methods

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[0] = content.Load<Texture2D>("unicorn_sprite"); 
            }
            sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
        }
        
        private void HandleInput()
        {

        }
    }
}
