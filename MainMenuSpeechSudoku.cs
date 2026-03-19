using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuSpeechSudoku : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Судоку - Заполните пустые клетки цифрами от 1 до 9 так, чтобы в каждой строке, каждом столбце и каждом квадрате 3×3 не повторялись числа.\r\nИгра развивает логику и концентрацию.";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}