using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject firePos;
    private GameManager gameManager;
    private AudioManager audioManager;
    PlayerBulletPool playerBulletPool;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBulletPool = PlayerBulletPool.Instance;
        audioManager = AudioManager.Instance;
        gameManager = GameManager.Instance;
        currentAmmo = maxAmmo;
        UpdateAmmoText();

    }

    // Update is called once per frame
    private void Update()
    {
        GunReloading(); 
        GunShooting(); 
        GunRotation();
    }

    // ================== Ammo Text =========================
    [SerializeField] private TextMeshProUGUI ammoText;

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            if (currentAmmo == 0)
            {
                ammoText.text = "Empty";

            }
            else
            {
                ammoText.text = currentAmmo.ToString();
            }
        }
    }


    // ================= Gun Ammo =================
    [SerializeField] private int maxAmmo = 10;
    private int currentAmmo;



    // ================= Gun Rotation =================
    private void GunRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 displacement = mousePos - transform.position; // vector displacement từ súng đến vị trí chuột, để tính góc xoay của súng so với trục x
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg; // atan2 trả về góc giữa vector displacement và trục x, * Mathf.Rad2Deg để chuyển từ radian sang độ
        transform.rotation = Quaternion.Euler(0, 0, angle ); // xoay quanh axis z vì đây là 2D, Gọi Quaternion xoay hộ tao với tọa độ hiển thị theo Euler rồi gán vào transform.rotation
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1); 
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // ================= Gun Reloading =================    
    public bool IsReloading()
    {
        return isReloading;
    }

    private bool isReloading = false;
    private float reloadTimer = 0f;
    [SerializeField] private float reloadCooldown = 2f;
    private void GunReloading()
    {
        if (!isReloading && currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            audioManager.PlayReloadingAudio(); 
            isReloading = true;
            reloadTimer = 0f;
        }

        else if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadCooldown)
            {
                currentAmmo = maxAmmo;
                UpdateAmmoText();
                isReloading = false;
            }
        }
        else return;


    }
    // ================= Gun Shooting =================

    private void GunShooting()
    {
        if (!isReloading && currentAmmo > 0 && Input.GetMouseButtonDown(0))
        {
            audioManager.PlayShootingAudio();

            --currentAmmo; UpdateAmmoText();
            if (playerBulletPool != null)
            {
                GameObject bullet = playerBulletPool.GetBullet(); 
                bullet.transform.position = firePos.transform.position;
                bullet.transform.rotation = firePos.transform.rotation;
                bullet.GetComponent<PlayerBullet>().BulletMovement();              
            }
        }   

    }
 }

