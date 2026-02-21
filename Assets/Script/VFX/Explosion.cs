using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionDamage = 15f;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();
        {
            if (character != null)
            {
                character.TakeDamage(explosionDamage);
            }
        }
    }

}
