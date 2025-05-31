using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private RectTransform crosshairGroup;
    [SerializeField] private float expandAmount = 20f;
    [SerializeField] private float expandDuration = 0.05f;
    [SerializeField] private float shrinkDuration = 0.2f;

    private Vector2[] originalPositions;
    private RectTransform[] crosshairParts;
    private Vector2[] directions;

    void Start()
    {
        int count = crosshairGroup.childCount;
        originalPositions = new Vector2[count];
        crosshairParts = new RectTransform[count];
        directions = new Vector2[count];

        directions[0] = Vector2.down;
        directions[1] = Vector2.up;
        directions[2] = Vector2.right;
        directions[3] = Vector2.left;

        for (int i = 0; i < count; i++)
        {
            crosshairParts[i] = crosshairGroup.GetChild(i).GetComponent<RectTransform>();
            originalPositions[i] = crosshairParts[i].anchoredPosition;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Expand();
        }
    }

    public void Expand()
    {
        for (int i = 0; i < crosshairParts.Length; i++)
        {
            var rect = crosshairParts[i];
            var originalPos = originalPositions[i];
            DOTween.Kill(rect);

            Vector2 expandedPos = originalPos + directions[i] * expandAmount;

            rect.DOAnchorPos(expandedPos, expandDuration)
                .OnComplete(() => rect.DOAnchorPos(originalPos, shrinkDuration));
        }
    }
}
