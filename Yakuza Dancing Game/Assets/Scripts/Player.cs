using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool CountSteps;

    [SerializeField] Tile _currentTile;

    private List<Tile> _currentSteppedTiles;

    private void Awake()
    {
        _currentSteppedTiles = new List<Tile>();
    }

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

            // Add tile to steps list
            if(CountSteps)
            {
                AddTileStep(newTile);
            }   
        }
    }
    #endregion

    public void ClearTileSteps()
    {
        _currentSteppedTiles.Clear();
    }

    public int GetTileSteps()
    {
        return _currentSteppedTiles.Count;
    }


    private void AddTileStep(Tile tile)
    {
        // Add tile if it was currently not stepped on
        if(!_currentSteppedTiles.Contains(tile))
        {
            _currentSteppedTiles.Add(tile);
        }
    }

}
