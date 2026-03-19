using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ReactionMathInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public TMP_Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Задача: вам необходимо будет решить арифметическую задачу за отведенное время";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}