using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Ammo UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Gun")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private GameObject firePos;
    [SerializeField] private float reloadCooldown = 2f;

    private int currentAmmo;
    private bool isReloading = false;
    private float reloadTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    #region AmmoUI
    private void UpdateAmmoText()
    {
        if (ammoText == null) return;

        if (currentAmmo == 0)
        {
            ammoText.text = "Empty";
        }
        else
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
    #endregion

    #region Gun
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
    public bool IsReloading()
    {
        return isReloading;
    }

    private void GunReloading()
    {
        if (!isReloading && currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            if (AudioManager.Instance == null) return;
            AudioManager.Instance.PlayReloadingAudio(); 
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
    private void GunShooting()
    {
        if (!isReloading && currentAmmo > 0 && Input.GetMouseButtonDown(0))
        {
            if (AudioManager.Instance == null) return;
            if (PlayerBulletPool.Instance == null) return;

            AudioManager.Instance.PlayShootingAudio();
            --currentAmmo; UpdateAmmoText();


            GameObject bullet = PlayerBulletPool.Instance.GetBullet();
            if (bullet == null) return;
            if (firePos == null) return;
            bullet.transform.position = firePos.transform.position;
            bullet.transform.rotation = firePos.transform.rotation;
            bullet.GetComponent<PlayerBullet>().BulletMovement();              
        }   
    }
    #endregion
}

