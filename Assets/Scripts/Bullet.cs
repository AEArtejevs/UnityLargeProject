using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        // Check if the bullet collides with an object tagged as "Enemy"
        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            print("Enemy hit!");

            CreateBulletEffect(objectWeHit);

            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            // Destroy the wall object
            print("Wall hit!");

            CreateBulletEffect(objectWeHit);

            Destroy(gameObject);
        }
        // Destroy the bullet after it collides with any object
        // Destroy(gameObject);

        // if (objectWeHit.gameObject.CompareTag("Bottle"))
        // {
        //     // Destroy the beer bottle object
        //     print("Bottle hit!");

        //     objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
        //     // We wont destroy the bullet, because we want it to pass through the bottle and hit the wall behind it or destroy multiple bottles in row.
        //     // bullet will despawn after it lifespan is over.

        // }

        BeerBottle bottle = objectWeHit.gameObject.GetComponentInParent<BeerBottle>();

        if (bottle != null)
        {
            print("Bottle hit!");
            bottle.Shatter();
            return;
        }
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
