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

            if (Input.GetKeyDown(BlockKeyCode)) avatar.process(Avatar.Command.Block);
            if (Input.GetKeyUp(BlockKeyCode)) avatar.process(Avatar.Command.NoBlock);
            if (Input.GetKeyDown(JumpKeyCode)) avatar.process(Avatar.Command.Jump);
            if (Input.GetKeyDown(BlockKeyCode)) avatar.process(Avatar.Command.Block);
            if (Input.GetKeyDown(PunchKeyCode)) avatar.process(Avatar.Command.Punch);
        }
    }
}