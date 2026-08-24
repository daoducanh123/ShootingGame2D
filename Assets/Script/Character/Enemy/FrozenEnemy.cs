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
        CreateFrozen();
        base.Die();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            CreateFrozen();
            base.Die();
        }
        
    }


}
