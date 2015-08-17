using UnityEngine;

namespace Assets.Scripts
{
    public class NavigationManager : MonoBehaviour
    {

        public GameObject fightScreen;
        public GameObject avatar;

        private void Start()
        {
            fightScreen.SetActive(false);
        }

        public void fight()
        {
            avatar.SetActive(false);
            fightScreen.SetActive(true);
        }
        public void menu()
        {
            avatar.SetActive(true);
            fightScreen.SetActive(false);
        }
    }
}