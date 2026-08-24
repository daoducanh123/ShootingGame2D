using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (GameManager.Instance == null) return;

        Debug.Log("EnergyOrb Taken"); 
        GameManager.Instance.IncreaseCurrentEnergy(1);
            
        Destroy(gameObject);
    }
}
