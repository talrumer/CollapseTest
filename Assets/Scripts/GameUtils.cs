using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils : MonoBehaviour
{

    #region singleton
    public static GameUtils Instance;
    public GameUtils()
    {
        Instance = this;
    }
    #endregion

    public static IEnumerator delayedInvoke(float seconds, System.Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    public void invokeIn(float seconds, System.Action action)
    {
        StartCoroutine(delayedInvoke(seconds, action));
    }

    public static void invokeInStatic(float seconds, System.Action action)
    {
        Instance.invokeIn(seconds, action);
    }
}
