using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BarComponent : MonoBehaviour
    {
        public RectTransform bar;
        public float initial;
        public float max;
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

        private void Start()
        {
            maxBarSize = bar.sizeDelta.x;
            minBarSize = 0;
            step = (maxBarSize - minBarSize) / max;
            Value = initial;
        }

        private void updateUi(float value)
        {
            bar.sizeDelta = new Vector2(value*step, bar.sizeDelta.y);
        }
    }
}