using System.Linq;
using Assets.Scripts.Ai;
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

        public Animator FightText;
        public Animator DeadText;
        public Animator EndText;
        public Animator WinnerText;
        public Animator Credits;
        public Animator FadeInOut;
        private float _time = 0.5f;

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

            StartTheGame();
            //restartScene();
        }

        public void fightEnd(bool success)
        {
            if (success)
            {
                if (Fighter.HasNext(playerFighter))
                {
                    var next = Fighter.Next(playerFighter);
                    repo.FighterType = next;

                    WinnerText.SetTrigger("Show");
                    FadeInOut.SetTrigger("In");
                    Invoke("GoToMenu", 0.5f);
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

        private void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        private void Dead()
        {
            DeadText.SetTrigger("Show");
            player.Sleep();
            enemy.Sleep();
        }

        private void EndGame()
        {
            FadeInOut.SetTrigger("In");
            EndText.SetTrigger("Show");
            player.Sleep();
            enemy.Sleep();
            Credits.SetTrigger("Show");
        }

        public void StartTheGame()
        {
            FadeInOut.SetTrigger("Out");
            FightText.SetTrigger("Show");

            enemy.GetComponent<AiController>().Restart();
            Invoke("WakeUp", 1f);
        }

        private void WakeUp()
        {
            player.WakeUp();
            enemy.WakeUp();
        }

        public void WinRound()
        {
            player.Active = false;
            enemy.Active = false;
            WinnerText.SetTrigger("Show");
            Invoke("FadeIn", 2);
            Invoke("RespoisitonPlayers", 2.5f);
            Invoke("StartTheGame", 2.5f);
        }

        private void FadeIn()
        {
            FadeInOut.SetTrigger("In");
        }


        public void FailRound()
        {
            player.Active = false;
            enemy.Active = false;
            DeadText.SetTrigger("Show");
            Invoke("FadeIn", 2);
            Invoke("RespoisitonPlayers", 2.5f);
            Invoke("StartTheGame", 2.5f);
        }

        private void RespoisitonPlayers()
        {
            player.Reposition();
            enemy.Reposition();
        }
    }
}