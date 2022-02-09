using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
    public static class CoroutinesHelper
    {
        public static IEnumerator WaitAndExecuteAction(float timeout, Action action)
        {
            yield return new WaitForSeconds(timeout);

            action();
        }
    }
}