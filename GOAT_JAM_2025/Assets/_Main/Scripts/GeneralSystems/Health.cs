using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GeneralSystems
{
    public class Health : MonoBehaviour
    {
        //TODO-> Health save dataya ihtiyaç duyabilir
        public bool isInDefense;
        [field: SerializeField] public TextMeshProUGUI HealthText { get; private set; }
        [field: SerializeField] public Slider HealthSlider { get; private set; }
        [field: SerializeField] public float CurrentHealth { get; set; }
        [field: SerializeField] public float MaxHealth { get; set; }

        private Tween _sliderTween;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            CurrentHealth = MaxHealth;
            UpdateHealthUI();
        }

        public void TakeDamage(float damage, Transform damageTextTransform, bool isRandomizePos = false, bool isCrit = false, Action onDeadAction = null)
        {
            if (isInDefense)
            {
                return;
            }
            
            if (CurrentHealth - damage <= 0)
            {
                var roundedHealth = (float)Math.Round(CurrentHealth, 2);
                CurrentHealth -= roundedHealth;
                UpdateHealthUI();
                onDeadAction?.Invoke();
            }
            else
            {
                var roundedHealth = (float)System.Math.Round(damage, 2);
                CurrentHealth -= roundedHealth;
            }

            // var damageText = PoolSystem.Instance.SpawnGameObject("DamageText");
            // damageText.GetComponent<DamageText>().SetText(damageTextTransform.position, damage.ToString(), isCrit, isRandomizePos);
            UpdateHealthUI();
        }

        public void Heal(float healValue, Transform healTextTransform, bool isCrit = false, bool isHeal = false)
        {
            if (CurrentHealth >= MaxHealth) return;
            CurrentHealth += healValue;
            if (CurrentHealth + healValue > MaxHealth)
            {
                healValue = MaxHealth - CurrentHealth;
                CurrentHealth = MaxHealth;
            }

            // var damageText = PoolSystem.Instance.SpawnGameObject("DamageText");
            // damageText.GetComponent<DamageText>().SetText(healTextTransform.position, healValue.ToString(), isCrit, isHeal);
            UpdateHealthUI();
        }

        public void RefillHealthBar()
        {
            CurrentHealth = MaxHealth;
            UpdateHealthUI();
        }

        public void UpdateHealthUI()
        {
            var roundedHealth = (float)System.Math.Round(CurrentHealth, 2);

            if (HealthText != null)
                HealthText.text = roundedHealth + "/" + MaxHealth;
            if (HealthSlider != null)
            {
                if (_sliderTween != null && _sliderTween.IsActive()) _sliderTween.Kill();
                _sliderTween = DOVirtual.Float(HealthSlider.value, roundedHealth / MaxHealth, 0.3f, x => { HealthSlider.value = x; });
                // HealthSlider.value = roundedHealth / MaxHealth;
            }
        }
    }
}