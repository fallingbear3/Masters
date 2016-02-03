using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class NavigationManager : MonoBehaviour
    {
        public static bool SwitchToChooseOponentScreen;

        public GameObject startScreen;
        public GameObject storyScreen;
        public GameObject howToPlayScreen;
        public GameObject chooseOpponentScene;

        private GameObject[] allScenes;

        private void Start()
        {
            allScenes = new[] {startScreen, storyScreen, howToPlayScreen, chooseOpponentScene};
            if (SwitchToChooseOponentScreen)
            {
                setScene(chooseOpponentScene);
            }
            else
            {
                setScene(startScreen);
                Invoke("story", 10);
                Invoke("howToPlay", 20);
            }
        }

        private void setScene(GameObject newScene)
        {
            foreach (var scene in allScenes)
            {
                scene.SetActive(scene == newScene);
            }
        }

        private void story()
        {
            setScene(storyScreen);
        }
        private void howToPlay()
        {
            setScene(howToPlayScreen);
        }

        public void chooseOponnetn()
        {
            setScene(chooseOpponentScene);
        }

        public void StartScene()
        {
            SwitchToChooseOponentScreen = true;
            SceneManager.LoadScene("FightScene");
        }
    }
}