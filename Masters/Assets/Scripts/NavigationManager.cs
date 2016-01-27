using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class NavigationManager : MonoBehaviour
    {
        public static bool SwitchToChooseOponentScreen;

        public GameObject startScreen;
        public GameObject howToPlayScreen;
        public GameObject chooseOpponentScene;

        private GameObject[] allScenes;

        private void Start()
        {
            allScenes = new[] {startScreen, howToPlayScreen, chooseOpponentScene};
            if (SwitchToChooseOponentScreen)
            {
                setScene(chooseOpponentScene);
            }
            else
            {
                setScene(startScreen);
                Invoke("howToPlay", 5);
            }
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