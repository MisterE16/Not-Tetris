using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.CompilerServices;

public class TetrisManager : MonoBehaviour
{
    private TetrisGrid grid;
    public int score;
    private float gameTime;
    public float timeLimit = 60f;

    [SerializeField]
    GameObject gameOverText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TetrisSpawner spawner;
    [SerializeField]
    GameObject timerText;

    public enum GameState { Menu, Gameplay, GameOver, Options}

    public GameState gameState;
    void Start()
    {
        grid = FindObjectOfType<TetrisGrid>();

        gameTime = 0f;
    }

   
    void Update()
    {
        CheckGameOver();
        scoreText.text = "Score: " + score;

        timerText.GetComponent<TextMeshProUGUI>().text = "Time: " + gameTime.ToString("F2");

        gameTime += Time.deltaTime;
    }

    public void CalculateScore(int linesCleared)
    {
        switch(linesCleared)
        {
            case 1: score += 100;
                break;
            case 2: score += 300; 
                break;
            case 3: score += 500;
                break;
            case 4: score += 800;
                break;
        }
       /* switch (gameState)
        {
            case GameState.GameOver:
                break;
            case GameState.Gameplay: 
                break;
        }*/
    }

    public void CheckGameOver()
    {
        if (grid.IsCellOccupied(new Vector2Int((int)Mathf.Floor(grid.width / 2f), grid.height - 1)))
        {
           TriggerGameOver();
        }

        if (gameTime >= timeLimit && score < 800)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("Game Over");
        gameOverText.SetActive(true);
        spawner.gameObject.SetActive(false);
        gameState = GameState.GameOver;
        Invoke("ReloadScene", 5);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Tetris");
    }
}
