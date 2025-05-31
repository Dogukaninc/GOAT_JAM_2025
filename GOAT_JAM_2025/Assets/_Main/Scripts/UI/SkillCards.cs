using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
public class SkillCards : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private SkillEffect skillEffect;
    [SerializeField] private Vector3 rotationAmount;
    [SerializeField] private GameObject emptyObject;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private Vector3 originalCardHolderPosition;
    private GameObject cardHolder;
    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        cardHolder = rectTransform.parent.gameObject;
        originalCardHolderPosition = cardHolder.GetComponent<RectTransform>().position;
        originalPosition = rectTransform.position;
        originalRotation = rectTransform.rotation.eulerAngles;
        rectTransform.DOAnchorPosY(rectTransform.position.y + 150, 1f);
    }

    private void OnDisable(){
        cardHolder.GetComponent<RectTransform>().position = originalCardHolderPosition;
        rectTransform.position = originalPosition;
    }

    public void OnCursorEnter()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        rectTransform.DORotate(originalRotation + rotationAmount, 0.5f);
    }

    public void OnCursorExit()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        rectTransform.DOKill();
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        rectTransform.DORotate(originalRotation, 0.5f);
    }

    public void OnCursorClick()
    {
        if (GeneralValuesHolder.Instance.SkillSelected) return;
        StartCoroutine(OnCursorClickCoroutine());
    }
    
    private IEnumerator OnCursorClickCoroutine()
    {
        GeneralValuesHolder.Instance.SkillSelected = true;
        rectTransform.DOKill();
        rectTransform.DOAnchorPosY(originalPosition.y + 300, 1);
        rectTransform.DORotate(originalRotation + new Vector3(0, 180f, 0), 0.6f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.3f);
        emptyObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        rectTransform.DORotate(originalRotation - new Vector3(0, -1f, 0), 0.6f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.3f);
        emptyObject.SetActive(true);
        skillEffect.ApplyEffect(amount);        
        yield return new WaitForSeconds(0.3f);
        cardHolder.GetComponent<RectTransform>().DOAnchorPosY(originalCardHolderPosition.y - 1000, 0.5f);
        yield return new WaitForSeconds(0.5f);
        cardHolder.SetActive(false);


    }
}
