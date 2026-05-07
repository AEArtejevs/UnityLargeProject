using UnityEngine;

public class GlobalReferences : MonoBehaviour
{

    public static GlobalReferences Instance { get; set; }

    public GameObject bulletImpactEffectPrefab;
    // Update is called once per frame
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
}
