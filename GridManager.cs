using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { private set; get; }
    
    [SerializeField] private Vector2Int gridDimension;
    [SerializeField] private GridCell[] cells;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GridCell CoordinateToCell(int x, int y)
    {
        if (x < 1 || y < 1 || x > gridDimension.x || y > gridDimension.y)
        {
            // Debug.LogWarning("Cell coordinate out of grid!");
            return null;
        }
        
        var index = (gridDimension.x * y) - (gridDimension.x - x) - 1;
        // print(index);
        return cells[index];
    }
}
