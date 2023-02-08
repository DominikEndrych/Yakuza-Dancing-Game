using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart;

    private void Start()
    {
        OnGameStart.Invoke();
    }
}
