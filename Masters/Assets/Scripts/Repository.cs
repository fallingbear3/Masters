using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.model
{
    public class Repository : MonoBehaviour
    {
        private LevelInfo currentLevel;
        // TODO add another level modes private LevelMode mode;
        private Dictionary<LevelName, float> personalBest;
        private Dictionary<LevelName, float> raceProgress;
        private List<LevelName> unlockedLevels;
        private bool autoSignIn;
        private bool? firstTime;

        public LevelInfo CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;
                PlayerPrefsSerializer.Save("CurrentLevel", currentLevel);
            }
        }

       /* public LevelMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                PlayerPrefsSerializer.Save("Mode", value);
            }
        }*/

        /*public InputSettings InputSettings
        {
            get { return inputSettings; }
            set
            {
                inputSettings = value;
                PlayerPrefsSerializer.Save("InputSettings", value);
            }
        }*/

        /*public bool AutoSignIn
        {
            get { return autoSignIn; }
            set
            {
                autoSignIn = value;
                PlayerPrefsSerializer.Save("AutoSignIn", value);
            }
        }*/

        public float? getRaceProgress(LevelInfo levelInfo)
        {
            return levelInfo != null && raceProgress.ContainsKey(levelInfo.LevelName) ? raceProgress[levelInfo.LevelName] : (float?)null;
        }

        public void setRaceProgress(LevelInfo levelInfo, float time)
        {
            raceProgress[levelInfo.LevelName] = time;
            PlayerPrefsSerializer.Save("RaceProgress", raceProgress);
        }

        public List<LevelName> UnlockedLevels
        {
            get { return unlockedLevels; }
            set
            {
                unlockedLevels = value;
                PlayerPrefsSerializer.Save("UnlockedLevels", value);
            }
        }

       /* public float? getPersonalBest(LevelName levelName)
        {
            return personalBest.ContainsKey(levelName) ? personalBest[levelName] : (float?)null;
        }
        public void setPersonalBest(LevelName levelName, float time)
        {
            personalBest[levelName] = time;
            PlayerPrefsSerializer.Save("PersonalBest", personalBest);
        }

        public bool FirstTime
        {
            get { return firstTime.Value; }
            set
            {
                firstTime = value;
                PlayerPrefsSerializer.Save("FirstTime", value);
            }
        }*/

        private void Awake()
        {
            currentLevel = PlayerPrefsSerializer.Load<LevelInfo>("CurrentLevel");
            /*mode = PlayerPrefsSerializer.Load<LevelMode>("Mode");
            inputSettings = PlayerPrefsSerializer.Load<InputSettings>("InputSettings");*/
            personalBest = PlayerPrefsSerializer.Load<Dictionary<LevelName, float>>("PersonalBest");
            raceProgress = PlayerPrefsSerializer.Load<Dictionary<LevelName, float>>("RaceProgress");
            unlockedLevels = PlayerPrefsSerializer.Load<List<LevelName>>("UnlockedLevels");
            autoSignIn = PlayerPrefsSerializer.Load<bool>("AutoSignIn");
            firstTime = PlayerPrefsSerializer.Load<bool?>("FirstTime");

            ensureValidity();
        }

        private void ensureValidity()
        {
            if (Debug.isDebugBuild)
            {
                //if (!PlayerPrefs.HasKey("Mode")) mode = LevelMode.Practice;
                if (!PlayerPrefs.HasKey("CurrentLevel")) currentLevel = LevelInfo.Janacek;
            }
            if (!PlayerPrefs.HasKey("RaceProgress")) raceProgress = new Dictionary<LevelName, float>();
            if (!PlayerPrefs.HasKey("UnlockedLevels")) unlockedLevels = new List<LevelName> { LevelInfo.Levels.First().LevelName };

           /* if (!PlayerPrefs.HasKey("InputSettings")) inputSettings = new InputSettings();

            if (inputSettings.InputType == InputManager.Type.None)
            {
                if (Application.isMobilePlatform)
                {
                    if (SystemInfo.supportsAccelerometer || SystemInfo.supportsGyroscope)
                    {
                        inputSettings.InputType = InputManager.Type.Gyro;
                    }
                    else
                    {
                        inputSettings.InputType = InputManager.Type.Joystick;
                    }
                }
                else
                {
                    inputSettings.InputType = InputManager.Type.External;
                }
            }

            if (!inputSettings.GyroInputSettings.Sensitivity.HasValue)
            {
                inputSettings.GyroInputSettings.Sensitivity = 0.5f;
            }

            if (!inputSettings.GyroInputSettings.Tilt.HasValue)
            {
                inputSettings.GyroInputSettings.Tilt = 0f;
            }*/

            if (personalBest == null)
            {
                personalBest = new Dictionary<LevelName, float>();
            }

            if (!firstTime.HasValue)
            {
                firstTime = true;
            }
        }
    }
}