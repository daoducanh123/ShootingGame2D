using UnityEngine;

public class Frozen : MonoBehaviour
{
    [SerializeField] private float frozenDamage = 5f;
    private Character frozenCharacter;

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
