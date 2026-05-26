using UnityEngine;

public class BeamProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    
    [SerializeField] private float beamWidthInCells = 3f;
    [SerializeField] private float maxTravelDistance = 11f;
    [SerializeField] private float beamThickness = 1f;

    private Vector3 _direction;
    private float _distanceTravelled = 0f;
    private bool _launched = false;

    public void Launch(Vector3 direction)
    {
        _direction = direction.normalized;
        _launched = true;

        // rotate the beam
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        
        transform.localScale = new Vector3(beamWidthInCells, beamThickness, 1f);
    }

    void Update()
    {
        if (!_launched) return;

        float step = speed * Time.deltaTime;
        transform.position += _direction * step;
        _distanceTravelled += step;

        if (_distanceTravelled >= maxTravelDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // clear the cell state before destroying
            Cell enemyCell = GridCreator.Instance.GetCellFromWorldPosition(other.transform.position);
            if (enemyCell != null)
                enemyCell.currentState = CellState.Empty;

            Destroy(other.gameObject);
        }
    }
}