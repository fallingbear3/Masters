using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BarComponent : MonoBehaviour
    {
        public RectTransform bar;
        public RectTransform livesBar;
        public float initial;
        public float max;
        public int lives;

        public Sprite Hearth;

        private float minBarSize;
        private float maxBarSize;
        private float step;
        private float value;

        public delegate void OnChangedHandler(float value, float changed);

        public event OnChangedHandler OnValueChanged;

        public float Value
        {
            get { return value; }
            set
            {
                var dif = value - this.value;
                this.value = Mathf.Clamp(value, 0, max);
                updateUi(this.value);
                if (OnValueChanged != null) OnValueChanged(value, dif);
            }
        }

        public void RemoveHeath()
        {
            if (livesBar.transform.childCount > 0)
            {
                var child = livesBar.GetChild(livesBar.transform.childCount - 1);
                Destroy(child.gameObject);
            }
        }

        public void ResetHealth()
        {
            Value = max;
        }

        private void Start()
        {
            maxBarSize = bar.sizeDelta.x;
            minBarSize = 0;
            step = (maxBarSize - minBarSize) / max;
            Value = initial;

            for (int i = 0; i < lives; i++)
            {
                var hearthGo = new GameObject("Hearth");

                var image = hearthGo.AddComponent<Image>();
                image.sprite = Hearth;
                image.preserveAspect = true;

                hearthGo.transform.SetParent(livesBar.transform);
                hearthGo.transform.localScale = Vector3.one;
            }
        }

        private void updateUi(float value)
        {
            bar.sizeDelta = new Vector2(value*step, bar.sizeDelta.y);
        }
    }
}