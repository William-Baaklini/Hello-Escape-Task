using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCell : MonoBehaviour
{
    public Vector2Int position;

    private GridCell GetNeighbourCell(Direction direction)
    {
        GridCell cell = null;
        switch (direction)
        {
            case Direction.North:
                cell = GridManager.Instance.CoordinateToCell(position.x, position.y - 1);
                break;
            case Direction.South:
                cell = GridManager.Instance.CoordinateToCell(position.x, position.y + 1);
                break;
            case Direction.East:
                cell = GridManager.Instance.CoordinateToCell(position.x + 1, position.y);
                break;
            case Direction.West:                
                cell = GridManager.Instance.CoordinateToCell(position.x - 1, position.y);
                break;
        }

        return cell;
    }

    public bool NeighbourCellEmpty(Direction direction)
    {
        GridCell cell = GetNeighbourCell(direction);
        return (cell != null && cell.isEmpty);
    }

    
}
