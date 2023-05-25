using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopUpContorller : MonoBehaviour
{
    public GameObject pauseMenu;
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ResumeGame()
    {


        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!gm.getPauseStatus())
        {
            PauseGame();
            return;
        }
        gm.PauseResumeGame();
        pauseMenu.SetActive(false);
    }
    public void PauseGame()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gm.getPauseStatus())
        {
            ResumeGame();
            return;
        }
        gm.PauseResumeGame();
        pauseMenu.SetActive(true);
    }
}
