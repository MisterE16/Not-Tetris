using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 23;
    private Transform[,] grid;
    TetrisManager tetrisManager;

    public GridCell[,] debugGrid;

    public class GridCell
    {
        public Vector3 position;
    }


    void Start()
    {
        debugGrid = new GridCell[width, height];
        grid = new Transform[width, height];

        tetrisManager = FindObjectOfType<TetrisManager>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                debugGrid[i, j] = new GridCell
                {
                    position = new Vector3(i, j, 0) // Set y to j to create vertical stacking
                };
            }
        }
    }




    public void AddToGrid(Transform piece)
    {
        foreach (Transform block in piece)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);
            grid[position.x, position.y] = block;
        }
    }

    public bool IsLineFull(int rowNumber)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, rowNumber] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearLine(int rowNumber)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, rowNumber].gameObject);
            grid[x, rowNumber] = null;
        }
    }

    public void ClearFullLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                ShiftRowsDown(y);
                y--; //recheck the current row after shifting
                linesCleared++;
            }
        }
        if (linesCleared > 0)
        {
            tetrisManager.CalculateScore(linesCleared);
        }
    }

    public void ShiftRowsDown(int clearedRow)
    {
        for (int y = clearedRow; y < height - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = grid[x, y + 1];
                if (grid[x, y] != null)
                {
                    grid[x, y].position += Vector3.down;
                }
                grid[x, y + 1] = null;

            }
        }
    }
    public bool IsCellOccupied(Vector2Int position)
    {
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)
        {
            return true; //out of bounds
        }
        return grid[position.x, position.y] != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        if (debugGrid != null)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    
                    Vector3 position = debugGrid[i, j].position;

                    
                    position.z = 0;

                 
                    Gizmos.DrawWireCube(position, new Vector3(1f, 1f, 0f)); 
                }
            }
        }
    }

}
