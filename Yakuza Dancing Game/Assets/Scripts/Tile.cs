using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _counter;
    [SerializeField] Image _backgound;

    [Header("Neighbors")]
    public Tile UpNeighbor = null;
    public Tile DownNeighbor = null;
    public Tile LeftNeighbor = null;
    public Tile RightNeighbor = null;

    #region GET Methods

    // Neighbors
    public Tile GetUpNeighbor() => UpNeighbor;
    public Tile GetDownNeighbor() => DownNeighbor;
    public Tile GetLeftNeighbor() => LeftNeighbor;
    public Tile GetRightNeighbor() => RightNeighbor;
    #endregion

    private void Start()
    {
        HideCounter();      // Hide counter at the beginning
    }

    public void RevealCounter(int n)
    {
        _backgound.color = new Color(_backgound.color.r, _backgound.color.g, _backgound.color.b, 0.25f);
        _counter.enabled = true;
        _counter.text = n.ToString();
    }

    public void HideCounter()
    {
        _backgound.color = new Color(_backgound.color.r, _backgound.color.g, _backgound.color.b, 0f);
        _counter.enabled = false;
    }







}
