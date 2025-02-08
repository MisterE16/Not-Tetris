using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{

    public GameObject[] tetrominoPrefabs;
    //public List<GameObject> tetrominoes;
    private TetrisGrid grid;
    private GameObject nextPiece;
   
    
    void Start()
    {
        

        grid = FindObjectOfType<TetrisGrid>();
        if (grid == null)
        {
            return;
        }
        SpawnPiece(); //initial piece spawn
    }

    
   public void SpawnPiece()
    {
        //calculate the top center of the grid, and spawn there
        Vector3 spawnPosition = new Vector3(Mathf.Floor(grid.width / 2f), grid.height - 1, 0);
        
        if (nextPiece != null)
        {
            nextPiece.SetActive(true);

            nextPiece.transform.position = spawnPosition;
        }
        else
        {
            nextPiece = InstantiateRandomPiece();
            nextPiece.transform.position = spawnPosition;
        }

        nextPiece = InstantiateRandomPiece();
        nextPiece.SetActive(false);

    }

    private GameObject InstantiateRandomPiece()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length);
        return Instantiate(tetrominoPrefabs[index]);
    }
}
