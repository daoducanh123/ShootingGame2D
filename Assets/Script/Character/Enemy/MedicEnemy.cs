using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMedic : Enemy
{
    [SerializeField] private GameObject healthPackPrefab;
    protected override void Die()
    {
        if (isDead) return;

        base.Die();

        if (healthPackPrefab == null) return;
        GameObject heathPack = Instantiate(healthPackPrefab,transform.position,Quaternion.identity);
        Destroy(heathPack,8f);
    }
}
