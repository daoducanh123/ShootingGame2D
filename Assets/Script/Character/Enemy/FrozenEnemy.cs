using UnityEngine;

public class FrozenEnemy : Enemy
{
    // ================ CreateFrozen ==============
    private void CreateFrozen()
    {
        GameObject frozen = Instantiate(frozenPrefab, transform.position, Quaternion.identity);
        Destroy(frozen, 0.6f);
    }
    // =============== Die Frozen ================
    [SerializeField] GameObject frozenPrefab;
    protected override void Die()
    {
        CreateFrozen();
        base.Die();
    }

    // ================ Die Impact On Player ====================
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            CreateFrozen();
            base.Die();
        }
        
    }


}
