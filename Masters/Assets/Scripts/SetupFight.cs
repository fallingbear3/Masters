using System.Linq;
using Assets.Scripts.model;
using Shared.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SetupFight : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;
        private Fighter.Type playerFighter;
        private Repository repo;

        private void Awake()
        {
            repo = FindObjectsOfType<Repository>()[0];
            playerFighter = FindObjectsOfType<Repository>()[0].FighterType;
            player.GetComponentInChildren<SuitUp>().FighterType = playerFighter;
            enemy.GetComponentInChildren<SuitUp>().FighterType = Fighter.Type.Rocc;

            player.GetComponentInChildren<Avatar>().PlayerProfile.Profile.sprite =
                Resources.LoadAll<Sprite>("Profiles").First(sprite => sprite.name == playerFighter.ToString());
        }

        public void fightEnd(bool success)
        {
            if (success)
            {
                var next  = Fighter.Next(playerFighter);
                repo.FighterType = next;
                SceneManager.LoadScene("FightScene");
            }
        }
    }
}