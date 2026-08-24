using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private float healValuePackage = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Health pack obtained");
        Player.Instance.Healing(healValuePackage);

        Destroy(gameObject);
    }
}
