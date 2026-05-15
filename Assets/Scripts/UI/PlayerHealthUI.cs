using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthFillImage;

    private void Start()
    {
        if (playerHealth == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                playerHealth = playerObject.GetComponent<Health>();
            }
        }

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (playerHealth == null)
            return;

        if (healthFillImage == null)
            return;

        float healthPercent = playerHealth.CurrentHealth / playerHealth.MaxHealth;

        healthFillImage.fillAmount = healthPercent;
    }
}