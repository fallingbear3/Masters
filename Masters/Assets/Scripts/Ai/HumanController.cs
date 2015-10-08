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
        public KeyCode SpecialAttackA;
        public KeyCode SpecialAttackB;
        public KeyCode SpecialAttackC;

        private void Update()
        {
            GetComponent<Avatar>().move( Input.GetAxis("Horizontal"));

            if (Input.GetKey(CrouchKeyCode))
            {
                GetComponent<Avatar>().crouchDown();
            }
            else
            {
                GetComponent<Avatar>().crouchUp();
            }

            if (Input.GetKeyDown(JumpKeyCode))
            {
                GetComponent<Avatar>().jump();
            }
            if (Input.GetKeyDown(PunchKeyCode))
            {
                GetComponent<Avatar>().punch();
            }
            if (Input.GetKeyDown(SpecialAttackA))
            {
                GetComponent<Avatar>().executeSpecialAttackA();
                GetComponent<Animator>().SetTrigger("SpecialAttack");
            }
            if (Input.GetKeyDown(SpecialAttackB))
            {
                GetComponent<Avatar>().executeSpecialAttackB();
                GetComponent<Animator>().SetTrigger("SpecialAttack");
            }
            if (Input.GetKeyDown(SpecialAttackC))
            {
                GetComponent<Avatar>().executeSpecialAttackC();
                GetComponent<Animator>().SetTrigger("SpecialAttack");
            }
        }
    }
}