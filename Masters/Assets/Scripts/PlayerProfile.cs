using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerProfile : MonoBehaviour
    {
        public delegate void OnHealthChangedHandler(float value);

        public event OnHealthChangedHandler OnHealthChanged;

        public RectTransform healthbar;
        private float health;
        private float minHealth;
        private float maxHealth;
        private float healthStep;

        public float Health
        {
            get { return health; }
            set
            {
                health = Math.Max(0, value);
                updateHealthUi(health);
                if (OnHealthChanged != null) OnHealthChanged(value);
            }
        }

        private void Start()
        {
            health = 100;
            maxHealth = healthbar.sizeDelta.x;
            minHealth = 0;
            healthStep = (maxHealth - minHealth) / health;
        }

        private void updateHealthUi(float value)
        {
            healthbar.sizeDelta = new Vector2(value * healthStep, healthbar.sizeDelta.y);
        }
    }
}