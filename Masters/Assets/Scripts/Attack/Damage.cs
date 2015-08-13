using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Attack
{
    public class Damage : MonoBehaviour
    {
        public float damage;

        public void OnTriggerEnter2D(Collider2D other)
        {
            var avatar = other.gameObject.GetComponent<Avatar>();
            if (avatar)
            {
                if (other.gameObject.GetComponentsInChildren<Damage>().All(damageComponent => damageComponent != this))
                {
                    avatar.PlayerProfile.Health -= damage;
                }
            }
        }
    }
}