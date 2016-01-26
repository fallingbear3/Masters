using System.Linq;
using Assets.Scripts.model;
using Shared.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SetupFight : MonoBehaviour
    {
        private Avatar player;
        private Avatar enemy;
        private Fighter.Type playerFighter;
        private Repository repo;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("asdfasdfads").GetComponent<Avatar>();
            enemy = GameObject.FindGameObjectWithTag("asdfasdfasdfasaf").GetComponent<Avatar>();

            player.Active = false;
            enemy.Active = false;

            repo = FindObjectsOfType<Repository>()[0];
            playerFighter = FindObjectsOfType<Repository>()[0].FighterType;
            player.GetComponentInChildren<SuitUp>().FighterType = playerFighter;
            enemy.GetComponentInChildren<SuitUp>().FighterType = Fighter.Type.Rocc;

            player.GetComponentInChildren<Avatar>().PlayerProfile.Profile.sprite =
                Resources.LoadAll<Sprite>("Profiles").First(sprite => sprite.name == playerFighter.ToString());

            GetComponent<Animator>().SetTrigger("Start");
        }

        public void StartTheGame()
        {
            player.Active = true;
            enemy.Active = true;
        }

        public void fightEnd(bool success)
        {
            if (success)
            {
                if (Fighter.HasNext(playerFighter))
                {
                    var next = Fighter.Next(playerFighter);
                    repo.FighterType = next;
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    EndGame();
                }
            }
        }

        private void EndGame()
        {
            GetComponent<Animator>().SetTrigger("Victory");
        }
    }
}