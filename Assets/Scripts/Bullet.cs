using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            print("Enemy hit!");
            Destroy(collision.gameObject);
        }
        // Destroy the bullet after it collides with any object
        // Destroy(gameObject);
    }
}
