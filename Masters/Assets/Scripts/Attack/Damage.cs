using UnityEngine;

namespace Assets.Scripts.Attack
{
    public class Damage : MonoBehaviour
    {
        public Avatar caster;
        public float damage;

        public void OnTriggerEnter2D(Collider2D other)
        {
            var avatar = other.gameObject.GetComponent<Avatar>();
            if (avatar && avatar != caster)
            {
                avatar.PlayerProfile.HealthBar.Value -= damage;
                avatar.takeDamage();
                caster.PlayerProfile.PowerBar.Value += 20;
            }
        }
    }
}