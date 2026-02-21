using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMedic : Enemy
{
    [SerializeField] private GameObject healthPackPrefab;
    protected override void Die()
    {
        GameObject heathPack = Instantiate(healthPackPrefab,transform.position,Quaternion.identity);
        base.Die();
        Destroy(heathPack,8f);
    }
}
