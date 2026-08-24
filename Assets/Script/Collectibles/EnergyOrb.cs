using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    [SerializeField] private int energyValue = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (GameManager.Instance == null) return;

        Debug.Log("EnergyOrb Taken"); 
        GameManager.Instance.IncreaseCurrentEnergy(energyValue);
            
        Destroy(gameObject);
    }
}
