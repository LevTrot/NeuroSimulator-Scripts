using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class BackToMainMenu : MonoBehaviour
    {
        //public AudioSource buttonClickClip;
        //public AudioClip clip;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }
}