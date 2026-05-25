using UnityEngine;

public enum CellState
{
    Empty,
    Wall,
    Enemy,
    PowerUp
}
    
public class Cell 
{
    public int xIndex;
    public int yIndex;
    public CellState currentState = CellState.Empty;
    
    // Holds a reference to the physical tile sprite in the scene
    public GameObject VisualTile; 

    // A constructor to easily set it up when we create it
    public Cell(int x, int y, GameObject tileObj)
    {
        xIndex = x;
        yIndex = y;
        VisualTile = tileObj;
        currentState = CellState.Empty;
    }

    public bool IsWalkable()
    {
        return currentState == CellState.Empty || currentState == CellState.PowerUp;
    }
}