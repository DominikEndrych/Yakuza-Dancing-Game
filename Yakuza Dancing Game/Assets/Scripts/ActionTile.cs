using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ActionTile : MonoBehaviour
{
    public event EventHandler<ActionTile> ActionCompleted;    // Event handler for when this action is completed
    public Tile Tile;

    private ActionIcon _actionIcon;
    

    public void SetTile(Tile tile)
    {
        Tile = tile;
    }

    private void OnActionCompleted()
    {
        ActionCompleted.Invoke(this, gameObject.GetComponent<ActionTile>());      // Invoke ActionCompleted event
    }
}
