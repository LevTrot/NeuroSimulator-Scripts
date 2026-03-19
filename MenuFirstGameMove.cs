using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFirstGameMove : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("TrueColors");
    }
}
