using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
