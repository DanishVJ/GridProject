using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Adjust this to make them faster/slower
    private Vector3 _targetPosition;
    private bool _hasTarget = false;

    // The Spawner will call this public function right when the enemy is born
    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        _hasTarget = true;
        
        Cell targetCell = GridCreator.Instance.GetCellFromWorldPosition(target);
        if (targetCell != null) { targetCell.currentState = CellState.Enemy; }
    }

    void Update()
    {
        // Only move if a target has actually been assigned
        if (_hasTarget)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        float step = speed * Time.deltaTime;
        
        Cell currentCell = GridCreator.Instance.GetCellFromWorldPosition(transform.position);
        
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
        
        if (currentCell != null && GridCreator.Instance.GetCellFromWorldPosition(transform.position) != currentCell)
        {
            currentCell.currentState = CellState.Empty;
        }
        
       if (transform.position == _targetPosition)
        {
            Cell targetCell = GridCreator.Instance.GetCellFromWorldPosition(_targetPosition);
            if (targetCell != null) { targetCell.currentState = CellState.Empty; }
            
            Destroy(gameObject);
        }
    }
}