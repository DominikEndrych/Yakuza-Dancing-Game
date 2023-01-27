using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{


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






}
