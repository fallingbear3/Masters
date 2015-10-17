using Assets.Shared.Scripts;
using UnityEngine;

namespace Assets.Scripts.Ai
{
    [RequireComponent(typeof(Avatar))]
    public class AiController : MonoBehaviour
    {

        private void Start()
        {
            GetComponentsInChildren<SpriteRenderer>().ForEach(sr => sr.sortingLayerName = "Avatar B");
        }
        private void Update()
        {
            if (GetComponent<Avatar>().PlayerProfile.PowerBar.Value == 100)
            {
                GetComponent<Avatar>().executeSpecialAttackA();
            }
            else
            {
                GetComponent<Avatar>().move(-0.2f);
                GetComponent<Avatar>().punch();
            }
        }
    }
}