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

            CreateBulletEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            // Destroy the wall object
            print("Wall hit!");

            CreateBulletEffect(collision);
            
            Destroy(gameObject);
        }
        // Destroy the bullet after it collides with any object
        // Destroy(gameObject);
    }

    void CreateBulletEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)

        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
        // Instantiate bullet impact effect at the collision point
        // GameObject impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        // Destroy the impact effect after a short duration
        // Destroy(impactEffect, 2f);
    }


}
