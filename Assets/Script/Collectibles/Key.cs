using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance == null) return;
        if (collision.gameObject.CompareTag("Player")){
            GameManager.Instance.GameWinMenu();
            Debug.Log("Win!");
            Destroy(gameObject);
        }
    }
}
