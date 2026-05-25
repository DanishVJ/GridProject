using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Enemy's collider triggers the player's 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}