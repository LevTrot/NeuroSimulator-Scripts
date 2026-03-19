using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ReactionClickInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public TMP_Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Светлячки: на экране будут появляться зеленые круги на которые необходимо нажать за отведенное время";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}