using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Avatar))]
    public class BlockPlayer : MonoBehaviour
    {
        private Avatar avatar;

        private void Start()
        {
            avatar = GetComponent<Avatar>();
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            var otherAvatar = collision.gameObject.GetComponent<Avatar>();

            if (otherAvatar && otherAvatar != avatar)
            {
                var directiona = Math.Sign((avatar.transform.position - otherAvatar.transform.position).x);
                var directionb = Math.Sign(avatar.Direction);

                avatar.AllowMovement = directiona * directionb != -1;
            }
        }
    }
}