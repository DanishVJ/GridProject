using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] private float fallSpeed = 3f;

    // set up by spawner
    private Vector3 _targetPosition;
    private bool _hasTarget = false;

    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        _hasTarget = true;
    }

    void Update()
    {
        if (!_hasTarget) return;

        transform.position = Vector3.MoveTowards(
            transform.position, _targetPosition, fallSpeed * Time.deltaTime);

        // destroys once it reaches the end of the grid if not picked up
        if (transform.position == _targetPosition)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerFacing facing = other.GetComponent<PlayerFacing>();
        Vector3 direction = facing != null ? facing.FacingDirection : Vector3.up;

        // this logic spawns a rectangular beam to destroy the enemies, in the direction the player is facing
        if (beamPrefab != null)
        {
            GameObject beam = Instantiate(beamPrefab, other.transform.position, Quaternion.identity);
            BeamProjectile beamScript = beam.GetComponent<BeamProjectile>();
            if (beamScript != null)
                beamScript.Launch(direction);
        }

        Destroy(gameObject);
    }
}
