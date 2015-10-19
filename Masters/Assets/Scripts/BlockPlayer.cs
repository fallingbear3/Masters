using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Avatar))]
    public class BlockPlayer : MonoBehaviour
    {
        private Avatar avatar;
        private bool colliding;
        private bool calledLastUpdate;

        private void Awake()
        {
            avatar = GetComponent<Avatar>();
        }

        private void Update()
        {
            if (!colliding) avatar.AllowMovement = true;
            if (colliding && !calledLastUpdate) colliding = false;
            if (calledLastUpdate) calledLastUpdate = false;
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            var otherAvatar = collision.gameObject.GetComponent<Avatar>();

            if (otherAvatar && otherAvatar != avatar)
            {
                colliding = true;
                calledLastUpdate = true;
                var directiona = Math.Sign((avatar.transform.position - otherAvatar.transform.position).x);
                var directionb = Math.Sign(avatar.Direction);

                avatar.AllowMovement = directiona * directionb != -1;
            }
        }
    }
}