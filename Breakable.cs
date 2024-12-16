using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public class Breakable : Environment, IDamagable, IThrowable
    {
        private int health;
        private int normalHealth;
        private Vector2 startPosition;
        private bool hasBeenThrown;
        private float throwTime;
        private bool pickedUp;
        private Character following;

        public int Health
        {
            get => health;
            set
            {
                if (value <= 0)
                {
                    SpawnItem();
                    RemoveThis();
                }
                else
                {
                    health = value;
                }
            }
        }

        //ignore this, it's just for the interface
        public int NormalHealth
        {
            get => normalHealth; set => normalHealth=value;
        }

        public Vector2 StartPosition { get => startPosition; set => startPosition = value; }
        public bool HasBeenThrown { get => hasBeenThrown; set => hasBeenThrown = value; }
        public float ThrowTime { get => throwTime; set => throwTime = value; }
        public float InvincibilityTimer { get => invincibilityTimer; set => invincibilityTimer = value; }
        public float InvincibilityFrames { get; set; }
        public float HurtTimer { get; set; }
        public float HurtTime { get; set; }
        public bool TakingDamage { get; set; }
        public bool IsGoingRight { get; set; }
        public bool PickedUp { get => pickedUp; set => pickedUp = value; }
        public Character Following { get => following; set => following = value; }
        public SoundEffect HurtSound { get; set; }

        public Breakable(Vector2 position) : base()
        {
            Position = position;
            Health = 10;
            hasBeenThrown = false;
            throwTime = 0;
            doShadow = true;
            scale = 0.2f;
        }

        public override void LoadContent(ContentManager content)
        {
            //randomly decides the displayed sprite
            if (new Random().Next(0, 2) == 1)
            {
                sprite = content.Load<Texture2D>("barrel");
                sprites[SpriteType.Standard] = content.Load<Texture2D>("barrel");
            }
            else
            {
                sprite = content.Load<Texture2D>("box");
                sprites[SpriteType.Standard] = content.Load<Texture2D>("box");
            }
            base.LoadContent(content);
        }


        /// <summary>
        /// When a breakable is broken there is a chance it will spawn an item.
        /// </summary>
        public void SpawnItem()
        {
            //66% chance???
            if (GameWorld.Random.Next(0, 3 + 1) >= 2)
            {
                GameWorld.MakeObject(Item.GetRandomItem(Position));
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            ((IThrowable)this).UpdateThrow(gameTime, screenSize);
            base.Update(gameTime, screenSize);
        }

        public override bool OnCollision(GameObject other)
        {
            if (base.OnCollision(other))
            {
                if (this is IThrowable)
                {
                    (this as IThrowable).OnThrownCollision(other);
                    return true;
                }
            }
            return false;
        }
    }
}
