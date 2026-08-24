using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField] private GameObject energyPrefab;
    protected override void Die()
    {
        if (energyPrefab == null) return;

        GameObject energyOrb = Instantiate(energyPrefab, transform.position, Quaternion.identity);
        base.Die();
        Destroy(energyOrb, 8f);
    }
}
