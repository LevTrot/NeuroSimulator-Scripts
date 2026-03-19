using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSecondGameMove : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Reaction");
    }
}
