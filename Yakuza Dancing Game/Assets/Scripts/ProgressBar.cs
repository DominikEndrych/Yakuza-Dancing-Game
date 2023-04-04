using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public UnityEvent OnFull;
    public UnityEvent OnEmpty;

    [SerializeField] int _maximum;
    [SerializeField] int _current;

    [SerializeField] Image _fill;

    private bool _fullInvoked = false;
    private bool _emptyInvoked = false;

    private void Update()
    {
        GetCurrentFill();
    }

    public void AddFill(int amount)
    {
        _current += amount;
    }

    public void ResetFill()
    {
        _current = 0;
    }

    private void GetCurrentFill()
    {
        float fillAmount = (float)_current / (float)_maximum;
        _fill.fillAmount = fillAmount;

        // Check if bar is empty or full
        //Debug.Log("Fill amount: " + fillAmount);
        if(fillAmount >= 1)
        {
            if(!_fullInvoked)
            {
                OnFull.Invoke();
                _fullInvoked = true;
                Debug.Log("Progress full");
            }
            
        }
        else if(fillAmount <= 0)
        {
            if (!_emptyInvoked)
            {
                OnEmpty.Invoke();
                _emptyInvoked = true;
                Debug.Log("progress empty");
            }
        }
        else
        {
            _fullInvoked = false;
            _emptyInvoked = false;
        }
    }

}
