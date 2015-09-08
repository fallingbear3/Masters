using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Attack
{
    [RequireComponent(typeof(Animator))]
    public class SpecialAttack : MonoBehaviour
    {
        public void startAttack(Avatar caster)
        {
            GetComponentsInChildren<Damage>().ToList().ForEach(damage => damage.caster = caster);
            GetComponent<Animator>().SetTrigger("Execute");
        }
    }
}