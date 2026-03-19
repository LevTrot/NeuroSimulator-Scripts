using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellPrefabScript : MonoBehaviour
    {
        public TextMeshProUGUI textValue;
        public TMP_InputField inputField;

        public bool isEditable = false;

        private int row;
        private int col;
        private SudokuGameplay gameplay;

        public void Init(int r, int c, SudokuGameplay g)
        {
            row = r;
            col = c;
            gameplay = g;

            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        void OnValueChanged(string value)
        {
            gameplay.OnCellValueChanged(row, col, value);
        }

        public void SetValue(int n, bool editable)
        {
            isEditable = editable;

            if (editable)
            {
                textValue.text = "";
                inputField.text = "";
                inputField.interactable = true;
            }
            else
            {
                textValue.text = n.ToString();
                inputField.text = "";
                inputField.interactable = false;
            }
        }
    }
}