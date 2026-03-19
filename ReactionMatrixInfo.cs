using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ReactionMatrixInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public TMP_Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Матрица: вам нужно будет прокликать цифры в правильном порядке";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}