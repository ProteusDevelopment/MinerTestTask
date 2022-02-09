using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extension
    {
        public static float Angle(this Vector3 vector)
        {
            if (vector.x < 0)
            {
                return 360 - (Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg * -1);
            }
            else
            {
                return Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
            }
        }
    }
}
