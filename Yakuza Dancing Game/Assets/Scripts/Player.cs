using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool CountSteps;

    [SerializeField] int _maxSteps;         // Max steps player can do for one action
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
            // Add tile to steps list
            if(CountSteps)
            {
                AddTileStep(_currentTile);
            }

            transform.position = newTile.transform.position;
            _currentTile = newTile;
        }
    }
    #endregion

    public Tile GetCurrentTile()
    {
        return _currentTile;
    }

    public void ClearTileSteps()
    {
        // Hide counters of all stepped tiles
        foreach(Tile tile in _currentSteppedTiles)
        {
            tile.HideCounter();
        }
        
        _currentSteppedTiles.Clear();       // Clear stepped tiles
    }

    public int GetTileSteps()
    {
        if(_currentSteppedTiles.Count > _maxSteps) { return _maxSteps; }
        else return _currentSteppedTiles.Count;

    }

    private void AddTileStep(Tile tile)
    {
        // Add tile if it was currently not stepped on
        if(!_currentSteppedTiles.Contains(tile))
        {
            _currentSteppedTiles.Add(tile);
            tile.RevealCounter(GetTileSteps());
        }
    }

}
