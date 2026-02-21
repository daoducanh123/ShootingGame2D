using UnityEngine;

public class EnergyOrb : MonoBehaviour
{

    private GameManager gameManager;
    private Player player;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("EnergyOrb Taken"); gameManager.IncreaseCurrentEnergy(1);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy Take EnergyOrb");
        }
    }
}
