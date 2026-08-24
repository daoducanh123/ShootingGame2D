using UnityEngine;

public class Fire : MonoBehaviour
{
    private Character firedCharacter;

    [SerializeField] private float fireDamagePerSecond = 5f;
    [SerializeField] private GameObject fireEffect; // fire2Prefab
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (fireEffect == null) return;

        firedCharacter = other.GetComponent<Character>();
        {
            if (firedCharacter != null)
            {
                firedCharacter.FireState(fireDamagePerSecond, fireEffect);
            }
        }
    }



}

