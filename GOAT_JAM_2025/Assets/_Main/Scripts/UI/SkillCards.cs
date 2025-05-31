using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class SkillCards : MonoBehaviour
{
    [SerializeField] private GameObject messageBox;
    [SerializeField] private float amount;
    [SerializeField] private SkillEffect skillEffect;
    [SerializeField] private Vector3 rotationAmount;
    [SerializeField] private float positionAmount;
    [SerializeField] private GameObject emptyObject;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject text;

    private RectTransform rectTransform;
    private Vector3 originalRotation;
    private Vector3 originalCardHolderPosition;
    private GameObject cardHolder;
    private Vector3 targetPosition;

    private void Start()
    {
        icon.GetComponent<Image>().sprite = skillEffect.skillIcon;
        text.GetComponent<TextMeshProUGUI>().text = skillEffect.skillName;
        //CloseCharacterInput
        rectTransform = GetComponent<RectTransform>();
        cardHolder = rectTransform.parent.gameObject;
        originalCardHolderPosition = cardHolder.GetComponent<RectTransform>().position;
        originalPosition = rectTransform.position;
        originalRotation = rectTransform.rotation.eulerAngles;
        targetPosition = rectTransform.position + (rectTransform.up * Screen.height/2);
        rectTransform.DOMove(targetPosition, 1f);

    }

    public void OnCursorEnter()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        var seq = DOTween.Sequence();
        seq.Append(rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
        seq.Join(rectTransform.DOMove(targetPosition + rectTransform.up * 30, 0.5f));

    }

    public void OnCursorExit()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        var seq = DOTween.Sequence();
        seq.Append(rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
        seq.Join(rectTransform.DOMove(targetPosition, 0.5f));
    }

    public void OnCursorClick()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        StartCoroutine(OnCursorClickCoroutine());
    }

    private IEnumerator OnCursorClickCoroutine()
    {
        GeneralValuesHolder.Instance.SkillSelected = true;
        rectTransform.DOLocalMoveY(originalPosition.y + 300, 1);
        rectTransform.DORotate(originalRotation + new Vector3(0, 180f, 0), 0.6f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.3f);
        emptyObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        rectTransform.DORotate(originalRotation - new Vector3(0, -1f, 0), 0.6f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.3f);
        emptyObject.SetActive(true);
        skillEffect.ApplyEffect(amount);
        yield return new WaitForSeconds(0.3f);
        cardHolder.GetComponent<RectTransform>().DOMove(originalCardHolderPosition - rectTransform.up * Screen.height, 0.75f);
        yield return new WaitForSeconds(0.75f);
        messageBox.GetComponent<RectTransform>().GetChild(0).GetComponent<TextMeshProUGUI>().text = skillEffect.skillDescription;
        messageBox.SetActive(true);
        messageBox.GetComponent<RectTransform>().DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0f), 0.75f);
    }

    public void OnCloseButtonPressed () {
        Destroy(cardHolder);
        //openCharacterInput
    }

}
