using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PositionHelper
{

    public const int Rows = 8;
    public const int Columns = 8;
    public const int TileSize = 1;


    public static Position GridPosition(Vector3 worldPosition)
    {
        var scaledWorldPosition = (worldPosition / TileSize);

        var gridPositionX = (int)(scaledWorldPosition.x + (Columns / 2) - 0.5f);
        var gridPositionY = (int)(scaledWorldPosition.z + (Rows / 2) - 0.5f);

        return new Position(gridPositionX, gridPositionY);
    }

    public static Vector3 WorldPosition(Position gridPosition)
    {
        var scaledWorldPositionX = gridPosition.X - (Columns / 2) + 0.5f;
        var scaledWorldPositionZ = gridPosition.Y - (Rows / 2) + 0.5f;

        return new Vector3(scaledWorldPositionX, 0, scaledWorldPositionZ) * TileSize;
    }
}


