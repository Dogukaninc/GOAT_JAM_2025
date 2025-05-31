using DG.Tweening;
using Main._Project.Scripts.Items.FloatingTexts.Base;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main._Project.Scripts.Items.FloatingTexts
{
    public class DamageText : FloatingText
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color critColor;
        [SerializeField] private Color healColor;
        [SerializeField] private float scaleToReach;
        [SerializeField] private float critYPositionIncreaser;
        [SerializeField] private float yOffset;
        [SerializeField] private float posOffset;
        [SerializeField] private bool renderInFront;

        private TextMeshPro _tmpText;

        private Camera _camera;

        private void Awake()
        {
            _tmpText = GetComponent<TextMeshPro>();
            _camera = Camera.main;
        }

        private void Start()
        {
            if (renderInFront)
            {
                _tmpText.fontMaterial.renderQueue = 4000;
                Debug.Log("asdasdas" + _tmpText.fontMaterial.renderQueue);
            }
        }

        public void SetText(Vector3 position, string text, bool isCrit, bool isRandomizePos, bool isHeal = false)
        {
            if (isCrit) textField.color = critColor;
            else if (!isHeal) textField.color = defaultColor;
            else textField.color = healColor;
            // textField.color = isCrit ? critColor : defaultColor;
            var randomPos = position + Random.insideUnitSphere * posOffset; // Todo -> Bu metodun Vector2 ve Vector3 alan versiyonlarını yap. 
            position = isRandomizePos ? randomPos : position;
            position = isCrit ? position + new Vector3(0f, critYPositionIncreaser, 0f) : position;
            SetValuesAndPlay(position, text);
        }

        public override void SetValuesAndPlay(Vector3 position, string txt)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
            textField.text = txt;
            transform.localScale = Vector3.zero;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(Vector3.one * scaleToReach, upDuration).SetLoops(2, LoopType.Yoyo));
            seq.Join(transform.DOMoveY(transform.position.y + yOffset, upDuration * 2));
            seq.OnComplete(() => gameObject.SetActive(false));
        }
    }
}