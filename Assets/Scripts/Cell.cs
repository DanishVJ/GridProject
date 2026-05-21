using UnityEngine;

public class Cell 
{
    public int xIndex;
    public int yIndex;
    public bool isWalkable = true;
    
    // Holds a reference to the physical tile sprite in the scene
    public GameObject VisualTile; 

    // A constructor to easily set it up when we create it
    public Cell(int x, int y, GameObject tileObj)
    {
        xIndex = x;
        yIndex = y;
        VisualTile = tileObj;
    }
}