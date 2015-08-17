using System;
using Assets.Scripts.model;
using Assets.Shared.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ui.menu
{
    internal class RaceLevelItemsInit : MonoBehaviour
    {
        public delegate void OnLevelSelectedHandler(LevelInfo levelName);

        public LevelItem levelItem;
        private Repository repo;
        public event OnLevelSelectedHandler OnLevelSelected;

        protected void Start()
        {
            repo = GameObject.FindGameObjectWithTag("Repository").GetComponent<Repository>();
            transform.RemoveAllChildren();

            foreach (LevelInfo levelInfo in LevelInfo.Levels)
            {
                addLevelItem(levelInfo);
            }
        }

        private void addLevelItem(LevelInfo levelInfo)
        {
            var levelItemCopy = Instantiate(levelItem);
            levelItemCopy.GetComponent<Toggle>().onValueChanged.AddListener(value => repo.CurrentLevel = levelInfo);
            levelItemCopy.transform.SetParent(transform, false);
            levelItemCopy.transform.localScale = Vector3.one;
            init(levelItemCopy, levelInfo);
        }

        private void init(LevelItem levelItemCopy, LevelInfo levelInfo)
        {
            var unlocked = levelInfo.previousLevel() == null || repo.getRaceProgress(levelInfo.previousLevel()) != null;
            levelItemCopy.init(levelInfo, unlocked);

            if (unlocked)
            {
                levelItemCopy.gameObject.Submit();
                repo.CurrentLevel = levelInfo;
            }
        }

        public void startLevel()
        {
            if (repo.CurrentLevel == null)
            {
                throw new InvalidOperationException("Trying to start level but no information about what level to start is available.");
            }
            //repo.Mode = LevelMode.Challenge;
            Application.LoadLevel(repo.CurrentLevel.LevelName.ToString());
        }
    }
}