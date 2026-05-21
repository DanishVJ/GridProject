using UnityEngine;
using System.Collections;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    
    [SerializeField] private Transform cellLocation;

    void Start()
    {
        StartCoroutine(TimedCellCreator());
    }
    IEnumerator TimedCellCreator()
    {
        float startX = -4.5f;
        float startY = 4.5f;
        
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                float xPos = startX + col;
                float yPos = startY - row;
                
                Vector3 SpawnPosition = new Vector3 (xPos, yPos, 0);
                
                Instantiate(gridPrefab, SpawnPosition, Quaternion.identity, cellLocation);
                
                yield return new WaitForSeconds(0.25f);
            }
        }
        
    }
}
