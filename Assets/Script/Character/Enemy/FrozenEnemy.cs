using UnityEngine;

public class FrozenEnemy : Enemy
{
    [SerializeField] GameObject frozenPrefab;
    
    private void CreateFrozen()
    {
        if (frozenPrefab == null) return;

        GameObject frozen = Instantiate(frozenPrefab, transform.position, Quaternion.identity);
        Destroy(frozen, 0.6f);
    }
    protected override void Die()
    {
        if (isDead) return;
        base.Die();
        CreateFrozen();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            Die();
        }
        
    }


}
