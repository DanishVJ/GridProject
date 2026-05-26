using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    public Vector3 FacingDirection { get; private set; } = Vector3.up;

    public void SetFacing(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            FacingDirection = direction.normalized;

            // 1. Calculate the base angle from movement
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 2. Subtract 90 degrees so it perfectly matches your beam's rotation math!
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}