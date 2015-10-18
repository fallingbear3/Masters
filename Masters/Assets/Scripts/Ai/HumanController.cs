using UnityEngine;

namespace Assets.Scripts.Ai
{
    [RequireComponent(typeof(Avatar))]
    public class HumanController : MonoBehaviour
    {
        public KeyCode MoveLeftKeyCode;
        public KeyCode MoveRightKeyCode;
        public KeyCode JumpKeyCode;
        public KeyCode BlockKeyCode;
        public KeyCode PunchKeyCode;
        public KeyCode KickKeyCode;
        public KeyCode SpecialKeyCode;

        private void Update()
        {
            var avatar = GetComponent<Avatar>();
            if (Input.GetKey(MoveLeftKeyCode))
            {
                avatar.process(Avatar.Command.MoveLeft);
            }
            else if (Input.GetKey(MoveRightKeyCode))
            {
                avatar.process(Avatar.Command.MoveRight);
            }
            else
            {
                avatar.process(Avatar.Command.MoveNone);
            }

            if (Input.GetKey(JumpKeyCode)) avatar.process(Avatar.Command.Jump);
            if (Input.GetKey(BlockKeyCode)) avatar.process(Avatar.Command.Block);
            if (Input.GetKey(PunchKeyCode)) avatar.process(Avatar.Command.Punch);
            if (Input.GetKey(KickKeyCode)) avatar.process(Avatar.Command.Kick);
            if (Input.GetKey(SpecialKeyCode)) avatar.process(Avatar.Command.Special);
        }
    }
}