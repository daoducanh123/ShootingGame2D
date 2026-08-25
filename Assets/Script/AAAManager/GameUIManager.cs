using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{

    public void StartGame()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.StartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.ResumeGame();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
