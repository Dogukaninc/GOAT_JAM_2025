using TMPro;
using UnityEngine;

namespace Main._Project.Scripts.Items.FloatingTexts.Base
{
    public abstract class FloatingText : MonoBehaviour
    {
        [SerializeField] protected TextMeshPro textField;
        [SerializeField] protected float upDuration;
        private Vector3 _initialScale;
        private Color _initialColor;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialColor = textField.color;
        }

        private void OnEnable()
        {
            transform.localScale = _initialScale;
            textField.color = _initialColor;
        }

        public abstract void SetValuesAndPlay(Vector3 position, string txt);
    }
}