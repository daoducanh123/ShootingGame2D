using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private float healValuePackage = 15f;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();    
        if (player != null)
        {
            Debug.Log("Health pack obtained");
            player.Healing(healValuePackage);
            Destroy(gameObject);
        }
    }
}
