using System;
using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts.Ai
{
    [RequireComponent(typeof (Avatar))]
    public class AiController : MonoBehaviour
    {
        private const int ATTACKING_DISTANCE = 7;
        private const float reflexes = 0.55f;
        private Avatar avatar;
        private Avatar avatarOpponent;
        private bool flee;
        private float waitTo;

        private void Start()
        {
            avatar = GetComponent<Avatar>();
            avatar.disableRunning(); //TODO take a look at this.
            avatarOpponent = avatar.Opponent.GetComponent<Avatar>();
        }

        private void Update()
        {
            if (Time.time < waitTo) return;

            if (avatar.CurrentState == Avatar.State.Blocking)
            {
                process(Avatar.Command.NoBlock);
            }

            var distance = avatar.Opponent.transform.position.Distance(avatar.transform.position);
            var facing = Math.Sign(avatar.Opponent.transform.position.x - gameObject.transform.position.x);


            if (avatarOpponent.CurrentState == Avatar.State.Attacking && distance <= ATTACKING_DISTANCE+5)
            {
                if (UnityEngine.Random.value*5f > 2.5f)
                {
                    process(Avatar.Command.Block, 1f);
                }
                else
                {
                    flee = true;
                }
            }

            if (avatarOpponent.CurrentState != Avatar.State.Attacking)
            {
                flee = false;
            }

            if (flee)
            {
                if (facing > 0)
                {
                    process(Avatar.Command.MoveLeft);
                }
                else
                {
                    process(Avatar.Command.MoveRight);
                }
                return;
            }

            if (avatar.Opponent.transform.position.Distance(avatar.transform.position) <= ATTACKING_DISTANCE)
            {
                process(Avatar.Command.Punch);
            }
            else if (avatar.Opponent.transform.position.Distance(avatar.transform.position) > ATTACKING_DISTANCE)
            {

                if (facing > 0)
                {
                    process(Avatar.Command.MoveRight);
                }
                else
                {
                    process(Avatar.Command.MoveLeft);
                }
            }
            else
            {
                process(Avatar.Command.MoveNone);
            }
        }

        private void process(Avatar.Command command, float f)
        {
            waitTo = Time.time + UnityEngine.Random.value*f;
            avatar.process(command);
        }

        private void process(Avatar.Command command)
        {
            waitTo = Time.time + reflexes;
            process(command, reflexes);
        }
    }
}