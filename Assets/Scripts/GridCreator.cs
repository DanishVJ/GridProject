using UnityEngine;
using System.Collections;

public class GridCreator : MonoBehaviour
{
    //Singleton setup
    public static GridCreator Instance { get; private set; }
    
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private Transform cellLocation;

    // 1. Declare the 2D array container
    private Cell[,] gameGrid = new Cell[9, 9];

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        else {Destroy(gameObject);}
    }
    
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        float startX = -4f;
        float startY = 4f;
        
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
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
                newTile.name = "Cell_" + col + "_" + row; ;
            }
        }
        
        // Debug check to prove the array works after the grid finishes loading
        Debug.Log("Grid fully loaded!");
    }

    public Cell GetCellFromWorldPosition(Vector3 worldPosition)
    {
        float startX = -4f;
        float startY = 4f;

        // 1. Reverse-engineer the position math from your loop
        int col = Mathf.RoundToInt(worldPosition.x - startX);
        int row = Mathf.RoundToInt(startY - worldPosition.y);

        // 2. Safety check: Make sure the coordinates are safely inside your 10x10 array bounds
        if (col >= 0 && col < 9 && row >= 0 && row < 9)
        {
            return gameGrid[col, row];
        }

        return null; // Return nothing if it's out of bounds (off the grid)
    }
    
}