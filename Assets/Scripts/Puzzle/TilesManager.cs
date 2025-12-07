using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public Tile[] tiles;

    private void Awake()
    {
        tiles = FindObjectsOfType<Tile>();
    }

    public bool AreAllTilesPlaced()
    {
        foreach (var tile in tiles)
        {
            if (!tile.isPlaced)
            {
                return false;
            }
        }
        return true;
    }
}
