using UnityEngine;

public class TestDamagePlayer : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.TakeDamage(10f);
        }
    }
}