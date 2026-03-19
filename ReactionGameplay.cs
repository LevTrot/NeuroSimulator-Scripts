using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ReactionGameplay : MonoBehaviour
    {
        [Header("UI Panels")]
        public GameObject mathPanel;
        public GameObject matrixPanel;
        public GameObject clickPanel;
        public GameObject wordPanel;
        public GameObject startPanel;
        public GameObject endPanel;

        [Header("UI Elements")]
        public Button mathButton;
        public TMP_Text mathRightText;
        public TMP_Text mathQuestionText;
        public TMP_InputField mathInputField;
        public TMP_Text timerText;
        public TMP_Text endGameScore;
        public TMP_Text endGameTimerAllTime;
        public Button endGameRestart;
        public Button startGame;
        public Button wordEnter;
        public TMP_InputField wordEnterField;
        public Button wordRepeat;
        public TMP_Text wordRightText;
        public Button[] click = {};
        public Button[] matrixButtons = {};
        public TMP_Text matrixRight;
        public TMP_Text matrixTimer;
        public TMP_Text matrixInfo;
        public TMP_Text clickQuestion;

        private CanvasGroup cvLeftUp;
        private CanvasGroup cvLeft;
        private CanvasGroup cvLeftDown;
        private CanvasGroup cvUp;
        private CanvasGroup cvDown;
        private CanvasGroup cvRightUp;
        private CanvasGroup cvRight;
        private CanvasGroup cvRightDown;

        public AudioSource audioSource;
        public AudioSource voiceAudio;
        public AudioSource timerAudio;
        public AudioClip gameOver;
        public AudioClip gameOverTimer;
        public AudioClip word0;
        public AudioClip word1;
        public AudioClip word2;
        public AudioClip word3;
        public AudioClip word4;
        public AudioClip word5;
        public AudioClip word6;
        public AudioClip word7;
        public AudioClip word8;
        public AudioClip word9;
        public AudioClip clickGame;
        public AudioClip matrixIn;
        public AudioClip matrixOut;

        public double timer;
        public double timerAllTime;
        public int score;
        private int gameNumber;
        private int mathAnswer;
        private string wordAnswer;
        private bool timerActive = false;
        private int rand;
        private int clickTimes;
        private int[] matrixNumbers = new int[16];
        private int matrixTargetNumber;
        private int matrixDirection;

        void Start()
        {
            cvLeftUp = click[0].GetComponent<CanvasGroup>();
            cvLeft = click[1].GetComponent<CanvasGroup>();
            cvLeftDown = click[2].GetComponent<CanvasGroup>();
            cvUp = click[3].GetComponent<CanvasGroup>();
            cvDown = click[4].GetComponent<CanvasGroup>();
            cvRightUp = click[5].GetComponent<CanvasGroup>();
            cvRight = click[6].GetComponent<CanvasGroup>();
            cvRightDown = click[7].GetComponent<CanvasGroup>();
            mathPanel.SetActive(false);
            matrixPanel.SetActive(false);
            clickPanel.SetActive(false);
            wordPanel.SetActive(false);
            startPanel.SetActive(true);
            endPanel.SetActive(false);
        }

        void Update()
        {
            if (timerActive)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F1");
                timerAllTime += Time.deltaTime;
            }
            if (timerActive && timer <= 0)
            {
                audioSource.volume = 0.5f;
                audioSource.PlayOneShot(gameOverTimer);
                EndGame();
            }
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if (startPanel.activeSelf)
                {
                    startGame.onClick.Invoke();
                }
                else if (mathPanel.activeSelf)
                {
                    mathButton.onClick.Invoke();
                }
                else if (wordPanel.activeSelf)
                {
                    wordEnter.onClick.Invoke();
                }
                else if (clickPanel.activeSelf)
                {
                    //Button.onClick.Invoke();
                }
                else if (matrixPanel.activeSelf)
                {
                    //mathButton.onClick.Invoke();
                }
                else if (endPanel.activeSelf)
                {
                    endGameRestart.onClick.Invoke();
                }
            }
        }

        public void StartGame()
        {
            endPanel.SetActive(false);
            timerAllTime = 0;
            score = 0;
            RandomGame();
            startPanel.SetActive(false);
        }

        void RandomGame()
        {
            timerAudio.Stop();
            gameNumber = Random.Range(0, 4);
            if (gameNumber == 0)
            {
                timerAudio.Play();
                timerActive = true;
                MathGame();
            }
            else if (gameNumber == 1)
            {
                timerAudio.Play();
                timerActive = true;
                WordGame();
            }
            else if (gameNumber == 2)
            {
                timerActive = false;
                Debug.Log("clickGame");
                ClickGame();
            }
            else if (gameNumber == 3)
            {
                timerAudio.Play();
                timerActive = true;
                MatrixGame();
            }
        }

        void MathGame() 
        {
            mathPanel.SetActive(true);
            int num1, num2, ns;
            num1 = Random.Range(0, 100);
            num2 = Random.Range(0, 100);
            ns = Random.Range(0, 4);
            timer = 30;
            switch (ns)
            {
                case 0: 
                    mathQuestionText.text = "Сколько будет: " + num1 + " + " + num2 + "?";
                    mathAnswer = num1 + num2;
                    break;

                case 1:
                    mathQuestionText.text = "Сколько будет: " + num1 + " - " + num2 + "?";
                    mathAnswer = num1 - num2;
                    break;

                case 2:
                    num1 = Random.Range(1, 31);
                    num2 = Random.Range(1, 31);
                    mathQuestionText.text = "Сколько будет: " + num1 + " * " + num2 + "?";
                    mathAnswer = num1 * num2;
                    timer += 15;
                    break;

                case 3:
                    num1 = Random.Range(1, 301);
                    num2 = Random.Range(1, 31);
                    mathQuestionText.text = "Сколько будет: " + num1 + " / " + num2 + "?";
                    mathAnswer = num1 / num2;
                    timer += 5;
                    break;
            }
            Debug.Log(mathAnswer);
        }

        public void MathGameAnswered()
        {
            if (int.TryParse(mathInputField.text.Trim(), out int answer) && mathAnswer == answer)
            {
                mathRightText.text = "Верно!";
                mathRightText.color = Color.green;
                score++;
                StartCoroutine(MathWaitAndNextGame());

            }
            else
            {
                mathRightText.text = "Неверно: " + mathAnswer;
                mathRightText.color = Color.red;
                StartCoroutine(MathEndGameAfterDelay());
            }
        }

        void WordGame()
        {
            wordPanel.SetActive(true);
            rand = Random.Range(0, 10);
            timer = 35;
            switch (rand)
            {
                case 0:
                    voiceAudio.PlayOneShot(word0);
                    wordAnswer = "сегодня хорошая погода";
                    break;
                case 1:
                    voiceAudio.PlayOneShot(word1);
                    wordAnswer = "кошка спит на подоконнике";
                    break;
                case 2:
                    voiceAudio.PlayOneShot(word2);
                    wordAnswer = "я люблю пить чай по утрам";
                    break;
                case 3:
                    voiceAudio.PlayOneShot(word3);
                    wordAnswer = "в парке поют птицы";
                    break;
                case 4:
                    voiceAudio.PlayOneShot(word4);
                    wordAnswer = "мой телефон разрядился";
                    break;
                case 5:
                    voiceAudio.PlayOneShot(word5);
                    wordAnswer = "собака бежит по дорожке";
                    break;
                case 6:
                    voiceAudio.PlayOneShot(word6);
                    wordAnswer = "на улице идет сильный дождь";
                    break;
                case 7:
                    voiceAudio.PlayOneShot(word7);
                    wordAnswer = "пора приготовить ужин";
                    break;
                case 8:
                    voiceAudio.PlayOneShot(word8);
                    wordAnswer = "в библиотеке тихо и спокойно";
                    break;
                case 9:
                    voiceAudio.PlayOneShot(word9);
                    wordAnswer = "поезд уехал без опоздания";
                    break;
            }
            Debug.Log(wordAnswer);
        }

        public void WordGameAnswered()
        {
            if (wordEnterField.text.ToLower() == wordAnswer)
            {
                wordRightText.text = "Верно!";
                wordRightText.color = Color.green;
                score++;
                StartCoroutine(WordWaitAndNextGame());
            }
            else
            {
                wordRightText.text = "Неверно: ";
                wordRightText.color = Color.red;
                StartCoroutine(WordEndGameAfterDelay());
            }
        }

        public void WordGameRepeat()
        {
            if (voiceAudio.isPlaying)
            {
                voiceAudio.Stop();
            }
            switch (rand)
            {
                case 0:
                    voiceAudio.PlayOneShot(word0);
                    break;
                case 1:
                    voiceAudio.PlayOneShot(word1);
                    break;
                case 2:
                    voiceAudio.PlayOneShot(word2);
                    break;
                case 3:
                    voiceAudio.PlayOneShot(word3);
                    break;
                case 4:
                    voiceAudio.PlayOneShot(word4);
                    break;
                case 5:
                    voiceAudio.PlayOneShot(word5);
                    break;
                case 6:
                    voiceAudio.PlayOneShot(word6);
                    break;
                case 7:
                    voiceAudio.PlayOneShot(word7);
                    break;
                case 8:
                    voiceAudio.PlayOneShot(word8);
                    break;
                case 9:
                    voiceAudio.PlayOneShot(word9);
                    break;
            }
        }

        void ClickGame()
        {
            clickPanel.SetActive(true);
            timer = 30;
            clickQuestion.text = "Жмите на круги!";
            foreach (Button btn in click)
            {
                CanvasGroup cg = btn.GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
            timerActive = true;
            clickTimes = -1;
            voiceAudio.PlayOneShot(clickGame);
            ClickButtonActivated();
            timerAudio.Play();
        }

        public void ClickButtonActivated()
        {
            clickTimes++;
            rand = Random.Range(0, 8);
            foreach (Button btn in click)
            {
                CanvasGroup cg = btn.GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
            if (clickTimes >= 10)
            {
                timer += 2.5;
                score++;
                mathPanel.SetActive(false);
                timerActive = false;
                clickQuestion.text = "";
                RandomGame();
            }
            else
            {
                switch (rand)
                {
                    case 0:
                        cvLeftUp.alpha = 1;
                        cvLeftUp.interactable = true;
                        cvLeftUp.blocksRaycasts = true;
                        break;
                    case 1:
                        cvLeft.alpha = 1;
                        cvLeft.interactable = true;
                        cvLeft.blocksRaycasts = true;
                        break;
                    case 2:
                        cvLeftDown.alpha = 1;
                        cvLeftDown.interactable = true;
                        cvLeftDown.blocksRaycasts = true;
                        break;
                    case 3:
                        cvUp.alpha = 1;
                        cvUp.interactable = true;
                        cvUp.blocksRaycasts = true;
                        break;
                    case 4:
                        cvDown.alpha = 1;
                        cvDown.interactable = true;
                        cvDown.blocksRaycasts = true;
                        break;
                    case 5:
                        cvRightUp.alpha = 1;
                        cvRightUp.interactable = true;
                        cvRightUp.blocksRaycasts = true;
                        break;
                    case 6:
                        cvRight.alpha = 1;
                        cvRight.interactable = true;
                        cvRight.blocksRaycasts = true;
                        break;
                    case 7:
                        cvRightDown.alpha = 1;
                        cvRightDown.interactable = true;
                        cvRightDown.blocksRaycasts = true;
                        break;
                }
            }
        }

        void MatrixGame()
        {
            matrixPanel.SetActive(true);
            timer = 30;
            timerActive = true;
            matrixDirection = Random.Range(0, 2);
            if (matrixDirection != 1)
            {
                matrixInfo.text = "Кликните на все цифры в порядке убывания";
                voiceAudio.PlayOneShot(matrixOut);
                matrixDirection = -1;
            }
            else 
            {
                matrixInfo.text = "Кликните на все цифры в порядке возрастания";
                voiceAudio.PlayOneShot(matrixIn);
            }
            GenerateMatrix();
        }

        void GenerateMatrix()
        {
            List<int> ints = new List<int>();
            for (int i = 1; i <= 16; i++)
            {
                ints.Add(i);
            }
            for (int i = 0; i < 16; i++)
            {
                int random = Random.Range(0, ints.Count);
                matrixNumbers[i] = ints[random];
                ints.RemoveAt(random);
            }
            if (matrixDirection == 1)
            {
                matrixTargetNumber = 1;
            }
            else
            {
                matrixTargetNumber = 16;
            }
            for (int i = 0; i < 16; i++)
            {
                TMP_Text text = matrixButtons[i].GetComponentInChildren<TMP_Text>();
                text.text = matrixNumbers[i].ToString();

                matrixButtons[i].interactable = true;
                matrixButtons[i].GetComponent<Image>().color = Color.black;
            }

        }

        public void MatrixGameClick(Button btn)
        {
            TMP_Text txt = btn.GetComponentInChildren<TMP_Text>();
            int clinckedNumber = int.Parse(txt.text);

            if(clinckedNumber == matrixTargetNumber)
            {
                btn.GetComponent<Image>().color = Color.green;
                btn.interactable = false;
                matrixTargetNumber += matrixDirection;
                if ((matrixDirection == 1 && matrixTargetNumber > 16) || (matrixDirection == -1 && matrixTargetNumber < 1))
                {
                    matrixRight.text = "Верно!";
                    StartCoroutine(MatrixWaitAndNextGame());
                }
            }
            else
            {
                btn.GetComponent<Image>().color = Color.red;
                matrixTimer.text = "-5.0";
                timer -= 5;
            }
        }

        IEnumerator MathWaitAndNextGame()
        {
            timerAudio.Stop();
            timer = +2.5;
            timerActive = false;
            timer = 0;
            yield return new WaitForSeconds(2f);
            mathInputField.placeholder.GetComponent<TMP_Text>().text = "Введите только целое...";
            mathRightText.text = " ";
            mathInputField.text = "";
            mathPanel.SetActive(false);
            RandomGame();
        }

        IEnumerator WordWaitAndNextGame()
        {
            timerAudio.Stop();
            timer = +2.5;
            timerActive = false;
            timer = 0;
            yield return new WaitForSeconds(2f);
            wordEnterField.placeholder.GetComponent<TMP_Text>().text = "Введите услышанное...";
            wordRightText.text = " ";
            wordEnterField.text = "";
            wordPanel.SetActive(false);
            RandomGame();
        }

        IEnumerator MathEndGameAfterDelay()
        {
            timerActive = false;
            timerAudio.Stop();
            timer = 0;
            yield return new WaitForSeconds(2f);
            EndGame();
        }

        IEnumerator WordEndGameAfterDelay()
        {
            timerActive = false;
            timer = 0;
            timerAudio.Stop();
            yield return new WaitForSeconds(2f);
            EndGame();
        }

        IEnumerator MatrixWaitAndNextGame()
        {
            timerAudio.Stop();
            timerActive = false;
            timer = 0;
            score++;
            yield return new WaitForSeconds(2f);
            matrixRight.text = "";
            matrixTimer.text = "";
            matrixInfo.text = "";
            matrixPanel.SetActive(false);
            RandomGame();
        }

        void EndGame()
        {
            timerAudio.Stop();
            mathInputField.placeholder.GetComponent<TMP_Text>().text = "Введите только целое...";
            mathRightText.text = "";
            mathInputField.text = "";
            wordEnterField.placeholder.GetComponent<TMP_Text>().text = "Введите услышанное...";
            wordRightText.text = "";
            wordEnterField.text = "";
            timerActive = false;
            timer = 0;
            mathPanel.SetActive(false);
            matrixPanel.SetActive(false);
            clickPanel.SetActive(false);
            wordPanel.SetActive(false);
            startPanel.SetActive(false);
            endPanel.SetActive(true);
            endGameTimerAllTime.text = "Общее время: " + timerAllTime.ToString("F1");
            endGameScore.text = "Правильных ответов: " + score.ToString();
        }
    }
}