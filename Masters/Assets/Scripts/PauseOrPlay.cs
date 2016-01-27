using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PauseOrPlay : MonoBehaviour
    {
        private bool pause;

        private HashSet<AudioSource> sourcesToPlay= new HashSet<AudioSource>();

        private void Start()
        {
            pause = Time.timeScale != 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (pause)
                {
                    sourcesToPlay = new HashSet<AudioSource>();
                    foreach (var audio in FindObjectsOfType<AudioSource>())
                    {
                        if (audio.isPlaying)
                        {
                            sourcesToPlay.Add(audio);
                            audio.Stop();
                        }
                    }
                    Time.timeScale = 0;
                }
                else
                {
                    foreach (var audio in sourcesToPlay)
                    {
                        audio.Play();
                    }
                    Time.timeScale = 1;
                }
                pause = !pause;
            }
        }
    }
}