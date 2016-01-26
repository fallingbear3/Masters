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


            repo = FindObjectsOfType<Repository>()[0];
            playerFighter = FindObjectsOfType<Repository>()[0].FighterType;
            player.GetComponentInChildren<SuitUp>().FighterType = playerFighter;
            enemy.GetComponentInChildren<SuitUp>().FighterType = Fighter.Type.Rocc;

            player.GetComponentInChildren<Avatar>().PlayerProfile.Profile.sprite =
                Resources.LoadAll<Sprite>("Profiles").First(sprite => sprite.name == playerFighter.ToString());

            restartScene();
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
                    repo.FighterType = Fighter.Type.Janacek;
                    EndGame();
                }
            }
            else
            {
                Dead();
            }
        }

        private void Dead()
        {
            GetComponent<Animator>().SetTrigger("Dead");
            player.Active = false;
            enemy.Active = false;
        }
        private void EndGame()
        {
            GetComponent<Animator>().SetTrigger("Victory");
            player.Active = false;
            enemy.Active = false;
        }

        public void restartScene()
        {
            player.Active = false;
            enemy.Active = false;
            player.Restart();
            enemy.Restart();
            GetComponent<Animator>().SetTrigger("Start");
        }
    }
}