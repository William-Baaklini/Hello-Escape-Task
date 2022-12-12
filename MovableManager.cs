using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovableManager : MonoBehaviour
{
    public static MovableManager Instance { private set; get; }

    [SerializeField] private GameObject victoryCanvas;
    public Vector2Int victoryCoordinates;
    
    public MovableObject selectedMovable;
    public float moveAmount = 205f;

    private bool _gameOver = false;

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

    public void SetSelectedMovable(MovableObject movable)
    {
        if (_gameOver) return;
        if(selectedMovable) selectedMovable.DeselectObject();
        movable.SelectObject();
        selectedMovable = movable;
    }

    public void GameVictory()
    {
        _gameOver = true;
        victoryCanvas.SetActive(true);
        selectedMovable.DeselectObject();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
