using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    // instance variable: biến của object
    // nên ko dùng static để có thể tạo ra các loại đạn khác nhau với tốc độ, thời gian sống và sát thương khác nhau vì mỗi loại đạn sẽ có một prefab riêng với các giá trị khác nhau được gán trong inspector, nếu dùng static thì tất cả các viên đạn sẽ có cùng giá trị và ko thể tạo ra các loại đạn khác nhau được
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected float bulletLifetime = 2f;
    [SerializeField] protected float bulletDamage = 1f;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    protected virtual void Start()
    {

        
    }


    // =============== Bullet Collision =================

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }



}

