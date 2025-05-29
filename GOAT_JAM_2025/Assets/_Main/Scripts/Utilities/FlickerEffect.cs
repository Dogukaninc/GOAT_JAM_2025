using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Utilities
{
    public class FlickerEffect : MonoBehaviour
    {
        [field: SerializeField] public List<SpriteRenderer> Renderers { get; private set; }
        [SerializeField] private Color targetColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private float flickDuration;
        [SerializeField] private bool isCustomMaterial;

        public void FlickRenderers()
        {
            foreach (var renderer in Renderers)
            {
                if (isCustomMaterial)
                {
                    FlickCustomMaterial(renderer);
                }
                else
                {
                    ColorSequence(renderer);
                }
            }

            Debug.Log("Flicked!!!");
        }

        private void ColorSequence(SpriteRenderer renderer)
        {
            renderer.DOKill();
            var seq = DOTween.Sequence();
            var interval = flickDuration / 2;
            seq.Append(renderer.DOColor(targetColor, interval));
            seq.Append(renderer.DOColor(defaultColor, interval));
            seq.AppendCallback(() => { renderer.color = defaultColor; });
            seq.OnKill(() => renderer.color = defaultColor);
        }

        private void FlickCustomMaterial(SpriteRenderer renderer)
        {
            renderer.DOKill();
            // var seq = DOTween.Sequence();
            var interval = flickDuration / 2;
            DOVirtual.Float(renderer.material.GetFloat("_FlashAmount"), 1, interval, (value) => { renderer.material.SetFloat("_FlashAmount", value); });
        }
    }
}