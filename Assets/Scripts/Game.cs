using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityEvent StartGameEvent;

    public void StartGame()
    {
        StartGameEvent.Invoke();
    }
}
