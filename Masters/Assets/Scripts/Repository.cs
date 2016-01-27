using DefaultNamespace;
using UnityEngine;

namespace Assets.Scripts.model
{
    public class Repository : MonoBehaviour
    {
        private Fighter.Type fighterType;

        public Fighter.Type FighterType
        {
            get { return PlayerPrefsSerializer.Load<Fighter.Type>("FighterType"); }
            set
            {
                fighterType = value;
                PlayerPrefsSerializer.Save("FighterType", fighterType);
            }
        }

        private void Awake()
        {
            // TODO remove
            FighterType = Fighter.Type.Strauss;
            // PlayerPrefs.DeleteAll();
        }
    }
}