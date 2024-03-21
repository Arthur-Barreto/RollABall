using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void OnInstrutionButton()
    {
        SceneManager.LoadScene("Instructions");
    }
}
