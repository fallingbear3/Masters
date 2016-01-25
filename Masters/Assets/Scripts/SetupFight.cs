using Shared.Scripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class SetupFight : MonoBehaviour
    {

        public FighterTypes playerType;
        public GameObject player;
        public GameObject enemy;

        private void Awake()
        {
            player.GetComponentInChildren<SuitUp>().FighterType = playerType;
            enemy.GetComponentInChildren<SuitUp>().FighterType = FighterTypes.Rocc;
        }
    }
}