using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerProfile : MonoBehaviour
    {
        public RectTransform healthbar;
        private float health;
        private float minHealth;
        private float maxHealth;
        private float healthStep;

        public float Health
        {
            get { return health; }
            set { health = Math.Max(0,value);
                updateHealth(health);
            }
        }

        private void Start()
        {
            health = 100;
            maxHealth = healthbar.sizeDelta.x;
            minHealth = 0;
            healthStep = (maxHealth - minHealth) / health;
        }

        private void updateHealth(float value)
        {
            healthbar.sizeDelta = new Vector2(value * healthStep, healthbar.sizeDelta.y);
        }
    }
}