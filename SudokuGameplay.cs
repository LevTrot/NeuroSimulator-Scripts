using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.Windows;

namespace Assets.Scripts
{
    public class SudokuGameplay : MonoBehaviour
    {
        const int SIZE = 9;

        public GridCreate grid;
        public Transform gridParent;
        private CellPrefabScript[] cell;
        public Button difficult;
        public TextMeshProUGUI infoError;
        public TextMeshProUGUI infoTimer;
        public Image timerImage;

        public int dif = 0;
        private bool timerActive = false;
        private double timer = 0;
        System.Random r = new System.Random();
        System.Random rnd = new System.Random();

        int[,] solution = new int[SIZE, SIZE];
        int[,] puzzle = new int[SIZE, SIZE];


        IEnumerator Start()
        {
            yield return null;

            cell = gridParent.GetComponentsInChildren<CellPrefabScript>();
            for (int i = 0; i < 81; i++)
            {
                int r = i / 9;
                int c = i % 9;
                cell[i].Init(r, c, this);
            }

            Debug.Log("Найдено клеток: " + cell.Length);

            GenerateSudoku();
        }

        private void Update()
        {
            if(timerActive)
            {
                timer += Time.deltaTime;
                infoTimer.text = timer.ToString("F0");
            }
        }

        public void DifficultChange()
        {
            TextMeshProUGUI textButton = difficult.GetComponentInChildren<TextMeshProUGUI>();
            if (dif == 0)
            {
                dif++;
                textButton.text = "Средний";
            }
            else if (dif == 1)
            {
                dif++;
                textButton.text = "Сложный";
            }
            else if (dif == 2)
            {
                dif = 0;
                textButton.text = "Легкий";
            }
        }

        public void GenerateSudoku()
        {
            infoError.gameObject.SetActive(false);
            timer = 0;
            timerActive = true;
            GenerateFullSolution();
            if (dif == 0)
            {
                GeneratePuzzle(30);
            } 
            else if(dif == 1)
            {
                GeneratePuzzle(45);
            }
            else if(dif == 2)
            {
                GeneratePuzzle(60);
            }
            UpdateUI();
        }

        bool GenerateFullSolution()
        {
            return FillCell(0, 0);
        }

        bool FillCell(int row, int col) 
        {
            if(row == SIZE)
            {
                return true;
            }

            int nextRow = col == SIZE - 1 ? row + 1 : row;
            int nextCol = (col + 1) % SIZE;

            int[] numbers = Shuffle(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            foreach (int number in numbers) 
            {
                if (IsSafe(row, col, number))
                {
                    solution[row, col] = number;

                    if (FillCell(nextRow, nextCol))
                        return true;

                    solution[row, col] = 0;
                }
            }

            return false;
        }

        bool IsSafe(int row, int col, int n)
        {
            for (int i = 0; i < SIZE; i++)
                if (solution[row, i] == n) return false;
            for (int i = 0; i < SIZE; i++)
                if (solution[i, col] == n) return false;
            int br = row / 3 * 3;
            int bc = col / 3 * 3;
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (solution[br + r, bc + c] == n) return false;
            return true;
        }

        int[] Shuffle(int[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int rnd = r.Next(i + 1);
                (arr[i], arr[rnd]) = (arr[rnd], arr[i]);
            }
            return arr;
        }

        void GeneratePuzzle(int removed)
        {
            for(int r = 0; r < SIZE; r++)
            {
                for (int c = 0; c < SIZE; c++)
                {
                    puzzle[r, c] = solution[r, c];
                }
            }

            int count = removed;

            while (count > 0) 
            {
                int r = rnd.Next(9);
                int c = rnd.Next(9);

                if (puzzle[r, c] != 0)
                {
                    puzzle[r, c] = 0;
                    count--;
                }
            }
        }

        void UpdateUI()
        {
            for (int i = 0; i < 81; i++)
            {
                int r = i / 9;
                int c = i % 9;

                int val = puzzle[r, c];

                if (val == 0)
                {
                    cell[i].SetValue(0, true);
                }
                else
                {
                    cell[i].SetValue(val, false);
                }
                cell[i].GetComponentInChildren<TMP_InputField>().textComponent.color = Color.black;
            }
        }
        public void OnCellValueChanged(int row, int col, string value)
        {
            if (!int.TryParse(value, out int v) || v < 1 || v > 9)
            {
                puzzle[row, col] = 0;
                return;
            }

            puzzle[row, col] = v;
        }


        public void Endgame()
        {
            int ans = 0;
            timerActive = false;
            infoError.gameObject.SetActive(false);
            for (int i = 0; i < 81; i++)
            {
                TMP_InputField input = cell[i].GetComponentInChildren<TMP_InputField>();
                TMP_Text txt = input.textComponent;

                if (!cell[i].isEditable)
                {
                    ans++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(input.text))
                {
                    txt.color = Color.blue;
                    infoError.gameObject.SetActive(true);
                    continue;
                }

                int row = i / 9;
                int col = i % 9;

                if (!int.TryParse(input.text, out int val) || val < 1 || val > 9)
                {
                    txt.color = Color.red;
                    continue;
                }

                if (val < 1 || val > 9)
                {
                    ans++;
                }

                bool ok = true;
                for (int c = 0; c < 9; c++) 
                {
                    if (col !=c && puzzle[row, c] == val) 
                    { 
                        ok = false; 
                        break; 
                    }
                }

                for (int r = 0; r < 9; r++) 
                {
                    if(row != r &&  puzzle[r, col] == val)
                    {
                        ok = false;
                        break;
                    }
                }

                int br = row / 3 * 3;
                int bc = col / 3 * 3;
                for(int c = 0; c < 3; c++)
                {
                    for (int r = 0; r < 3; r++)
                    {
                        if(br + r != row && bc + c != col && puzzle[br + r, bc + c] == val)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if(!ok) { break; }
                }

                if (ok)
                {
                    txt.color = Color.green;
                } else
                {
                    txt.color = Color.red;
                }
            }
        }
    }
}