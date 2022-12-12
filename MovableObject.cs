using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] private GameObject[] arrows;

    [SerializeField] private bool isObjective = false;

    [Header("Cells")] 
    [SerializeField] private ObjectCell[] allCells;
    [SerializeField] private ObjectCell[] northCells;
    [SerializeField] private ObjectCell[] southCells;
    [SerializeField] private ObjectCell[] eastCells;
    [SerializeField] private ObjectCell[] westCells;

    private void Start()
    {
        TakePlace();
    }

    private bool CanMove(Direction direction)
    {
        bool canMove = true;
        switch (direction)
        {
            case Direction.North:
                foreach (var cell in northCells)
                    if (!cell.NeighbourCellEmpty(direction))
                        canMove = false;
                break;
            case Direction.South:
                foreach (var cell in southCells)
                    if (!cell.NeighbourCellEmpty(direction))
                        canMove = false;
                break;
            case Direction.East:
                foreach (var cell in eastCells)
                    if (!cell.NeighbourCellEmpty(direction))
                        canMove = false;
                break;
            case Direction.West:
                foreach (var cell in westCells)
                    if (!cell.NeighbourCellEmpty(direction))
                        canMove = false;
                break;
        }

        return canMove;
    }

    public void SelectMovable()
    {
        MovableManager.Instance.SetSelectedMovable(this);
    }

    public void SelectObject()
    {
        arrows[0].SetActive(CanMove(Direction.North));
        arrows[1].SetActive(CanMove(Direction.South));
        arrows[2].SetActive(CanMove(Direction.East));
        arrows[3].SetActive(CanMove(Direction.West));
    }

    public void DeselectObject()
    {
        foreach (var arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }

    private void TakePlace()
    {
        foreach (var cell in allCells)
        {
            GridManager.Instance.CoordinateToCell(cell.position.x, cell.position.y).isEmpty = false;
        }
    }

    private void DisplaceCells(Direction direction)
    {
        foreach (var cell in allCells)
        {
            switch (direction)
            {
                case Direction.North:
                    cell.position.y -= 1;
                    break;
                case Direction.South:
                    cell.position.y += 1;
                    break;
                case Direction.East:
                    cell.position.x += 1;
                    break;
                case Direction.West:
                    cell.position.x -= 1;
                    break;
            }
        }
    }

    public void MoveObject(int arrow)
    {
        float amount = MovableManager.Instance.moveAmount;
        Direction direction = IntToDirection(arrow);
        
        foreach (var cell in allCells)
        {
            GridManager.Instance.CoordinateToCell(cell.position.x, cell.position.y).isEmpty = true;
        }
        DisplaceCells(direction);
        TakePlace();
        SelectObject();
        
        Vector3 targetPosition = transform.localPosition;
        switch (direction)
        {
            case Direction.North:
                targetPosition.y += amount;
                break;
            case Direction.South:
                targetPosition.y -= amount;
                break;
            case Direction.East:
                targetPosition.x += amount;
                break;
            case Direction.West:
                targetPosition.x -= amount;
                break;
        }
        transform.localPosition = targetPosition;

        if (isObjective)
        {
            if (allCells[0].position == MovableManager.Instance.victoryCoordinates)
            {
                MovableManager.Instance.GameVictory();
            }
        }
    }

    private Direction IntToDirection(int i)
    {
        Direction result;
        switch (i)
        {
            case 0:
                result = Direction.North;
                break;
            case 1:
                result = Direction.South;
                break;
            case 2:
                result = Direction.East;
                break;
            case 3:
                result = Direction.West;
                break;
            default:
                result = Direction.East;
                break;
        }

        return result;
    }
}
