using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 25f;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        // Try to find Health on the object we hit or its parent.
        Health health = objectWeHit.gameObject.GetComponentInParent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Enemy hit
        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            print("Enemy hit!");

            CreateBulletEffect(objectWeHit);

            Destroy(gameObject);
            return;
        }

        // Map hit
        if (objectWeHit.gameObject.CompareTag("Map object"))
        {
            print("Map object hit!");

            CreateBulletEffect(objectWeHit);

            Destroy(gameObject);
            return;
        }

        // Bottle hit
        BeerBottle bottle = objectWeHit.gameObject.GetComponentInParent<BeerBottle>();

        if (bottle != null)
        {
            print("Bottle hit!");

            bottle.Shatter();

            // Bullet is not destroyed here because you wanted it to pass through bottles.
            return;
        }

        // Default behavior:
        // If bullet hits anything else, destroy it.
        Destroy(gameObject);
    }

    private void CreateBulletEffect(Collision objectWeHit)
    {
        if (GlobalReferences.Instance == null)
            return;

        if (GlobalReferences.Instance.bulletImpactEffectPrefab == null)
            return;

        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}