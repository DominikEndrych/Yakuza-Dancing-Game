using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    public Transform tileTransform;
    public Transform player;

    private void Start()
    {
        player.position = tileTransform.position;
    }
}
