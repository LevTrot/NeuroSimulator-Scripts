using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class SudokuMusic : MonoBehaviour
    {
        public AudioSource music;

        public void MusicPlay()
        {
            if (music.isPlaying)
            {
                music.Pause();
            }
            else
            {
                music.Play();
            }
        }

        private void Update()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                if (music.isPlaying)
                {
                    music.Pause();
                }
                else 
                {
                    music.Play();
                }
            }
        }
    }
}