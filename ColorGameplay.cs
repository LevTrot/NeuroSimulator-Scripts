using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts
{
    public class ColorGameplay : MonoBehaviour
    {
        [Header("UI Panels")]
        public GameObject startPanel;
        public GameObject endPanel;
        public GameObject gamePanel;

        [Header("UI Elements")]
        public TMP_Text timerText;
        public TMP_Text timerPlusText;
        public TMP_Text finalScoreText;
        public TMP_Text finalTimeText;
        public Image colorSquare;
        public Button[] colorButtons;
        public TMP_Text[] buttonTexts;

        private double timer;
        public double timerForEndGame = 0;
        public int correctAnswers = 0;
        public AudioSource audioSource;
        public AudioClip timerOver;
        public AudioClip gameOver;

        private string[] colorNames = {"Красный", "Синий", "Зеленый", "Желтый", "Бирюзовый", "Розовый", "Оранжевый", "Фиолетовый", "Белый", "Черный"};
        private Color[] colors =
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            new Color(0.5f, 1f, 1f),      // Бирюзовый 
            new Color(1f, 0.75f, 0.8f),   // Розовый
            new Color(1f, 0.55f, 0f),     // Оранжевый
            new Color(0.54f, 0.17f, 0.89f), // Фиолетовый
            Color.white,
            Color.black
        };

        private int correctColorIndex;
        private bool isPlaying = false;

        void Start()
        {
            ShowStartPanel();
            gamePanel.SetActive(false);
            endPanel.SetActive(false);

        }
        void Update()
        {
            if (isPlaying)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F1");
                if (timer <= 0)
                {
                    Debug.Log("Время вышло!" + timer);
                    audioSource.volume = 0.5f;
                    audioSource.PlayOneShot(timerOver);
                    EndGame();
                }
            }
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        void GenerateNewQuestion()
        {
            correctColorIndex = Random.Range(0, colorNames.Length);
            colorSquare.color = colors[correctColorIndex];

            int correctButton = Random.Range(0, colorButtons.Length);
            int textTrapButton;
            do
            {
                textTrapButton = Random.Range(0, colorButtons.Length);
            } while (textTrapButton == correctButton);

            for (int i = 0; i < colorButtons.Length; i++)
            {
                colorButtons[i].onClick.RemoveAllListeners();

                if (i == correctButton)
                {
                    int randomTextIndex;
                    do
                    {
                        randomTextIndex = Random.Range(0, colorNames.Length);
                    } while (randomTextIndex == correctColorIndex);

                    buttonTexts[i].text = colorNames[randomTextIndex];
                    buttonTexts[i].color = colors[correctColorIndex];
                    colorButtons[i].onClick.AddListener(() => OnCorrectAnswer());
                }
                else if (i == textTrapButton)
                {
                    int wrongColorIndex;
                    do
                    {
                        wrongColorIndex = Random.Range(0, colors.Length);
                    } while (wrongColorIndex == correctColorIndex);

                    buttonTexts[i].text = colorNames[correctColorIndex];
                    buttonTexts[i].color = colors[wrongColorIndex];
                    colorButtons[i].onClick.AddListener(() => OnWrongAnswer());
                }
                else
                {
                    int randomTextIndex = Random.Range(0, colorNames.Length);
                    int randomColorIndex = Random.Range(0, colors.Length);

                    while (randomColorIndex == correctColorIndex ||
                           randomTextIndex == correctColorIndex)
                    {
                        randomTextIndex = Random.Range(0, colorNames.Length);
                        randomColorIndex = Random.Range(0, colors.Length);
                    }

                    buttonTexts[i].text = colorNames[randomTextIndex];
                    buttonTexts[i].color = colors[randomColorIndex];
                    colorButtons[i].onClick.AddListener(() => OnWrongAnswer());
                }
            }
        }

        public void StartGame()
        {
            startPanel.SetActive(false);
            endPanel.SetActive(false);
            gamePanel.SetActive(true);

            isPlaying = true;
            if (timerForEndGame > 100)
            {
                timer = 5.0;
            }
            else if (timerForEndGame > 60)
            {
                timer = 10.0;
            }
            else if (timerForEndGame > 30)
            {
                timer = 12.5;
            }
            else
            {
                timer = 15.0;
            }
            timerForEndGame = timer;
            correctAnswers = 0;
            GenerateNewQuestion();
        }

        void OnCorrectAnswer()
        {
            Debug.Log("Верно!");
            correctAnswers++;
            if (timerForEndGame <= 20) 
            {
                timer += 2.0;
                timerForEndGame += 2.0;
                timerPlusText.text = "+2.0";
            }
            else if (timerForEndGame <= 30 && timerForEndGame > 20)
            {
                timer += 1.5;
                timerForEndGame += 1.5;
                timerPlusText.text = "+1.5";
            }
            else if (timerForEndGame <= 40 && timerForEndGame > 30)
            {
                timer += 1.0;
                timerForEndGame += 1.0;
                timerPlusText.text = "+1.0";
            }
            else if(timerForEndGame >= 40)
            {
                timer += 0.5;
                timerForEndGame += 0.5;
                timerPlusText.text = "+0.5";
            }
            Debug.Log(timerForEndGame);
            GenerateNewQuestion();
        }

        void OnWrongAnswer()
        {
            Debug.Log("Ошибка!");
            audioSource.volume = 0.25f;
            audioSource.PlayOneShot(gameOver);
            EndGame();
        }

        public void EndGame()
        {
            isPlaying = false;
            gamePanel.SetActive(false);
            endPanel.SetActive(true);
            finalScoreText.text = "Ваш счет: " + correctAnswers;
            finalTimeText.text = "Общее время: " + timerForEndGame.ToString("F1") + " секунд";
        }

        public void RestartGame()
        {
            StartGame();
        }

        void ShowStartPanel()
        {
            startPanel.SetActive(true);
            gamePanel.SetActive(false);
            endPanel.SetActive(false);
        }
    }
}