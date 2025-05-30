using UnityEngine;

public class CursorCrosshair : MonoBehaviour
{
    [Header("Crosshair Settings")]
    [SerializeField] private bool smoothFollow = true;
    [SerializeField] private float smoothSpeed = 25f;
    
    private RectTransform _crosshairRect;
    private Canvas _canvas;
    private Vector2 _targetPosition;

    void Start()
    {
        _crosshairRect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false;
        
        // Initialize position
        _targetPosition = _crosshairRect.anchoredPosition;
    }

    void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            Input.mousePosition,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out mousePos
        );

        if (smoothFollow)
        {
            _targetPosition = mousePos;
            _crosshairRect.anchoredPosition = Vector2.Lerp(
                _crosshairRect.anchoredPosition,
                _targetPosition,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            _crosshairRect.anchoredPosition = mousePos;
        }
    }
}
