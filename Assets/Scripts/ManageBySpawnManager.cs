using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManageBySpawnManager : MonoBehaviour
{
    public UnityEvent onDestroyUnityEvent = new UnityEvent();

    private void OnDestroy()
    {
        onDestroyUnityEvent.Invoke();
    }
}
