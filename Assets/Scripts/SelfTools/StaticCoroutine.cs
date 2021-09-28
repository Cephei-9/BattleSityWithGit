using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoroutine
{
    public static IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
