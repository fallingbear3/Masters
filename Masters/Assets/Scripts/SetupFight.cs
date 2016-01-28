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

        public Animator PlayAgain;
        public Animator FightText;
        public Animator DeadText;
        public Animator EndText;
        public Animator WinnerText;
        public Animator Credits;
        public Animator FadeInOut;
        private float _time = 0.5f;
        private AudioManager audioManager;

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

            audioManager = FindObjectsOfType<AudioManager>().FirstOrDefault(manager => true);
           if(audioManager != null) audioManager.PlayB();
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
                    GoToMenuCoroutine();
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

        private void GoToMenuCoroutine()
        {
            if (audioManager != null) audioManager.PlayA();
            Invoke("FadeIn", 2);
            Invoke("GoToMenu", 3f);
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        private void Dead()
        {
            repo.FighterType = Fighter.Type.Janacek;
            EndText.SetTrigger("Show");
            GoToMenuCoroutine();
            player.Sleep();
            enemy.Sleep();
        }

        private void EndGame()
        {
            FadeInOut.SetTrigger("In");
            EndText.SetTrigger("Show");
            player.Sleep();
            enemy.Sleep();
            if (audioManager != null) audioManager.PlayA();
            Credits.SetTrigger("Show");
        }

        public void StartTheGame()
        {
            FadeInOut.SetTrigger("Out");
            FightText.SetTrigger("Show");

            enemy.gameObject.AddComponent<AiController>();
            switch (repo.FighterType)
            {
                case Fighter.Type.Janacek:
                    enemy.GetComponent<AiController>().Reflexes = 0.6f;
                    break;
                case Fighter.Type.Wagner:
                    enemy.GetComponent<AiController>().Reflexes = 0.4f;
                    break;
                case Fighter.Type.Martinu:
                    enemy.GetComponent<AiController>().Reflexes = 0.2f;
                    break;
                case Fighter.Type.Strauss:
                    enemy.GetComponent<AiController>().Reflexes = 0.125f;
                    break;
            }
            Invoke("WakeUp", 1f);
        }

        private void WakeUp()
        {
            player.WakeUp();
            enemy.WakeUp();
        }

        public void WinRound()
        {
            TurnOffActors();
            Destroy(enemy.GetComponent<AiController>());
            WinnerText.SetTrigger("Show");
            if (audioManager != null) audioManager.applase.Play();
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
            TurnOffActors();
            Destroy(enemy.GetComponent<AiController>());
            DeadText.SetTrigger("Show");
            if (audioManager != null) audioManager.laughter.Play();
            Invoke("FadeIn", 2);
            Invoke("RespoisitonPlayers", 2.5f);
            Invoke("StartTheGame", 2.5f);
        }

        private void TurnOffActors()
        {
            player.Active = false;
            enemy.Active = false;
        }

        private void RespoisitonPlayers()
        {
            player.Reposition();
            enemy.Reposition();
        }
    }
}