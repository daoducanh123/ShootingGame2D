using UnityEngine;

public class Frozen : MonoBehaviour
{
    private Character frozenCharacter;

    // ================== Frozen ======================
    [SerializeField] private float frozenDamage = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        frozenCharacter = collision.GetComponent<Character>();
        if (frozenCharacter != null)
        {
            frozenCharacter.TakeDamage(frozenDamage);
            frozenCharacter.FrozenState();
        }
    }

  
}
