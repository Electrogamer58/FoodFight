using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DelayHelper //accessible from anywhere
{
    public static Coroutine DelayAction(this MonoBehaviour monoBehaviour, Action action, float delayDuration)
    {
        return monoBehaviour.StartCoroutine(DelayActionRoutine(action, delayDuration));
    }

    private static IEnumerator DelayActionRoutine(Action action, float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        action();
    }

}