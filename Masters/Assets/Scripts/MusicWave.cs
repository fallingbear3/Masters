using UnityEngine;

namespace Assets.Scripts
{

    [RequireComponent(typeof(Animator))]
    public class MusicWave : MonoBehaviour
    {
        public void execute()
        {
            GetComponent<Animator>().SetTrigger("MusicWave");
        } 
    }
}