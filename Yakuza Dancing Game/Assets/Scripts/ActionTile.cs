using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ActionTile : MonoBehaviour
{
    public event EventHandler<ActionTile> ActionCompleted;    // Event handler for when this action is completed
    public Tile Tile;

    //[SerializeField] int _baseScore = 0;

    private ActionIcon _actionIcon;
    private int _currentScore;              // Real score should changed based on animation phase
    

    public void SetTile(Tile tile)
    {
        Tile = tile;
    }

    public void ChangeScore(int newScore)
    {
        _currentScore = newScore;
    }

    public int FinishSuccess()
    {
        OnActionCompleted();
        return _currentScore;
    }

    private void OnActionCompleted()
    {
        ActionCompleted.Invoke(this, gameObject.GetComponent<ActionTile>());      // Invoke ActionCompleted event
    }
}
