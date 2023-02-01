using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ActionTile : MonoBehaviour
{
    public event EventHandler<ActionTile> ActionCompleted;    // Event handler for when this action is completed
    public Tile Tile;

    [SerializeField] TextMeshProUGUI _finalStepsNumber;

    private ActionIcon _actionIcon;
    private int _currentScore;              // Real score should changed based on animation phase
    private Animator _animator;

    private bool _wasSuccess = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetTile(Tile tile)
    {
        Tile = tile;
    }

    public void ChangeScore(int newScore)
    {
        _currentScore = newScore;
    }

    public int GetScore() => _currentScore;

    public void Finish(bool wasSuccess, int steps)
    {
        _wasSuccess = wasSuccess;

        _finalStepsNumber.text = steps.ToString();
        OnActionCompleted();
    }

    private void OnActionCompleted()
    {
        ActionCompleted.Invoke(this, gameObject.GetComponent<ActionTile>());      // Invoke ActionCompleted event

        // Start success finish animation
        if (_wasSuccess)
        {
            if (_animator)
            {
                _animator.SetTrigger("Finish Success");
            }

        }
        // Start fail finish animation
        else
        {
            if(_animator)
            {
                _animator.SetTrigger("Finish Fail");
            }
        }
    }

    // Destroy this action
    // This is be triggered in finish animation
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
