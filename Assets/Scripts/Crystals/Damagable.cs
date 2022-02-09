using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crystals
{
    public abstract class Damagable : MonoBehaviour
    {
        protected int _health;

        public abstract bool TryDamage(int amount, out KeyValuePair<string, int> takenResource);
    }
}
