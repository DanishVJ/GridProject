using UnityEngine;
using System.Collections;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private Transform cellLocation;

    // 1. Declare the 2D array container
    private Cell[,] gameGrid = new Cell[10, 10];

    void Start()
    {
        StartCoroutine(TimedCellCreator());
    }

    IEnumerator TimedCellCreator()
    {
        float startX = -4.5f;
        float startY = 4.5f;
        
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                float xPos = startX + col;
                float yPos = startY - row;
                Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
                
                // 2. Spawn the visual tile like you did before
                GameObject newTile = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, cellLocation);
                
                // 3. Create the data cell and link the visual tile to it
                Cell newCell = new Cell(col, row, newTile);
                
                // 4. Save this cell into our 2D array matrix
                gameGrid[col, row] = newCell;
                
                // Name the object in the hierarchy by its array coordinates
                newTile.name = "Cell_" + col + "_" + row;

                yield return new WaitForSeconds(0.1f);
            }
        }
        
        // Debug check to prove the array works after the grid finishes loading
        Debug.Log("Grid fully loaded! Cell [4,4] visual object is: " + gameGrid[4, 4].VisualTile.name);
    }
}