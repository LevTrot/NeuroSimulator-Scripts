using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class ReactionAudioInfo : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip click;
        public AudioClip math;
        public AudioClip word;
        public AudioClip matrix;
        
        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                audioSource.Stop();
            }
        }

        public void Click()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(click);
        }

        public void Word()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(word);
        }

        public void Matrix()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(matrix);
        }

        public void Math()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(math);
        }
    }
}