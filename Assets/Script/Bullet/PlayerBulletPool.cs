using UnityEngine;
using System.Collections.Generic;
public class PlayerBulletPool : MonoBehaviour
{
    // ===================== Object Pooling =========================
    [SerializeField] private int poolSize = 40;
    [SerializeField] private  GameObject bulletPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>(); 
    public static PlayerBulletPool Instance;

    // ============= Singleton pattern =============
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }


        for (int i = 0; i < poolSize; ++i)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }


    public GameObject GetBullet()
    {
        if (pool.Count > 0)
        {
            Debug.Log("Bullet Dequeue");
            GameObject getBullet = pool.Dequeue();
            getBullet.SetActive(true);
            return getBullet;
        }
        else
        {
            Debug.Log("Pool empty return bulletTmp");
            GameObject bulletTMP = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            return bulletTMP;
        }
    }

    public void ReturnBullet(GameObject returnBullet) 
    {
        returnBullet.SetActive(false);
        pool.Enqueue(returnBullet);
    }
    
        

    
}
