using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Click : MonoBehaviour, /*IPointerEnterHandler,*/ IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    //public void OnPointerEnter(PointerEventData eventData)
    //{
        //if (hoverClip) audioSource.PlayOneShot(hoverClip);
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        if (clickClip) audioSource.PlayOneShot(clickClip);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            audioSource.Stop();
        }
    }
}
