using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuSpeechColors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "На экране появляется цветной квадрат и несколько слов. Нужно выбрать слово, написанное цветом, совпадающим с цветом квадрата, даже если само слово обозначает другой цвет.\r\nИгра развивает концентрацию, внимание и способность быстро принимать решения.";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}