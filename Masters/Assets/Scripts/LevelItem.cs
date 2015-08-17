using Assets.Scripts.model;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelItem : MonoBehaviour
    {
        public GameObject levelPreview;
        public LevelInfo LevelInfo { get; private set; }

        public void init(LevelInfo levelInfo, bool unlocked)
        {
            LevelInfo = levelInfo;
            var resourceImage = Utils.loadSprite("Profiles", levelInfo.LevelName.ToString());

            var image = levelPreview.GetComponent<Image>();
            image.sprite = resourceImage;

            image.material = new Material(image.material);
            if (!unlocked)
            {
                GetComponent<Toggle>().interactable = false;
                // TODO fix the shader issue
                // image.material.SetFloat("_SaturationAmount", 0);
            }
        }
    }
}