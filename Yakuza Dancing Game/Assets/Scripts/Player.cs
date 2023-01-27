using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Tile _currentTile;

    #region Movement
    public void MoveUp(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Tile newTile = _currentTile.GetUpNeighbor();
            MoveTo(newTile);
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Tile newTile = _currentTile.GetDownNeighbor();
            MoveTo(newTile);
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Tile newTile = _currentTile.GetLeftNeighbor();
            MoveTo(newTile);
        } 
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Tile newTile = _currentTile.GetRightNeighbor();
            MoveTo(newTile);
        }  
    }

    private void MoveTo(Tile newTile)
    {
        if(newTile)
        {
            transform.position = newTile.transform.position;
            _currentTile = newTile;
        }
    }
    #endregion


}
