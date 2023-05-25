using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneZero : MonoBehaviour
{
    public void GoToZero()
    {
        SceneManager.LoadScene(0);
    }
}
