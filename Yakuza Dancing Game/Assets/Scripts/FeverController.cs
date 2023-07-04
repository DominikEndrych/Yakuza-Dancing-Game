using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class FeverController : MonoBehaviour
{
    public UnityEvent OnFeverStart;
    public UnityEvent OnFeverEnd;
    public UnityEvent OnFeverSuccess;
    public UnityEvent OnFeverDisable;

    [SerializeField] ProgressBar _feverProgressBar;
    [SerializeField] List<ActionTile> _feverActions;
    [SerializeField] PlayableDirector _cutscene;

    private int _expectedIndex;     // Witch action is expected to be performed
    private bool _feverReady;
    private bool _feverActive;
    private bool _feverSuccess;
    private bool _disabled;

    private void Start()
    {
        _disabled = false;
        _feverSuccess = true;
        SetFeverReady(false);   // Fever is not ready at start
        foreach (ActionTile action in _feverActions)
        {
            action.ActionCompleted += TileActionCompleted;
        }
    }

    // Change if player can use fever
    public void SetFeverReady(bool state)
    {
        _feverReady = state;

        // Change animation based on new state
        if (_feverReady)
        {
            _feverProgressBar.gameObject.GetComponent<Animator>().SetTrigger("Fever Ready");
            _feverSuccess = true;
        }
        else
        {
            _feverProgressBar.gameObject.GetComponent<Animator>().SetTrigger("Fever Not Ready");
        }
    }

    public void StartFever(InputAction.CallbackContext context)
    {
        if (context.performed && _feverReady && !_disabled)
        {
            _expectedIndex = 0;
            OnFeverStart.Invoke();
            //_cutscene.Play();
            _feverActive = true;
        }

    }

    public void EndFever()
    {
        OnFeverEnd.Invoke();        // Invoke OnFeverEnd event

        // Invoke OnFeverSuccess event if fever was success
        if (_feverSuccess)
        {
            OnFeverSuccess.Invoke();
            Debug.Log("Fever success!");
        }
        else
        {
            Debug.Log("Fever failed");
        }
    }

    public void DisableFever()
    {
        _disabled = true;
        OnFeverDisable.Invoke();
    }

    public void AddFeverProgress(float amount)
    {
        _feverProgressBar.AddFill(amount);  // Add fill to fever progress bar
    }

    #region Action calls
    public void FeverActionUp(InputAction.CallbackContext context)
    {
        if (context.performed && _feverActive) FeverAction(0);
    }

    public void FeverActionLeft(InputAction.CallbackContext context)
    {
        if (context.performed && _feverActive) FeverAction(1);

    }

    public void FeverActionDown(InputAction.CallbackContext context)
    {
        if (context.performed && _feverActive) FeverAction(2);

    }

    public void FeverActionRight(InputAction.CallbackContext context)
    {
        if (context.performed && _feverActive) FeverAction(3);

    }
    #endregion

    private void FeverAction(int actionIndex)
    {
        // Do something only if correct action is invoked
        if (actionIndex == _expectedIndex)
        {
            if (_feverActions[actionIndex].GetScore() > 0)
            {
                _feverActions[actionIndex].Finish(true, 0);
                Debug.Log("Correct fever action!");
            }
            else
            {
                /*
                // Remove all actions from current fever
                RemoveAllActions();
                _expectedIndex = -1;
                */
                _feverActions[actionIndex].Finish(false, 0);
                _feverSuccess = false;
            }
            _expectedIndex++;
        }
    }

    // ActionCompleted event handler
    private void TileActionCompleted(object sender, ActionTile actionTile)
    {
        //Debug.Log("Remove everything");
        if (actionTile.GetScore() <= 0)
        {
            //Debug.Log("Add score!");
            _feverSuccess = false;
        }
    }

    private void RemoveAllActions()
    {
        foreach (ActionTile action in _feverActions)
        {
            action.Finish(false, 0);
            action.ActionCompleted -= TileActionCompleted;
        }
    }

}
