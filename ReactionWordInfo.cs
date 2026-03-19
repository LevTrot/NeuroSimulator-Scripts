using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ReactionWordInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public TMP_Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Слова: вам предстоит прослушать аудиосообщение и написать его вручную в поле ввода";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}