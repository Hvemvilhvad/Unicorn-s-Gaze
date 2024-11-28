using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_Gaze
{
    public interface IDamagable
    {
        public int Health { get; set; }

        /// <summary>
        /// Lowers the health of the Damagable when it takes damage.
        /// </summary>
        /// <param name="damage">The amount to lower it by.</param>
        void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
