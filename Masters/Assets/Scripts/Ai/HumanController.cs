using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts.Ai
{
    [RequireComponent(typeof(Avatar))]
    public class HumanController : MonoBehaviour
    {
        public KeyCode CrouchKeyCode;
        public KeyCode JumpKeyCode;
        public KeyCode PunchKeyCode;
        public KeyCode MusicWaveCode;
        public KeyCode CoinTossCode;

        private void Update()
        {
            GetComponent<Avatar>().move( Input.GetAxis("Horizontal"));

            if (Input.GetKeyDown(CrouchKeyCode))
            {
                GetComponent<Avatar>().crouch();
            }
            if (Input.GetKeyDown(JumpKeyCode))
            {
                GetComponent<Avatar>().jump();
            }
            if (Input.GetKeyDown(PunchKeyCode))
            {
                GetComponent<Avatar>().punch();
            }
            if (Input.GetKeyDown(CoinTossCode))
            {
                GetComponent<Avatar>().executeSpecialAttack();
                GetComponent<Animator>().SetTrigger("SpecialAttack");
            }
        }
    }
}