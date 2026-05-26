using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    public Vector3 FacingDirection { get; private set; } = Vector3.up;

    public void SetFacing(Vector3 direction)
    {
        if (direction != Vector3.zero)
            FacingDirection = direction.normalized;
    }
}
