using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ActionsController : MonoBehaviour
{
    [SerializeField] float _spawnInterval;
    [SerializeField] List<Tile> _availableTiles;
    [SerializeField] Transform _actionsParent;
    [SerializeField] GameObject _actionPrefab;
    [SerializeField] Transform _player;
    [SerializeField] float _stepScoreModifier;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI _scoreCounter;

    [Header("References")]
    [SerializeField] FeverController _feverController;

    private Coroutine _spawnCoroutine;
    private bool _continue;
    private int _currentScore;
    private int _currentCombo;

    [SerializeField] List<ActionTile> _actionTiles;

    private void Start()
    {
        _currentScore = 0;  // Set score to 0
        _currentCombo = 0;  // Set combo to 0
    }

    // Start spawning
    public void StartSpawning()
    {
        _continue = true;
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    // Stop spawning
    public void StopSpawning()
    {
        _continue = false;
        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;

        ActionTile[] allTiles = _actionsParent.GetComponentsInChildren<ActionTile>();
        foreach(ActionTile tile in allTiles)
        {
            tile.DestroyMe();
        }

        _actionTiles.Clear();   // Remove all action tiles from the list
    }

    public void AddScore(int amount)
    {
        _currentScore += amount;
        _scoreCounter.text = _currentScore.ToString();
    }

    private Tile GetRandomAvailableTile()
    {
        int idx = Random.Range(0, _availableTiles.Count);
        return _availableTiles[idx];
    }

    private IEnumerator SpawnRoutine()
    {
        while(_continue)
        {
            Tile tile = GetRandomAvailableTile();   // Get tile for this action
            _availableTiles.Remove(tile);           // Remove tile from available tiles

            GameObject actionTileObj = Instantiate(_actionPrefab, tile.transform.position, Quaternion.identity, _actionsParent);    // Instantiate new action tile

            actionTileObj.GetComponent<ActionTile>().SetTile(tile);
            actionTileObj.GetComponent<ActionTile>().ActionCompleted += TileActionCompleted;    // Subscribe to ActionCompleted event on currently spawned action tile

            _actionTiles.Add(actionTileObj.GetComponent<ActionTile>());

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    // ActionCompleted event handler
    private void TileActionCompleted(object sender, ActionTile actionTile)
    {
        _availableTiles.Add(actionTile.Tile);
        _actionTiles.Remove(actionTile);
        _player.GetComponent<Player>().ClearTileSteps();
        //Destroy(actionTile.gameObject);
    }

    public void Action1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // Iterate over action tiles and check if player is standing on one
            foreach (ActionTile actionTile in _actionTiles)
            {
                if (actionTile.transform.position == _player.position)
                {
                    Player player = _player.gameObject.GetComponent<Player>();
                    int steps = player.GetTileSteps();
                    int scoreToAdd = actionTile.GetScore();

                    if (scoreToAdd > 0)
                    {
                        actionTile.Finish(true, steps);    // Finish this action and get score

                        // Modifie score based on number of steps
                        float modifier = (float)steps * _stepScoreModifier;
                        int finalScore = (int)(scoreToAdd * (modifier + 1.0f));
                        //Debug.Log("Score: " + finalScore);

                        // Change score
                        AddScore(finalScore);

                        // Change combo and fever progress
                        _currentCombo++;

                        if (_currentCombo > 2) _feverController.AddFeverProgress(1);
                        if (_currentCombo > 10) _feverController.AddFeverProgress(2);
                    }
                    else
                    {
                        actionTile.Finish(false, steps);
                        _currentCombo = 0;  // Reset combo
                    }

                    player.ClearTileSteps();
                    break;
                }
            }
        }
        
    }

}
