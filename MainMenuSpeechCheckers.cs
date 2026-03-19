using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuSpeechCheckers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Text infoText;    

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Шашки - Ходите по диагонали, стараясь побить все шашки соперника, перепрыгивая через них.\r\nКогда шашка достигает последней линии, она становится дамкой и может ходить в обе стороны.\r\nИгра тренирует внимание и стратегическое мышление.";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}