using System;
using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts.Ai
{
    [RequireComponent(typeof (Avatar))]
    public class AiController : MonoBehaviour
    {
        private const int ATTACKING_DISTANCE = 7;
        private Avatar avatar;
        private Avatar avatarOpponent;
        private bool flee;

        private void Start()
        {
            avatar = GetComponent<Avatar>();
            avatarOpponent = avatar.Opponent.GetComponent<Avatar>();
        }

        private void Update()
        {
            if (avatar.PlayerProfile.PowerBar.Value == 100)
            {
                avatar.process(Avatar.Command.Special);
                return;
            }

            var distance = avatar.Opponent.transform.position.Distance(avatar.transform.position);
            var facing = Math.Sign(avatar.Opponent.transform.position.x - gameObject.transform.position.x);


            if (avatarOpponent.CurrentState == Avatar.State.Attacking && distance <= ATTACKING_DISTANCE+5)
            {
                flee = true;
            }

            if (avatarOpponent.CurrentState != Avatar.State.Attacking)
            {
                flee = false;
            }

            if (flee)
            {
                if (facing > 0)
                {
                    avatar.process(Avatar.Command.MoveLeft);
                }
                else
                {
                    avatar.process(Avatar.Command.MoveRight);
                }
                return;
            }

            if (avatar.Opponent.transform.position.Distance(avatar.transform.position) <= ATTACKING_DISTANCE)
            {
                avatar.process(Avatar.Command.Punch);
            }
            else if (avatar.Opponent.transform.position.Distance(avatar.transform.position) > ATTACKING_DISTANCE)
            {

                if (facing > 0)
                {
                    avatar.process(Avatar.Command.MoveRight);
                }
                else
                {
                    avatar.process(Avatar.Command.MoveLeft);
                }
            }
            else
            {
                avatar.process(Avatar.Command.MoveNone);
            }
        }
    }
}