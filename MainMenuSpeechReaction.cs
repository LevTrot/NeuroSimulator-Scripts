using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuSpeechReaction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Text infoText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "Вам предстоит выполнять короткие задания: быстро считать, печатать слова, искать числа по порядку и нажимать на появляющиеся области.\r\nКаждое упражнение длится несколько секунд и помогает развивать концентрацию и быстроту мышления.";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (infoText != null)
                infoText.text = "";
        }
    }
}