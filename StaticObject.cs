using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour
{
    [SerializeField] private ObjectCell[] allCells;

    private void Start()
    {
        TakePlace();
    }

    private void TakePlace()
    {
        foreach (var cell in allCells)
        {
            GridManager.Instance.CoordinateToCell(cell.position.x, cell.position.y).isEmpty = false;
        }
    }
}
