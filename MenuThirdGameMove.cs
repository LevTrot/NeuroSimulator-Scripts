using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuThirdGameMove : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Checkers");
    }
}
