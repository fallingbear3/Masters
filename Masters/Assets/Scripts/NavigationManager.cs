using UnityEngine;

namespace Assets.Scripts
{
    public class NavigationManager : MonoBehaviour
    {

        public GameObject menuSceen;
        public GameObject howToPlayScreen;
        public GameObject fightScreen;

        private GameObject[] allScenes;

        private void Start()
        {
            allScenes = new[] {menuSceen, howToPlayScreen, fightScreen};
            setScene(menuSceen);

            Invoke("howToPlay", 5);
        }

        private void setScene(GameObject newScene)
        {
            foreach (var scene in allScenes)
            {
                scene.SetActive(scene == newScene);
            }
        }

        private void howToPlay()
        {
            setScene(howToPlayScreen);
        }
    }
}