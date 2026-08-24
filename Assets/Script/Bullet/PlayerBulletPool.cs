using UnityEngine;
using System.Collections.Generic;
public class PlayerBulletPool : MonoBehaviour
{

    [SerializeField] private int poolSize = 40;
    [SerializeField] private  GameObject bulletPrefab;
    
    public static PlayerBulletPool Instance { get; private set; }
    private Queue<GameObject> pool = new Queue<GameObject>(); 

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

        CreatePool();
    }

    private void CreatePool()
    {
        if (bulletPrefab == null) return;

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
            if (bulletPrefab == null) return null;
            Debug.Log("Pool empty return bulletTmp");
            GameObject bulletTemp = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            return bulletTemp;
        }
    }

    public void ReturnBullet(GameObject returnBullet) 
    {
        returnBullet.SetActive(false);
        pool.Enqueue(returnBullet);
    }
}
