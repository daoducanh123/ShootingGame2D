using UnityEngine;
public class CursorManager : MonoBehaviour
{

    [Header("Cursor Textures")]
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D shootingCursor;
    [SerializeField] private Texture2D reloadingCursor;

    private Gun gun;

    private Vector2 cursorHotspot;
    private void Awake()
    {
        gun = FindAnyObjectByType<Gun>();
    }

    private void Start()
    {
        cursorHotspot = new Vector2(
            normalCursor.width / 2f,
            normalCursor.height / 2f
        );

        SetCursor(normalCursor);
    }

    private void Update()
    {
        UpdateCursor();
    }

    private void UpdateCursor()
    {
        if (gun == null)
            return;

        if (gun.IsReloading())
        {
            SetCursor(reloadingCursor);
        }
        else if (Input.GetMouseButton(0))
        {
            SetCursor(shootingCursor);
        }
        else
        {
            SetCursor(normalCursor);
        }
    }

    private void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(
            cursorTexture,
            cursorHotspot,
            CursorMode.Auto
        );
    }

}