using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class AudioManager : MonoBehaviour
    {
        public float sourceAVolume = 0.15f;
        public float sourceBVolume = 0.2f;

        public AudioSource applase;
        public AudioSource laughter;

        public AudioSource sourceA;
        public AudioSource sourceB;

        private bool playingA;
        private bool playingB;

        private float duration = 5f;

        private void Awake()
        {
            var audios = FindObjectsOfType<AudioManager>();
            for (int i = 1; i < audios.Length; i++)
            {
                Destroy(audios[i]);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            PlayA();
        }

        public void PlayA()
        {
            if (playingB)
            {
                sourceB.volume = sourceBVolume;
                sourceB.DOFade(0, duration);
                playingB = false;
            }
            if (!playingA)
            {
                sourceA.Play();
                sourceA.volume = 0;
                sourceA.DOFade(sourceAVolume, duration);
                playingA = true;
            }
        }

        public void PlayB()
        {
            if (playingA)
            {
                sourceA.volume = sourceAVolume;
                sourceA.DOFade(0, duration);
                playingA = false;
            }
            if (!playingB)
            {
                sourceB.Play();
                sourceB.volume = 0;
                sourceB.DOFade(sourceBVolume, duration);
                playingB = true;
            }
        }
    }
}