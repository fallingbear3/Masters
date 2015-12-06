using UnityEngine;

namespace Assets.Scripts
{
    internal class LevelCountDown : MonoBehaviour
    {
        private Avatar player;
        private Avatar enemy;

        public void Start()
        {
            player = GameObject.FindGameObjectWithTag("asdfasdfads").GetComponent<Avatar>();
            enemy = GameObject.FindGameObjectWithTag("asdfasdfasdfasaf").GetComponent<Avatar>();

            player.Active = false;
            enemy.Active = false;
        }

        public void StartTheGame()
        {
            player.Active = true;
            enemy.Active = true;
        }
    }
}