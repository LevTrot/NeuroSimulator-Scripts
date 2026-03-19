using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Quit : MonoBehaviour
    {
        public AudioSource music;
        //public AudioClip clip;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Debug.Log("Выход");
                Application.Quit();
            }
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                music.Stop();
            }
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                music.Play();
            }
        }
        
    }
}