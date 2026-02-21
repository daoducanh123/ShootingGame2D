using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D reloadingCursor;
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D shootingCursor;
 
    private Gun gun;
    private Vector2 hotSpot;

    void Awake()
    {
        gun = FindAnyObjectByType<Gun>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hotSpot = new Vector2 (normalCursor.width /2, normalCursor.height /2);
        Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
            SetCursor();
    }

    private void SetCursor()
    {
        if (gun != null)
        {
            if (gun.IsReloading())
            {
                Cursor.SetCursor(reloadingCursor, hotSpot, CursorMode.Auto);
            }
            else if (!gun.IsReloading() && Input.GetMouseButton(0))
            {
                Cursor.SetCursor(shootingCursor, hotSpot, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
            }
        }
        else Debug.Log("gun null cursor ");
    }
}
