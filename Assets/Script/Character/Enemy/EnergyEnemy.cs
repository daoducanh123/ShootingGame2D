using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField] private GameObject energyPrefab;
    protected override void Die()
    {
        if (isDead) return;
        base.Die();

        if (energyPrefab == null) return;
        GameObject energyOrb = Instantiate(energyPrefab, transform.position, Quaternion.identity);

        Destroy(energyOrb, 8f);
    }
}
